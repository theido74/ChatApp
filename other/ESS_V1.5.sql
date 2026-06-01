------------------------------------------------
--Script : ESS.sql
--Objet  : Insert des data au tables
--Authors: Ayman El Hadaoui, Arnaud Poncet, Damien Ryser
--Version    Visa       Date       Commentaires
--------- --------- ------------  -----------------
-- 1.1    FR        27/04/26       Adapté par Damien
-------------------------------------------------

-- SUPPRESSION DES TABLES
DROP TABLE ESS_LOGS;

DROP TABLE ESS_USERACT;

DROP TABLE ESS_SESSION;

DROP TABLE ESS_MESSAGE;

DROP TABLE ESS_FORUM;

DROP TABLE ESS_ELEVE;

DROP TABLE ESS_PERSONNE;

-- CREATION DES TABLES
CREATE TABLE ESS_PERSONNE (
    PER_ID NUMBER(5) PRIMARY KEY,
    PER_USERNAME VARCHAR2(50) NOT NULL UNIQUE,
    PER_NOM VARCHAR2(50) NOT NULL,
    PER_PRENOM VARCHAR2(50) NOT NULL,
    PER_DATENAISSANCE DATE,
    PER_EMAIL VARCHAR2(100) NOT NULL UNIQUE,
    PER_MDPHASHED VARCHAR2(255) NOT NULL,
    PER_DATECREATION DATE DEFAULT SYSDATE,
    PER_ISACTIVE NUMBER(1) DEFAULT 1, -- 1=actif, 0=inactif
    PER_CHATSTATUT VARCHAR2(50)
);

CREATE TABLE ESS_ELEVE (
    ELE_PER_ID NUMBER(5) PRIMARY KEY,
    ELE_NIVEAU NUMBER(3),
    ELE_NBPOINTS NUMBER(5),
    ELE_CLASSE VARCHAR2(50),
    CONSTRAINT FK_PER_ELE FOREIGN KEY (ELE_PER_ID) REFERENCES ESS_PERSONNE (PER_ID)
);

CREATE TABLE ESS_FORUM (
    FOR_ID NUMBER(5) PRIMARY KEY,
    FOR_NOM VARCHAR2(50) NOT NULL,
    FOR_DESCRIPTION VARCHAR2(100),
    FOR_DATECREATION DATE DEFAULT SYSDATE,
    FOR_ESTACTIF NUMBER(1) DEFAULT 1 -- 1=actif, 0=inactif
);

CREATE TABLE ESS_MESSAGE (
    MES_ID NUMBER(5) PRIMARY KEY,
    MES_PER_ID_EMM NUMBER(5),
    MES_PER_ID_REC NUMBER(5),
    MES_FOR_ID NUMBER(5),
    MES_CONTENU VARCHAR2(1000) NOT NULL,
    MES_TIMESTAMP DATE DEFAULT SYSDATE,
    MES_ESTLU NUMBER(1) DEFAULT 0, -- 1=lu, 0=non lu
    MES_ESTPRIVE NUMBER(1) DEFAULT 0, -- 1=actif, 0=inactif
    MES_ESTSUPPRIME NUMBER(1) DEFAULT 0, -- 1=supprimé, 0=non supprimé
    CONSTRAINT FK_EMMETEUR FOREIGN KEY (MES_PER_ID_EMM) REFERENCES ESS_PERSONNE (PER_ID),
    CONSTRAINT FK_RECEPTEUR FOREIGN KEY (MES_PER_ID_REC) REFERENCES ESS_PERSONNE (PER_ID),
    CONSTRAINT FK_FOR_ID FOREIGN KEY (MES_FOR_ID) REFERENCES ESS_FORUM (FOR_ID),
    CONSTRAINT CHK_MESSAGE_TYPE CHECK ((MES_ESTPRIVE = 1 AND MES_FOR_ID IS NULL) OR (MES_ESTPRIVE = 0 AND MES_FOR_ID IS NOT NULL))
);

CREATE TABLE ESS_SESSION (
    SES_ID VARCHAR2(50) PRIMARY KEY,
    SES_PER_ID NUMBER(5) NOT NULL UNIQUE,
    SES_TEMPSDERACTIVITE DATE DEFAULT SYSDATE,
    SES_ESTACTIF NUMBER(1) DEFAULT 1, -- 1=session valide, 0=session expirée
    CONSTRAINT FK_PER_ID FOREIGN KEY (SES_PER_ID) REFERENCES ESS_PERSONNE (PER_ID)
);

CREATE TABLE ESS_USERACT (
    ACT_ID NUMBER(5) PRIMARY KEY,
    ACT_PER_ID NUMBER(5) NOT NULL,
    ACT_LASTPINGTIME DATE DEFAULT SYSDATE, -- Dernière activité
    ACT_STATUS VARCHAR2(20) DEFAULT 'online', -- 'online' ou 'offline'
    CONSTRAINT FK_ACT_PER_ID FOREIGN KEY (ACT_PER_ID) REFERENCES ESS_PERSONNE (PER_ID)
);

CREATE TABLE ESS_LOGS (
    LOG_ID NUMBER(5) PRIMARY KEY,
    LOG_PER_ID NUMBER(5), -- NULL si action système
    LOG_ACTION VARCHAR2(50), -- 'LOGIN', 'SEND_MESSAGE', 'DELETE_MESSAGE', 'ERROR', etc.
    LOG_TIMESTAMP DATE DEFAULT SYSDATE,
    LOG_DETAILS VARCHAR2(1000), -- Détails supplémentaires
    LOG_ERRORMESSAGE VARCHAR2(1000), -- Si c'est une erreur
    CONSTRAINT FK_LOG_PER_ID FOREIGN KEY (LOG_PER_ID) REFERENCES ESS_PERSONNE (PER_ID)
);

COMMIT;

-- DROP DES SÉQUENCES
DROP SEQUENCE SEQ_LOGS;

DROP SEQUENCE SEQ_USERACT;

DROP SEQUENCE SEQ_SESSION;

DROP SEQUENCE SEQ_MESSAGE;

DROP SEQUENCE SEQ_FORUM;

DROP SEQUENCE SEQ_PERSONNE;

-- CRÉATION DES SÉQUENCES
CREATE SEQUENCE SEQ_PERSONNE START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE SEQ_FORUM START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE SEQ_MESSAGE START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE SEQ_SESSION START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE SEQ_USERACT START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE SEQ_LOGS START WITH 1 INCREMENT BY 1;

COMMIT;

-- DELETE DES DONNÉES
DELETE FROM ESS_LOGS;

DELETE FROM ESS_USERACT;

DELETE FROM ESS_SESSION;

DELETE FROM ESS_MESSAGE;

DELETE FROM ESS_FORUM;

DELETE FROM ESS_ELEVE;

DELETE FROM ESS_PERSONNE;

-- INSERT DES DONNÉES
/* =========================
   ESS_PERSONNE
   ========================= */
INSERT INTO ESS_PERSONNE (
    PER_ID,
    PER_USERNAME,
    PER_NOM,
    PER_PRENOM,
    PER_DATENAISSANCE,
    PER_EMAIL,
    PER_MDPHASHED,
    PER_DATECREATION,
    PER_ISACTIVE,
    PER_CHATSTATUT
) VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'jdupont',
    'Dupont',
    'Jean',
    DATE '2005-03-12',
    'jdupont@mail.com',
    'hash001',
    DATE '2026-04-01',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE (
    ELE_PER_ID,
    ELE_NIVEAU,
    ELE_NBPOINTS,
    ELE_CLASSE
) VALUES (
    SEQ_PERSONNE.CURRVAL,
    5,
    1200,
    '5A'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'mmartin',
    'Martin',
    'Marie',
    DATE '2004-07-25',
    'mmartin@mail.com',
    'hash002',
    DATE '2026-04-02',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    6,
    1450,
    '6B'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'pbernard',
    'Bernard',
    'Paul',
    DATE '2006-01-10',
    'pbernard@mail.com',
    'hash003',
    DATE '2026-04-03',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    4,
    980,
    '4C'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'csimon',
    'Simon',
    'Claire',
    DATE '2005-11-08',
    'csimon@mail.com',
    'hash004',
    DATE '2026-04-04',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    5,
    1320,
    '5A'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'tmorel',
    'Morel',
    'Thomas',
    DATE '2004-09-17',
    'tmorel@mail.com',
    'hash005',
    DATE '2026-04-05',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    6,
    1600,
    '6A'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'adurand',
    'Durand',
    'Alice',
    DATE '2005-05-23',
    'adurand@mail.com',
    'hash006',
    DATE '2026-04-06',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    5,
    1180,
    '5B'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'rpetit',
    'Petit',
    'Romain',
    DATE '2006-02-14',
    'rpetit@mail.com',
    'hash007',
    DATE '2026-04-07',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    4,
    890,
    '4A'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'leclerc',
    'Leclerc',
    'Laura',
    DATE '2005-12-01',
    'leclerc@mail.com',
    'hash008',
    DATE '2026-04-08',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    5,
    1275,
    '5C'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'gfaure',
    'Faure',
    'Gabriel',
    DATE '2004-08-30',
    'gfaure@mail.com',
    'hash009',
    DATE '2026-04-09',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    6,
    1700,
    '6C'
);

INSERT INTO ESS_PERSONNE VALUES (
    SEQ_PERSONNE.NEXTVAL,
    'nrobert',
    'Robert',
    'Nina',
    DATE '2005-06-19',
    'nrobert@mail.com',
    'hash010',
    DATE '2026-04-10',
    1,
    'Hors ligne'
);

INSERT INTO ESS_ELEVE VALUES (
    SEQ_PERSONNE.CURRVAL,
    5,
    1100,
    '5D'
);

/* =========================
   ESS_FORUM
   ========================= */
INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Mathématiques',
    'Forum pour les maths',
    DATE '2026-04-01',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Français',
    'Forum pour le français',
    DATE '2026-04-02',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Sciences',
    'Forum pour les sciences',
    DATE '2026-04-03',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Histoire',
    'Forum pour l''histoire',
    DATE '2026-04-04',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Géographie',
    'Forum pour la géographie',
    DATE '2026-04-05',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Anglais',
    'Forum pour l''anglais',
    DATE '2026-04-06',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Informatique',
    'Forum pour l''informatique',
    DATE '2026-04-07',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Physique',
    'Forum pour la physique',
    DATE '2026-04-08',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Chimie',
    'Forum pour la chimie',
    DATE '2026-04-09',
    1
);

INSERT INTO ESS_FORUM VALUES (
    SEQ_FORUM.NEXTVAL,
    'Divers',
    'Forum général',
    DATE '2026-04-10',
    1
);

/* =========================
   ESS_MESSAGE
   ========================= */
INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    1,
    NULL,
    1,
    'Bonjour à tous sur le forum Maths.',
    DATE '2026-04-11',
    1,
    0,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    2,
    NULL,
    2,
    'Quel est le sujet de dissertation ?',
    DATE '2026-04-11',
    0,
    0,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    3,
    NULL,
    3,
    'La cellule est fascinante.',
    DATE '2026-04-12',
    1,
    0,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    4,
    NULL,
    4,
    'Qui était Napoléon ?',
    DATE '2026-04-12',
    0,
    0,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    5,
    NULL,
    7,
    'J''adore programmer en Java.',
    DATE '2026-04-13',
    1,
    0,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    6,
    1,
    NULL,
    'Salut Jean, tu as fini les exercices ?',
    DATE '2026-04-13',
    1,
    1,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    7,
    2,
    NULL,
    'Peux-tu m''aider en français ?',
    DATE '2026-04-14',
    0,
    1,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    8,
    3,
    NULL,
    'Merci pour ton explication.',
    DATE '2026-04-14',
    1,
    1,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    9,
    4,
    NULL,
    'On révise ensemble ce soir ?',
    DATE '2026-04-15',
    0,
    1,
    0
);

INSERT INTO ESS_MESSAGE VALUES (
    SEQ_MESSAGE.NEXTVAL,
    10,
    5,
    NULL,
    'Bonne chance pour le test !',
    DATE '2026-04-15',
    1,
    1,
    0
);

COMMIT;