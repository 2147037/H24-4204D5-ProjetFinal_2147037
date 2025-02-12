USE master 
GO

-- CREATION ou RECREATION de la BD
IF
EXISTS(SELECT * FROM sys.databases WHERE name='ProjetFinal_2147037')
BEGIN
	DROP DATABASE ProjetFinal_2147037
END

CREATE DATABASE ProjetFinal_2147037
GO

-- Configuration de FILESTREAM
EXEC sp_configure filestream_access_level, 2 RECONFIGURE

ALTER DATABASE ProjetFinal_2147037
ADD FILEGROUP FG_Images2147037 CONTAINS FILESTREAM;
GO

ALTER DATABASE ProjetFinal_2147037
ADD FILE (
	NAME = FG_Images2147037,
	FILENAME = 'C:\EspaceLabo\FG_Images2147037'
)
TO FILEGROUP FG_Images2147037
GO

-- Configuration des cl�s sym�triques
USE ProjetFinal_2147037
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'CeciEst1GrosMotDePasse!'
GO
CREATE CERTIFICATE MonCertificat WITH SUBJECT = 'ChiffrementDonneeProjetFinal'
GO
CREATE SYMMETRIC KEY MaSuperCle WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE MonCertificat
GO