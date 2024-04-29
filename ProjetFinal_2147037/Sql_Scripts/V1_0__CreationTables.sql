

-- █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█
-- █ Création des tables + contraintes de clé primaire █
-- █▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█
CREATE TABLE Personne.Acteur(
-- À COMPLÉTER
	ActeurID int IDENTITY (1,1),
	Nom nvarchar(15) NOT NULL,
	Prenom nvarchar(15) NOT NULL,
	Age int NOT NULL,
	CONSTRAINT PK_Acteur_ActeurID PRIMARY KEY (ActeurID)
);
CREATE TABLE Television.Plateforme(
-- À COMPLÉTER
	PlateformeID int IDENTITY (1,1),
	Nom nvarchar(35) NOT NULL,
	Descriptions text NOT NULL,
	NbEmissionCoreenne int NOT NULL,
	CONSTRAINT PK_Plateforme_PlateformeID PRIMARY KEY (PlateformeID)
);

CREATE TABLE Television.EmissionTelevision(
-- À COMPLÉTER
	EmissionTelevisionID int IDENTITY (1,1),
	Nom nvarchar(50) NOT NULL,
	Descriptions text NOT NULL,
	EstCoreen bit NOT NULL,
	Cote int NOT NULL,
	NbVisionnement int NOT NULL,
	CoutProduction money NOT NULL,
	PlateformeID int NOT NULL,
	CONSTRAINT PK_EmissionTelevision_EmissionTelevisionID PRIMARY KEY (EmissionTelevisionID)
);
CREATE TABLE Television.Serie(
-- À COMPLÉTER
	SerieID int IDENTITY (1,1),
	NbEpisode int NOT NULL,
	EmissionTelevisionID int NOT NULL,
	CONSTRAINT PK_Serie_SerieID PRIMARY KEY (SerieID)
);
CREATE TABLE Television.Film(
-- À COMPLÉTER
	FilmID int IDENTITY (1,1),
	DureeMinute int NOT NULL,
	EmissionTelevisionID int NOT NULL,
	CONSTRAINT PK_Film_FilmID PRIMARY KEY (FilmID)
);
CREATE TABLE Personne.Utilisateur(
-- À COMPLÉTER
	UtilisateurID int IDENTITY (1,1),
	Pseudo nvarchar(25) NOT NULL,
	NoTelephone CHAR(10) NOT NULL,
	MotDePasse nvarchar(40),
	MotDePasseHache varbinary(max),
	PlateformeID int NOT NULL,
	CONSTRAINT PK_Utilisateur_UtilisateurID PRIMARY KEY (UtilisateurID)
);
CREATE TABLE Personne.Courriel(
-- À COMPLÉTER
	CourrielID int IDENTITY (1,1),
	Nom nvarchar(100) NOT NULL,
	UtilisateurID int NOT NULL,
	CONSTRAINT PK_Courriel_CourrielID PRIMARY KEY (CourrielID)
);

CREATE TABLE Personne.Adresse(
-- À COMPLÉTER
	AdresseID int IDENTITY (1,1),
	NoPorte int NOT NULL,
	Rue nvarchar(55) NOT NULL,
	NoAppartement int,
	Ville nvarchar(50) NOT NULL,
	CodePostal nvarchar(7) NOT NULL,
	Province nvarchar(25) NOT NULL,
	Pays nvarchar(20) NOT NULL,
	UtilisateurID int NOT NULL,
	CONSTRAINT PK_Adresse_AdresseID PRIMARY KEY (AdresseID)
);


CREATE TABLE Personne.Personnage(
-- À COMPLÉTER
	PersonnageID int IDENTITY (1,1),
	Nom nvarchar(15) NOT NULL,
	ActeurID int NOT NULL,
	EstVivant bit NOT NULL DEFAULT 1,
	DateMort datetime DEFAULT NULL,
	EmissionTelevisionID int NOT NULL,
	CONSTRAINT PK_Personnage_PersonnageID PRIMARY KEY (PersonnageID)
);





GO
-- █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█
-- █ Création des contraintes de clé étrangère █
-- █▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█
ALTER TABLE Personne.Personnage ADD CONSTRAINT FK_Personnage_ActeurID FOREIGN KEY (ActeurID) 
REFERENCES Personne.Acteur(ActeurID)
GO
ALTER TABLE Personne.Personnage ADD CONSTRAINT FK_Personnage_EmissionTelevisionID FOREIGN KEY (EmissionTelevisionID) 
REFERENCES Television.EmissionTelevision(EmissionTelevisionID)
GO
ALTER TABLE Personne.Courriel ADD CONSTRAINT FK_Courriel_UtilisateurID FOREIGN KEY (UtilisateurID) 
REFERENCES Personne.Utilisateur(UtilisateurID)
GO
ALTER TABLE Personne.Adresse ADD CONSTRAINT FK_Adresse_UtilisateurID FOREIGN KEY (UtilisateurID) 
REFERENCES Personne.Utilisateur(UtilisateurID)
GO
ALTER TABLE Television.EmissionTelevision ADD CONSTRAINT FK_EmissionTelevision_PlateformeID FOREIGN KEY (PlateformeID) 
REFERENCES Television.Plateforme(PlateformeID)
Go
ALTER TABLE Personne.Utilisateur ADD CONSTRAINT FK_Utilisateur_PlateformeID FOREIGN KEY (PlateformeID) 
REFERENCES Television.Plateforme(PlateformeID)
GO
ALTER TABLE Television.Serie ADD CONSTRAINT FK_Serie_EmissionTelevisionID FOREIGN KEY (EmissionTelevisionID) 
REFERENCES Television.EmissionTelevision(EmissionTelevisionID)
GO
ALTER TABLE Television.Film ADD CONSTRAINT FK_Film_EmissionTelevisionID FOREIGN KEY (EmissionTelevisionID) 
REFERENCES Television.EmissionTelevision(EmissionTelevisionID)
-- █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█
-- █      Création des autres contraintes      █
-- █▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█



-- Pseudo Utilisateur UNIQUE
ALTER TABLE Personne.Utilisateur ADD CONSTRAINT UC_Utilisateur_Pseudo UNIQUE (Pseudo)
GO

-- NoTelephone Utilisateur UNIQUE
ALTER TABLE Personne.Utilisateur ADD CONSTRAINT UC_Utilisateur_NoTelephone UNIQUE (NoTelephone)
GO

--Le courriel doit respecter la forme standard d'un courriel
ALTER TABLE Personne.Courriel ADD CONSTRAINT CK_Courriel_Nom CHECK (Nom LIKE '_%@_%._%')
GO

--Le numéro de téléphone doit être composée de 10 chiffres
ALTER TABLE Personne.Utilisateur ADD CONSTRAINT CK_Utilisateur_NoTelephone CHECK (NoTelephone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
GO

--Le code postal respecte le format standard
ALTER TABLE Personne.Adresse ADD CONSTRAINT CK_Adresse_CodePostal CHECK (CodePostal LIKE '___ ___')
GO

