BEGIN
    DBMS_SCHEDULER.CREATE_JOB (
        job_name        => 'JOB_PURGE_ESS_LOGS',
        job_type        => 'STORED_PROCEDURE',
        job_action      => 'PURGE_ESS_LOGS',
        start_date      => SYSTIMESTAMP,
        repeat_interval => 'FREQ=MINUTELY; INTERVAL=1',
        enabled         => TRUE
    );
END;
/
