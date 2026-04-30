create or replace procedure sp_GetUserByName( p_per_nom IN VARCHAR2,
    p_per_id OUT NUMBER,
    p_per_email OUT VARCHAR2,
    p_per_motDePasse OUT VARCHAR2,
    p_isActive OUT NUMBER,
    p_result OUT VARCHAR2) IS
BEGIN
	p_result := 'ERROR';

  SELECT per_id,per_email,per_mdpHashed CASE WHEN per_isActive = 1 THEN 1 ELSE 0 END 
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
/
