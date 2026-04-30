create or replace procedure sp_AuthUser(p_per_nom in VARCHAR2, --ARGUMENTS DONNES A LA PROCEDURE
	p_per_motDePasse in VARCHAR2,
	p_per_id OUT NUMBER, --OUT REPRESENTE LES VARIABLES RETOURNER PAR LA PROCEDURE
	p_result OUT VARCHAR2) is
v_motDePasseStocke VARCHAR2(100);
  v_per_id NUMBER;

BEGIN
  --p_result := 'ERROR';
  --p_per_id = IS NULL;

--CHERCHER PERSONNE PAR Username
  SELECT per_id, per_mdpHashed
  INTO v_per_id, v_motDePasseStocke
  FROM ESS_PERSONNE
  WHERE per_nom = p_per_nom AND per_isActive = 1;

--VERIFIER LE MOT DE PASSE
IF v_motDePasseStocke != p_per_motDePasse THEN
  DBMS_OUTPUT.put_line('LOGS ECHEC');

  --CREATION D'UN LOG TENTATIVE ECHOUEE
  INSERT INTO ESS_LOGS(log_per_id,log_action,log_timestamp,log_details)
  VALUES(NULL, 'LOGIN_FAILED', SYSDATE, 'username: ' || p_per_nom);
  COMMIT;
  p_result := 'INVALID PASSWORD';
  RETURN; --RETOURNE LES VARIABLES OUT
END IF;
--CREATION LOGS SUCCES
  DBMS_OUTPUT.put_line('LOGS OK');

INSERT INTO ESS_LOGS(log_id,log_per_id,log_action,log_timestamp,log_details)
VALUES(seq_logs.nextval,v_per_id,'LOGIN_SUCCES',SYSDATE,'username: ' || p_per_nom);
p_per_id := v_per_id;
p_result := 'SUCCES';
COMMIT;

EXCEPTION --GESTION DES EXCEPTIONS
  WHEN NO_DATA_FOUND THEN
    p_result := 'USER_NOT_FOUND';
  WHEN OTHERS THEN
    p_result := 'ERROR: ' || SQLERRM;  --ERROR: ORA-00942: ...
        ROLLBACK;
END sp_AuthUser;
/
