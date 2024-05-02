-- ¦¯¯¯¯¯¯¯¯¯¯¯¦
-- ¦ Fonctions ¦
-- ¦___________¦
-- C'est une fonction qui retourne l'Id de la plateforme qu'utilise un utilisateur.
GO
Create Function Personne.ufn_UtilisateurPlateforme
(@UtilisateurID int)
Returns int
AS
BEGIN
	Declare @PlateformeID int;
	SELECT @PlateformeID = P.PlateformeID
	FROM Television.Plateforme P
	InneR JOIN Personne.Utilisateur U ON U.PlateformeID = P.PlateformeID
	Where U.UtilisateurID = @UtilisateurID

	RETURN @PlateformeID
END
GO

--C'est une procédure qui affiche la liste d'émissions que peut regarder un utilisateur via la plateforme qu'il est inscrit. 
--La procédure reçoit l'id de l'utilisateur pour qu'on puisse aller chercher l'id de la plateforme à l'aide de la fonction créée avant.
--E.Nom, E.EstCoreen, E.NbVisionnement
Create procedure Television.uspListeEmission
(@UtilisateurID int)
AS
Begin
	Select E.EmissionTelevisionID, E.Nom, E.Descriptions, E.EstCoreen, E.Cote, E.NbVisionnement, E.CoutProduction, E.PlateformeID
	From Television.EmissionTelevision E
	Inner Join Television.Plateforme P ON P.PlateformeID = E.PlateformeID
	Inner Join Personne.Utilisateur U ON U.PlateformeID =P.PlateformeID
	Where P.PlateformeID = Personne.ufn_UtilisateurPlateforme(@UtilisateurID)


END