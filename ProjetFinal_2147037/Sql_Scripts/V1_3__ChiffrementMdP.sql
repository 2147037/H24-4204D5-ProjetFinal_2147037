

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
	@Pseudo nvarchar(25),
	@MotDePasse nvarchar(40)
AS
BEGIN


	OPEN SYMMETRIC KEY MaSuperCle
	DECRYPTION BY CERTIFICATE MonCertificat;

	Declare @MdPHash varbinary(max) = EncryptByKey(KEY_GUID('MaSuperCle'),@MotDePasse);

	Close Symmetric KEY MaSuperCle;

	UPDATE Personne.Utilisateur
	Set MotDePasseHache = @MdpHash
	Where Pseudo = @Pseudo

END
GO