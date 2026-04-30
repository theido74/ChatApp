
DECLARE
    v_id NUMBER;
    v_result VARCHAR2(100);
BEGIN
    sp_AuthUser('Ryser', 'hash1', v_id, v_result);

    DBMS_OUTPUT.PUT_LINE('ID: ' || v_id);
    DBMS_OUTPUT.PUT_LINE('RESULT: ' || v_result);
END;
/
