-- ¦¯¯¯¯¯¦
-- ¦ Vue ¦
-- ¦_____¦
--C'est une vue pour afficher le nom et le prénom d'un acteur, le nom du personnage qu'il a joué, le nom de l'émission que ce dernier apparaît et le côte de l'émission.
GO
Create view Personne.vw_ActeurPersonnageEmission
AS
	SELECT TOP(50) A.ActeurID, A.Nom, A.Prenom, P.PersonnageID, P.Nom as [NomPersonnage], E.EmissionTelevisionID, E.Nom as [NomEmission], E.Cote
	FROM Personne.Acteur A
	Inner Join Personne.Personnage P On P.ActeurID = A.ActeurID
	Inner Join Television.EmissionTelevision E ON E.EmissionTelevisionID = P.EmissionTelevisionID
	Order BY E.Cote

GO
