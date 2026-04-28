-- ============================================
-- Procédure 1: sp_AuthenticateUser
-- Valide l'utilisateur par username et password
-- ============================================

CREATE OR REPLACE PROCEDURE sp_AuthUser(
	p_per_nom in VARCHAR2 --ARGUMENTS DONNES A LA PROCEDURE
	p_per_motDePasse in VARCHAR2,
	p_per_id OUT NUMBER, --OUT REPRESENTE LES VARIABLES RETOURNER PAR LA PROCEDURE
	p_result OUT VARCHAR2
)
IS --DECLARE LE DEBUT DE LA PROCEDURE, LES VARIABLES CI DESSOUS SONT LOCALE
	v_motDePasseStocke VARCHAR2;
	v_per_id NUMBER;

BEGIN

	p_result := ERROR;
	p_per_id := NULL;

--CHERCHER PERSONNE PAR Username
	SELECT per_id, per_mdpHashed
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
END sp_AuthUser;


-- ============================================
-- Procédure 2: sp_GetUserByUsername
-- Récupère les infos utilisateur
-- ============================================
CREATE OR REPLACE PROCEDURE spGetUserByname(
	p_per_nom IN VARCHAR2,
	p_per_id IN NUMBER,
	p_per_email in VARCHAR2,
	p_per_motDePasse OUT VARCHAR2,
	p_isActive OUT NUMBER,
	p_result_OUT VARCHAR2, 
)
IS 
BEGIN
	p_result := 'ERROR';

	SELECT per_id,per_email,per_mdpHashed, CASE WHEN per_isActive = 1 THEN 1 ELSE 0 END 
	INTO p_per_id,p_per_email,p_per_motDePasse, p_isActive
	FROM ESS_PERSONNE
	WHERE per_nom = p_per_nom;

	p_result := 'SUCCES';

EXCEPTION
	WHEN NO_DATA_FOUND THEN
        	p_result := 'USER_NOT_FOUND';
	WHEN OTHERS THEN
        	p_result := 'ERROR: ' || SQLERRM;

END sp_GetUserByname;
