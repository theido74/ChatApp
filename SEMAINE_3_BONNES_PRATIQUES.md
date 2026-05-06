# SEMAINE 3 - Bonnes Pratiques DataAccess
## Best Practices for Forum and Private Messages | Week 3

---

## TABLE OF CONTENTS
- [Section 1: SQL Injection Prevention](#section-1-sql-injection-prevention)
- [Section 2: Transaction Management](#section-2-transaction-management)
- [Section 3: Connection Pooling](#section-3-connection-pooling)
- [Section 4: Logging and Audit Trails](#section-4-logging-and-audit-trails)
- [Section 5: Error Handling Strategies](#section-5-error-handling-strategies)
- [Section 6: Performance Optimization](#section-6-performance-optimization)

---

## SECTION 1: SQL Injection Prevention

### 1.1 Parameterized Queries (GOOD PRACTICE)

```vb
' ✓ GOOD - Using parameterized queries
Public Function GetUserMessages(userId As Integer) As List(Of PrivateMessage)
    Const sqlQuery As String = "
        SELECT MESSAGE_ID, SENDER_ID, SUBJECT, CONTENT
        FROM MESSAGES
        WHERE RECIPIENT_ID = :userId"

    Dim messages As New List(Of PrivateMessage)

    Using connection As New OracleConnection(_connectionString)
        Using command As New OracleCommand(sqlQuery, connection)
            ' Parameter binding prevents SQL injection
            command.Parameters.Add(":userId", OracleDbType.Decimal).Value = userId

            connection.Open()
            Using reader As OracleDataReader = command.ExecuteReader()
                While reader.Read()
                    messages.Add(New PrivateMessage With {
                        .MessageId = CInt(reader("MESSAGE_ID")),
                        .SenderId = CInt(reader("SENDER_ID")),
                        .Subject = reader("SUBJECT").ToString(),
                        .Content = reader("CONTENT").ToString()
                    })
                End While
            End Using
        End Using
    End Using

    Return messages
End Function
```

### 1.2 String Concatenation (BAD PRACTICE - DANGEROUS)

```vb
' ✗ BAD - VULNERABLE TO SQL INJECTION
Public Function GetUserMessages_UNSAFE(userId As String) As List(Of PrivateMessage)
    ' NEVER DO THIS - User input directly in SQL string
    Dim sqlQuery As String = "SELECT MESSAGE_ID, SENDER_ID, SUBJECT FROM MESSAGES WHERE RECIPIENT_ID = " & userId
    
    ' If userId contains: "1; DROP TABLE MESSAGES; --" the entire table would be deleted!
    ' Attack: GetUserMessages_UNSAFE("1; DROP TABLE MESSAGES; --")
    
    Dim messages As New List(Of PrivateMessage)
    ' ... rest of code would execute malicious SQL
    Return messages
End Function
```

### 1.3 Input Validation (DEFENSE IN DEPTH)

```vb
' ✓ GOOD - Multi-layer validation approach
Public Function ValidateAndCreatePost(post As ForumPost) As Integer
    ' Layer 1: Null/Empty check
    If String.IsNullOrWhiteSpace(post.Content) Then
        Throw New ArgumentException("Post content cannot be empty")
    End If

    ' Layer 2: Length validation
    If post.Content.Length > 5000 Then
        Throw New ArgumentException("Post content exceeds maximum length of 5000 characters")
    End If

    ' Layer 3: Prevent HTML/Script injection
    Dim sanitized As String = SanitizeInput(post.Content)
    post.Content = sanitized

    ' Layer 4: Parameterized query (prevents SQL injection)
    Const sqlQuery As String = "
        INSERT INTO FORUMS_POSTS (POST_ID, TOPIC_ID, CONTENT, CREATED_AT)
        VALUES (FORUMS_POSTS_SEQ.NEXTVAL, :topicId, :content, SYSDATE)
        RETURNING POST_ID INTO :postId"

    Using connection As New OracleConnection(_connectionString)
        Using command As New OracleCommand(sqlQuery, connection)
            command.Parameters.Add(":topicId", OracleDbType.Decimal).Value = post.TopicId
            command.Parameters.Add(":content", OracleDbType.Varchar2).Value = sanitized

            Dim postIdParam As New OracleParameter(":postId", OracleDbType.Decimal)
            postIdParam.Direction = ParameterDirection.Output
            command.Parameters.Add(postIdParam)

            connection.Open()
            command.ExecuteNonQuery()

            Return CInt(postIdParam.Value)
        End Using
    End Using
End Function

''' <summary>
''' Sanitizes user input to prevent XSS attacks
''' </summary>
Private Function SanitizeInput(input As String) As String
    Return System.Web.HttpUtility.HtmlEncode(input)
End Function
```

---

## SECTION 2: Transaction Management

### 2.1 ACID Transactions (GOOD PRACTICE)

```vb
' ✓ GOOD - Proper transaction handling with rollback
Public Function TransferMessage(fromUserId As Integer, toUserId As Integer, 
                               messageContent As String) As Boolean
    Dim transaction As OracleTransaction = Nothing

    Try
        Using connection As New OracleConnection(_connectionString)
            connection.Open()
            transaction = connection.BeginTransaction()

            ' Operation 1: Insert message into SENT box
            Const insertSentQuery As String = "
                INSERT INTO MESSAGES (MESSAGE_ID, SENDER_ID, RECIPIENT_ID, CONTENT, CREATED_AT)
                VALUES (MESSAGES_SEQ.NEXTVAL, :senderId, :recipientId, :content, SYSDATE)"

            Using command As New OracleCommand(insertSentQuery, connection, transaction)
                command.Parameters.Add(":senderId", OracleDbType.Decimal).Value = fromUserId
                command.Parameters.Add(":recipientId", OracleDbType.Decimal).Value = toUserId
                command.Parameters.Add(":content", OracleDbType.Varchar2).Value = messageContent
                command.ExecuteNonQuery()
            End Using

            ' Operation 2: Update user statistics
            Const updateStatsQuery As String = "
                UPDATE USER_STATS
                SET MESSAGES_SENT = MESSAGES_SENT + 1
                WHERE USER_ID = :userId"

            Using command As New OracleCommand(updateStatsQuery, connection, transaction)
                command.Parameters.Add(":userId", OracleDbType.Decimal).Value = fromUserId
                command.ExecuteNonQuery()
            End Using

            ' If both operations succeed, commit
            transaction.Commit()
            _logger.Log("Message transfer completed successfully", LogLevel.Info)
            Return True

        End Using
    Catch ex As OracleException
        ' Rollback entire transaction on any error
        If transaction IsNot Nothing Then
            transaction.Rollback()
            _logger.LogError("Transaction rolled back due to database error", ex)
        End If
        Throw New DataAccessException("Failed to transfer message", ex)
    Catch ex As Exception
        If transaction IsNot Nothing Then
            transaction.Rollback()
            _logger.LogError("Transaction rolled back due to unexpected error", ex)
        End If
        Throw New DataAccessException("Unexpected error during message transfer", ex)
    End Try
End Function
```

### 2.2 Nested Transactions (WARNING - Oracle Behavior)

```vb
' ⚠ WARNING - Oracle savepoints provide nesting behavior
Public Function ComplexForumOperation() As Boolean
    Dim transaction As OracleTransaction = Nothing
    
    Try
        Using connection As New OracleConnection(_connectionString)
            connection.Open()
            transaction = connection.BeginTransaction()

            ' Create savepoint for partial rollback capability
            Dim savepointName As String = "SP_BeforePostCreate"
            
            Using command As New OracleCommand("SAVEPOINT " & savepointName, connection)
                command.ExecuteNonQuery()
            End Using

            ' Attempt forum operation
            Dim postId As Integer = CreateForumPost(connection, transaction)

            ' If specific operation fails, we can rollback to savepoint
            ' and continue with other operations
            
            transaction.Commit()
            Return True
        End Using
    Catch ex As Exception
        If transaction IsNot Nothing Then transaction.Rollback()
        Throw
    End Try
End Function
```

### 2.3 Isolation Levels (CONFIGURATION)

```vb
' Configure appropriate isolation level for your use case
Public Class DataAccessConfig
    ''' <summary>
    ''' Sets up proper Oracle session configuration
    ''' </summary>
    Public Shared Function ConfigureOracleConnection(connection As OracleConnection) As OracleConnection
        ' Oracle default isolation level: READ COMMITTED
        ' For stricter consistency: use serializable isolation
        
        Using command As New OracleCommand("ALTER SESSION SET ISOLATION_LEVEL = SERIALIZABLE", connection)
            connection.Open()
            command.ExecuteNonQuery()
        End Using

        Return connection
    End Function

    ' Read consistency levels:
    ' READ UNCOMMITTED - Allows dirty reads (not supported in Oracle)
    ' READ COMMITTED - Default Oracle level (no dirty reads)
    ' REPEATABLE READ - Consistent view within transaction
    ' SERIALIZABLE - Highest isolation, lowest concurrency
End Class
```

---

## SECTION 3: Connection Pooling

### 3.1 Connection Pool Configuration (GOOD PRACTICE)

```vb
' ✓ GOOD - Connection pooling with proper configuration
Public Class OracleConnectionPool
    Private Shared _connectionString As String

    Public Shared Sub Initialize(connectionString As String)
        ' Build connection string with pooling parameters
        Dim builder As New OracleConnectionStringBuilder(connectionString) With {
            .Pooling = True,
            .Min_Pool_Size = 10,
            .Max_Pool_Size = 50,
            .Connection_Lifetime = 300,
            .Enlist = False
        }

        _connectionString = builder.ConnectionString
    End Sub

    ''' <summary>
    ''' Gets a connection from the pool
    ''' </summary>
    Public Shared Function GetConnection() As OracleConnection
        Dim connection As New OracleConnection(_connectionString)
        connection.Open()
        Return connection
    End Function

    ''' <summary>
    ''' Properly disposes connection back to pool
    ''' </summary>
    Public Shared Sub ReleaseConnection(connection As OracleConnection)
        If connection IsNot Nothing Then
            If connection.State <> ConnectionState.Closed Then
                connection.Close()
            End If
            connection.Dispose()
        End If
    End Sub
End Class

' Usage example
Public Sub ConfigureApplication()
    Const oracleConnectionString As String = "Data Source=ORCL;User Id=chatapp;Password=SecurePassword123;"
    OracleConnectionPool.Initialize(oracleConnectionString)
End Sub
```

### 3.2 Connection Pool Monitoring

```vb
' ⚠ Monitor pool exhaustion issues
Public Class ConnectionPoolMonitor
    ''' <summary>
    ''' Logs connection pool statistics
    ''' </summary>
    Public Shared Sub LogPoolStatistics()
        Dim poolStats As String = OracleConnection.GetPoolStatistics()
        ' Monitor active connections vs. pool size
        ' If approaching Max_Pool_Size, investigate:
        ' - Long-running queries
        ' - Unclosed connections
        ' - Connection leak
    End Sub

    ''' <summary>
    ''' Clear connection pool (use with caution in production)
    ''' </summary>
    Public Shared Sub ClearPool()
        OracleConnection.ClearPool(New OracleConnection(_connectionString))
    End Sub
End Class
```

---

## SECTION 4: Logging and Audit Trails

### 4.1 Comprehensive Logging (GOOD PRACTICE)

```vb
' ✓ GOOD - Structured logging for audit trail
Public Class AuditLogger
    Private _logger As ILogger

    Public Sub New(logger As ILogger)
        _logger = logger
    End Sub

    ''' <summary>
    ''' Logs all data modifications for compliance
    ''' </summary>
    Public Sub LogDataModification(operation As String, tableName As String, 
                                   recordId As Integer, userId As Integer, 
                                   details As Object)
        Dim auditEntry As New With {
            .Operation = operation,
            .TableName = tableName,
            .RecordId = recordId,
            .ModifiedBy = userId,
            .ModifiedAt = DateTime.UtcNow,
            .Details = details
        }

        _logger.LogInformation("Data Modification: {@AuditEntry}", auditEntry)
    End Sub

    ''' <summary>
    ''' Creates audit trail in database
    ''' </summary>
    Public Sub CreateAuditRecord(operation As String, userId As Integer, 
                                 affectedTable As String, affectedRecordId As Integer, 
                                 oldValues As String, newValues As String)
        Const sqlQuery As String = "
            INSERT INTO AUDIT_LOG 
            (AUDIT_ID, OPERATION, USER_ID, TABLE_NAME, RECORD_ID, OLD_VALUES, NEW_VALUES, CREATED_AT)
            VALUES 
            (AUDIT_LOG_SEQ.NEXTVAL, :operation, :userId, :table_name, :record_id, 
             :old_values, :new_values, SYSDATE)"

        Using connection As New OracleConnection(_connectionString)
            Using command As New OracleCommand(sqlQuery, connection)
                command.Parameters.Add(":operation", OracleDbType.Varchar2).Value = operation
                command.Parameters.Add(":userId", OracleDbType.Decimal).Value = userId
                command.Parameters.Add(":table_name", OracleDbType.Varchar2).Value = affectedTable
                command.Parameters.Add(":record_id", OracleDbType.Decimal).Value = affectedRecordId
                command.Parameters.Add(":old_values", OracleDbType.Varchar2).Value = oldValues
                command.Parameters.Add(":new_values", OracleDbType.Varchar2).Value = newValues

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class

' Audit table structure
' CREATE TABLE AUDIT_LOG (
'     AUDIT_ID NUMBER PRIMARY KEY,
'     OPERATION VARCHAR2(50),
'     USER_ID NUMBER,
'     TABLE_NAME VARCHAR2(100),
'     RECORD_ID NUMBER,
'     OLD_VALUES CLOB,
'     NEW_VALUES CLOB,
'     CREATED_AT DATE
' );
```

### 4.2 Performance Logging

```vb
' Monitor slow queries for optimization
Public Class PerformanceMonitor
    Private _logger As ILogger

    ''' <summary>
    ''' Logs query execution time for performance analysis
    ''' </summary>
    Public Sub LogQueryPerformance(query As String, executionTimeMs As Long)
        If executionTimeMs > 1000 Then
            _logger.LogWarning("Slow query detected: {@Query}. Execution time: {ExecutionTime}ms", 
                             query, executionTimeMs)
        Else
            _logger.LogDebug("Query executed in {ExecutionTime}ms", executionTimeMs)
        End If
    End Sub

    ''' <summary>
    ''' Measures and logs database operation timing
    ''' </summary>
    Public Function MeasureOperation(Of T)(operation As Func(Of T), operationName As String) As T
        Dim stopwatch As New System.Diagnostics.Stopwatch()
        stopwatch.Start()

        Try
            Return operation()
        Finally
            stopwatch.Stop()
            LogQueryPerformance(operationName, stopwatch.ElapsedMilliseconds)
        End Try
    End Function
End Class
```

---

## SECTION 5: Error Handling Strategies

### 5.1 Comprehensive Error Handling (GOOD PRACTICE)

```vb
' ✓ GOOD - Layered error handling with recovery
Public Class RobustDataAccess
    Private _connectionString As String
    Private _logger As ILogger
    Private _maxRetries As Integer = 3

    ''' <summary>
    ''' Executes query with automatic retry logic
    ''' </summary>
    Public Function ExecuteWithRetry(Of T)(operation As Func(Of T), 
                                           operationName As String) As T
        Dim retryCount As Integer = 0
        Dim lastException As Exception

        While retryCount < _maxRetries
            Try
                _logger.LogInformation("Executing {OperationName}. Attempt {Attempt} of {MaxRetries}", 
                                     operationName, retryCount + 1, _maxRetries)
                Return operation()
            Catch ex As OracleException When ex.Number = 1031 ' Insufficient privileges
                _logger.LogError("Authorization error: {Message}", ex.Message)
                Throw New UnauthorizedAccessException("Insufficient database privileges", ex)
            Catch ex As OracleException When ex.Number = 2291 ' Referential integrity
                _logger.LogError("Data integrity error: {Message}", ex.Message)
                Throw New InvalidOperationException("Referenced data does not exist", ex)
            Catch ex As OracleException When ex.Number = 1 ' Unique constraint violation
                _logger.LogError("Duplicate record: {Message}", ex.Message)
                Throw New InvalidOperationException("Duplicate record detected", ex)
            Catch ex As OracleException When CanRetry(ex.Number)
                lastException = ex
                retryCount += 1
                If retryCount < _maxRetries Then
                    Dim delayMs As Integer = Math.Min(1000 * CInt(Math.Pow(2, retryCount - 1)), 10000)
                    _logger.LogWarning("Transient error {ErrorCode}. Retrying after {DelayMs}ms", 
                                     ex.Number, delayMs)
                    System.Threading.Thread.Sleep(delayMs)
                End If
            Catch ex As TimeoutException
                lastException = ex
                retryCount += 1
                _logger.LogWarning("Query timeout. Attempt {Attempt}", retryCount)
                If retryCount >= _maxRetries Then
                    Throw New DataAccessException("Operation timed out after multiple retries", ex)
                End If
            Catch ex As Exception
                _logger.LogError("Unexpected error during {OperationName}: {Message}", 
                               operationName, ex.Message)
                Throw New DataAccessException($"Error during {operationName}", ex)
            End Try
        End While

        Throw New DataAccessException($"{operationName} failed after {_maxRetries} retries", lastException)
    End Function

    Private Function CanRetry(errorCode As Integer) As Boolean
        ' Codes indicating transient errors that should be retried
        Select Case errorCode
            Case 1012 ' Not connected
            Case 1014 ' Process killed
            Case 1041 ' Internal error
            Case 3113 ' End-of-file on communication channel
            Case 3114 ' Not connected to Oracle
            Case 12514 ' TNS listener could not resolve
            Case 12170 ' Connect timeout occurred
                Return True
            Case Else
                Return False
        End Select
    End Function
End Class
```

### 5.2 Graceful Degradation Pattern

```vb
' Allow partial failures without complete system failure
Public Class MessageServiceWithFallback
    Private _primaryDataAccess As MessageDataAccess
    Private _cacheService As CacheService
    Private _logger As ILogger

    ''' <summary>
    ''' Gets user inbox with cache fallback
    ''' </summary>
    Public Function GetInboxWithFallback(userId As Integer) As List(Of PrivateMessage)
        Try
            ' Try primary data source
            Return _primaryDataAccess.GetInbox(userId)
        Catch ex As DataAccessException
            _logger.LogWarning("Database error, attempting cache fallback: {Message}", ex.Message)
            
            Try
                ' Fall back to cached data
                Dim cachedMessages = _cacheService.GetFromCache(Of List(Of PrivateMessage))($"inbox_{userId}")
                If cachedMessages IsNot Nothing Then
                    _logger.LogInformation("Served from cache due to database unavailability")
                    Return cachedMessages
                End If
            Catch cacheEx As Exception
                _logger.LogError("Cache fallback failed: {Message}", cacheEx.Message)
            End Try

            ' If both fail, return empty list instead of throwing
            _logger.LogError("Both database and cache unavailable. Returning empty inbox.")
            Return New List(Of PrivateMessage)()
        End Try
    End Function
End Class
```

---

## SECTION 6: Performance Optimization

### 6.1 Query Optimization (BEST PRACTICES)

```sql
-- ✓ GOOD - Efficient query with proper indexing
CREATE INDEX IDX_MESSAGES_RECIPIENT ON MESSAGES(RECIPIENT_ID, CREATED_AT DESC);
CREATE INDEX IDX_MESSAGES_SENDER ON MESSAGES(SENDER_ID, CREATED_AT DESC);

-- Use explain plan to analyze
EXPLAIN PLAN FOR
SELECT m.MESSAGE_ID, m.SUBJECT, m.CONTENT
FROM MESSAGES m
WHERE m.RECIPIENT_ID = 123
ORDER BY m.CREATED_AT DESC;

-- ✗ BAD - N+1 query problem
-- Retrieving all topics, then one query per topic for post count
SELECT * FROM FORUMS_TOPICS;
-- Then for each topic:
SELECT COUNT(*) FROM FORUMS_POSTS WHERE TOPIC_ID = topic_id;

-- ✓ GOOD - Single query with aggregation
SELECT ft.*, COUNT(fp.POST_ID) as POST_COUNT
FROM FORUMS_TOPICS ft
LEFT JOIN FORUMS_POSTS fp ON ft.TOPIC_ID = fp.TOPIC_ID
GROUP BY ft.TOPIC_ID, ft.USER_ID, ft.TITLE, ft.DESCRIPTION, ft.CREATED_AT;
```

### 6.2 Caching Strategy (APPLICATION LEVEL)

```vb
' ✓ GOOD - Implement intelligent caching
Public Class MessageCachingService
    Private _cache As IMemoryCache
    Private _logger As ILogger
    Private Const CACHE_DURATION_MINUTES As Integer = 15

    ''' <summary>
    ''' Gets inbox with caching layer
    ''' </summary>
    Public Function GetInboxCached(userId As Integer) As List(Of PrivateMessage)
        Dim cacheKey As String = $"inbox_{userId}"

        If _cache.TryGetValue(cacheKey, value) Then
            _logger.LogInformation("Cache hit for inbox_{UserId}", userId)
            Return CType(value, List(Of PrivateMessage))
        End If

        ' Cache miss - fetch from database
        Dim messages = FetchInboxFromDatabase(userId)

        ' Store in cache with expiration
        Dim cacheOptions As New MemoryCacheEntryOptions() With {
            .AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CACHE_DURATION_MINUTES),
            .SlidingExpiration = TimeSpan.FromMinutes(5)
        }

        _cache.Set(cacheKey, messages, cacheOptions)
        _logger.LogInformation("Cached inbox_{UserId} for {Minutes} minutes", userId, CACHE_DURATION_MINUTES)

        Return messages
    End Function

    ''' <summary>
    ''' Invalidates cache when message is sent
    ''' </summary>
    Public Sub InvalidateInboxCache(recipientUserId As Integer)
        _cache.Remove($"inbox_{recipientUserId}")
        _logger.LogInformation("Invalidated cache for inbox_{UserId}", recipientUserId)
    End Sub
End Class
```

### 6.3 Pagination Implementation (SCALABILITY)

```vb
' ✓ GOOD - Proper pagination prevents memory overflow
Public Function GetLargeResultSetPaginated(pageNumber As Integer, 
                                          pageSize As Integer) As PagedResult(Of ForumTopic)
    Const pageSize_Max As Integer = 100
    
    ' Enforce maximum page size
    Dim actualPageSize As Integer = Math.Min(pageSize, pageSize_Max)
    
    ' Calculate offsets
    Dim offset As Integer = (pageNumber - 1) * actualPageSize
    
    Const sqlCountQuery As String = "SELECT COUNT(*) FROM FORUMS_TOPICS"
    Const sqlDataQuery As String = "
        SELECT * FROM (
            SELECT 
                TOPIC_ID, USER_ID, TITLE, CREATED_AT,
                ROW_NUMBER() OVER (ORDER BY CREATED_AT DESC) as rn
            FROM FORUMS_TOPICS
        )
        WHERE rn BETWEEN :start AND :end"
    
    Dim totalRecords As Integer
    Dim topics As New List(Of ForumTopic)
    
    Using connection As New OracleConnection(_connectionString)
        connection.Open()
        
        ' Get total count
        Using countCmd As New OracleCommand(sqlCountQuery, connection)
            totalRecords = CInt(countCmd.ExecuteScalar())
        End Using
        
        ' Get paged data
        Using dataCmd As New OracleCommand(sqlDataQuery, connection)
            dataCmd.Parameters.Add(":start", OracleDbType.Decimal).Value = offset + 1
            dataCmd.Parameters.Add(":end", OracleDbType.Decimal).Value = offset + actualPageSize
            
            Using reader As OracleDataReader = dataCmd.ExecuteReader()
                While reader.Read()
                    topics.Add(New ForumTopic With {
                        .TopicId = CInt(reader("TOPIC_ID")),
                        .Title = reader("TITLE").ToString()
                    })
                End While
            End Using
        End Using
    End Using
    
    Return New PagedResult(Of ForumTopic) With {
        .Items = topics,
        .TotalRecords = totalRecords,
        .PageNumber = pageNumber,
        .PageSize = actualPageSize,
        .TotalPages = Math.Ceiling(totalRecords / actualPageSize)
    }
End Function

Public Class PagedResult(Of T)
    Public Property Items As List(Of T)
    Public Property TotalRecords As Integer
    Public Property PageNumber As Integer
    Public Property PageSize As Integer
    Public Property TotalPages As Double
End Class
```

---

## Summary Checklist

- [ ] All queries use parameterized statements
- [ ] Input validation applied at multiple layers
- [ ] HTML encoding for XSS prevention
- [ ] Transactions used for multi-step operations
- [ ] Connection pooling configured
- [ ] Audit logging implemented
- [ ] Error handling with retry logic
- [ ] Slow query monitoring in place
- [ ] Pagination for large result sets
- [ ] Caching strategy evaluated

---

**Document Version:** 1.0  
**Last Updated:** Week 3 Deliverables  
**Compliance:** OWASP Top 10, ACID Principles, Oracle Best Practices
