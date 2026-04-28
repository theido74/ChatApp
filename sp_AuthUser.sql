-- ============================================
-- Procédure 1: sp_AuthenticateUser
-- Valide l'utilisateur par username et password
-- ============================================

CREATE OR REPLACE PROCEDURE sp_AuthUser(
	p_per_nom in VARCHAR(2),
	p_per_motDePasse in VARCHAR(2),
	p_per_id OUT NUMBER, --OUT REPRESENTE LES VARIABLES RETOURNER PAR LA PROCEDURE
	p_result OUT VARCHAR(2)
)
IS --DECLARE LE DEBUT DE LA PROCEDURE, LES VARIABLES CI DESSOUS SONT LOCALE
	v_motDePasseStocke VARCHAR(2);
	v_per_id NUMBER;

BEGIN

	p_result = ERROR;
	p_per_id = NULL;

--CHERCHER PERSONNE PAR Username
	SELECT per_id, per_motDePasse
	INTO v_per_id, v_motDePasseStocke
	FROM ESS_PERSONNE
	WHERE per_nom = p_per_nom AND per_isActive = 1;

--VERIFIER LE MOT DE PASSE
IF v_motDePasseStocke != p_per_motDePasse THEN
	--CREATION D'UN LOG TENTATIVE ECHOUEE
	INSERT INTO ESS_LOGS(log_per_id,log_action,log_timestamp,log_details)
	VALUES(NULL, 'LOGIN_FAILED', SYSDATE, 'username: ' || p_per_nom);
	COMMIT;
	p_per_nom := 'INVALID PASSWORD';
	RETURN; --RETOURNE LES VARIABLES OUT
END IF;
--CREATION LOGS SUCCES
INSERT INTO ESS_LOGS(log_per_id,log_action,log_timestamp,log_details)
VALUES(V_per_id,'LOGIN_SUCCES',SYSDATE,'username: ' || p_per_nom);
p_per_id := v_per_id;
p_result := 'SUCCES';
COMMIT;

EXCEPTION --GESTION DES EXCEPTIONS
	WHEN NO_DATE_FOUND THEN
		p_result := 'USER_NOT_FOUND'
	WHEN OTHERS THEN
		p_result := 'ERROR: ' || SQLERRM;  --ERROR: ORA-00942: ...
        ROLLBACK;
END sp_AuthenticateUser;
