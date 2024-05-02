ALTER TABLE Personne.Utilisateur ADD
	Identifiant uniqueidentifier NOT NULL ROWGUIDCOL DEFAULT newid();
GO

ALTER TABLE Personne.Utilisateur ADD CONSTRAINT UC_Utilisateur_Identifiant
UNIQUE (Identifiant);
GO

ALTER TABLE Personne.Utilisateur ADD
Photo varbinary(max) FILESTREAM NULL;
GO