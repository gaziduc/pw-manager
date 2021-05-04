/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[categories] ON
INSERT INTO [dbo].[categories] ([id], [name]) VALUES (1, N'Social Network')
INSERT INTO [dbo].[categories] ([id], [name]) VALUES (2, N'Bank')
INSERT INTO [dbo].[categories] ([id], [name]) VALUES (3, N'Messaging Service')
INSERT INTO [dbo].[categories] ([id], [name]) VALUES (4, N'E-shop')
INSERT INTO [dbo].[categories] ([id], [name]) VALUES (5, N'Entertainment')
INSERT INTO [dbo].[categories] ([id], [name]) VALUES (6, N'Other')
SET IDENTITY_INSERT [dbo].[categories] OFF
