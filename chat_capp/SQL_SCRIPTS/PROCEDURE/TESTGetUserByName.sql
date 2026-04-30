SET SERVEROUTPUT ON;

DECLARE
    v_per_id NUMBER;
    v_email VARCHAR2(100);
    v_mdp VARCHAR2(255);
    v_isActive NUMBER;
    v_result VARCHAR2(100);
BEGIN
    sp_GetUserByName(
        p_per_nom => 'Ryser',
        p_per_id => v_per_id,
        p_per_email => v_email,
        p_per_motDePasse => v_mdp,
        p_isActive => v_isActive,
        p_result => v_result
    );

    DBMS_OUTPUT.PUT_LINE('ID: ' || v_per_id);
    DBMS_OUTPUT.PUT_LINE('EMAIL: ' || v_email);
    DBMS_OUTPUT.PUT_LINE('ACTIVE: ' || v_isActive);
    DBMS_OUTPUT.PUT_LINE('RESULT: ' || v_result);
END;
/
