
	-- Table d'utilisateurs
	
	CREATE TABLE Utilisateurs.Utilisateur(
		UtilisateurID int IDENTITY(1,1),
		Pseudo nvarchar(50) NOT NULL,
		MotDePasseHache varbinary(32) NOT NULL,
		Sel varbinary(16) NOT NULL,
		CouleurPrefere varbinary(max) NOT NULL,
		CONSTRAINT PK_Utilisateur_UtilisateurID PRIMARY KEY (UtilisateurID)
	);
	GO
	
	-- Contraintes
	
	ALTER TABLE Utilisateurs.Utilisateur ADD CONSTRAINT
	UC_Utilisateur_Pseudo UNIQUE (Pseudo);
	GO
	
	-- Créer une clé maîtresse avec un mot de passe
	-- ?
	CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'P4ssw0rd!1234567890';
	GO
	
	-- Créer un certificat auto-signé
	-- ?
	CREATE CERTIFICATE MonCertificat WITH SUBJECT = 'ChiffrementCouleur';
	GO
	
	-- Créer une clé symétrique
	-- ?
	CREATE SYMMETRIC KEY MaSuperCle WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE MonCertificat;
	GO
	
	-- Procédure inscription
	CREATE PROCEDURE Utilisateurs.USP_CreerUtilisateur
		@Pseudo nvarchar(50),
		@MotDePasse nvarchar(100),
		@Couleur nvarchar(30)
	AS
	BEGIN
	
		DECLARE @Sel varbinary(16) = CRYPT_GEN_RANDOM(16);
		-- CORRECTION HASH PASS
		DECLARE @PassSel nvarchar(116) = CONCAT(@MotDePasse, @Sel);
		DECLARE @PassHash varbinary(32) = HASHBYTES('SHA2_256', @PassSel);
		-- FIN CORRECTION HASH PASS

		-- ENCRYPTION COULEUR
		OPEN SYMMETRIC KEY MaSuperCle DECRYPTION BY CERTIFICATE MonCertificat;
		DECLARE @CouleurEncrypt varbinary(max) = ENCRYPTBYKEY(KEY_GUID('MaSuperCle'), @Couleur);
		CLOSE SYMMETRIC KEY MaSuperCle;
		-- FIN ENCRYPTION COULEUR

		
		INSERT INTO Utilisateurs.Utilisateur (Pseudo, MotDePasseHache, Sel, CouleurPrefere)
		VALUES
		(@Pseudo, @PassHash, @Sel, @CouleurEncrypt);
	
	END
	GO
	
	-- Procédure authentification
	
	CREATE PROCEDURE Utilisateurs.USP_AuthUtilisateur
		@Pseudo nvarchar(50),
		@MotDePasse nvarchar(100)
	AS
	BEGIN
		-- HASH PASS
		DECLARE @Sel varbinary(16);
		DECLARE @PassHash varbinary(32);
		-- HASH PASS

		SELECT @Sel = Sel, @PassHash = MotDePasseHache
		FROM Utilisateurs.Utilisateur
		WHERE Pseudo = @Pseudo;
		
		IF HASHBYTES('SHA2_256', CONCAT(@MotDePasse, @Sel)) = @PassHash
		BEGIN
			SELECT  * FROM Utilisateurs.Utilisateur WHERE @Pseudo = Pseudo; -- 1 AS 'PassValid'; -- TRUE (Valide)
		END
		ELSE
		BEGIN
			SELECT  TOP(0) * FROM Utilisateurs.Utilisateur -- 0 AS 'PassValid'; -- FALSE (Invalide)
		END
	
	END
	GO
	
	-- Insertions de quelques utilisateurs (si jamais inscription ne marche pas, testez au moins la connexion / déconnexion)
	
	EXEC Utilisateurs.USP_CreerUtilisateur @Pseudo = 'max', @MotDePasse = 'Salut1!', @Couleur = 'indigo';
	GO
	
	EXEC Utilisateurs.USP_CreerUtilisateur @Pseudo = 'chantal', @MotDePasse = 'Allo2!', @Couleur = 'bourgogne';
	GO
	
	EXEC Utilisateurs.USP_CreerUtilisateur @Pseudo = 'kamalPro', @MotDePasse = 'Bonjour3!', @Couleur = 'cramoisi';
	GO
	
	
	
	
	
	