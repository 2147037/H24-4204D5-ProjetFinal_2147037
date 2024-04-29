CREATE TABLE Personne.MotDePasse(
		MotDePasse nvarchar(40)
	);

GO
Create PROCEDURE Personne.USP_CreationUtilisateur_Chiffrement
	@Pseudo nvarchar(25),
	@NoTelephone CHAR(10),
	@PlateformID int,
	@MotDePasse nvarchar(40)
AS
BEGIN


	OPEN SYMMETRIC KEY MaSuperCle
	DECRYPTION BY CERTIFICATE MonCertificat;

	Declare @MdPHash varbinary(max) = EncryptByKey(KEY_GUID('MaSuperCle'),@MotDePasse);

	Close Symmetric KEY MaSuperCle;

	INSERT INTO Personne.Utilisateur(Pseudo, NoTelephone,MotDePasseHache, PlateformeID)
	VALUES(@Pseudo, @NoTelephone, @MdPHash,  @PlateformID)


END


GO
Create PROCEDURE Personne.USP_ModificationUtilisateur_Chiffrement
AS
BEGIN

	OPEN SYMMETRIC KEY MaSuperCle
	DECRYPTION BY CERTIFICATE MonCertificat	
	UPDATE Personne.Utilisateur
	Set MotDePasseHache = EncryptByKey(KEY_GUID('MaSuperCle'),MotDePasse);

	Close Symmetric KEY MaSuperCle

	

END	
GO

Execute Personne.USP_ModificationUtilisateur_Chiffrement

GO

Alter table Personne.Utilisateur
Drop Column MotDePasse


GO

Create PROCEDURE Personne.USP_Utilisateur_Dechiffrement
	@UtilisateurID int
AS
BEGIN
	
	Declare @MdpHache varbinary(32);

	Select @MdpHache = MotDePasseHache 
	From Personne.Utilisateur
	Where UtilisateurID = @UtilisateurID


	OPEN SYMMETRIC KEY MaSuperCle
	DECRYPTION BY CERTIFICATE MonCertificat	
	
	Select Convert(nvarchar(40), DecryptByKey(MotDePasseHache)) AS MotDePasse
	From Personne.Utilisateur
	where UtilisateurID = @UtilisateurID

	Close Symmetric KEY MaSuperCle

	

END	
GO
