# pw-manager

A password manager program in C# with .NET

## Installation et lancement des scripts de base de données

Ouvrez la solution PWManager.sln et allez dans l'explorateur de solutions,
puis faites clic-droit sur le projet "Database". Cliquez ensuite sur "Publier".

Dans "Connexion de la base de données cible" cliquer sur "Modifier".
Allez dans l'onglet "Parcourir". Si possible, mettez comme nom de serveur: ".\MTI"
Laissez "Authentification Windows" et votre nom d'utilisateur.
Dans "nom de la base de données" tapez: PWManager
Puis cliquez sur OK.

Explication: Cela créé les schémas de tables de base de données de SQL Server.
De plus, le contenu du script Script.PostDeployment1.sql est executé.
Ce dernier ajoute le contenu de la table "categories" dans la base de données.

## Déploiement du service .svc (projet WCF) sous IIS

### Installation de IIS

Dans un premier temps il faut installer IIS sur sa machine en suivant la
documentation pour le système d'exploitation adapté.

Pour Windows 10 :
- Ouvrez le Panneau de configuration, puis cliquez sur Programmes et
  fonctionnalités > Activer ou désactiver des fonctionnalités Windows.
- Activez Internet Information Services ainsi que tous les composants
  IIS requis disponible à la fin de ce tutoriel:
  https://enterprise.arcgis.com/fr/web-adaptor/latest/install/iis/enable-iis-10-components-server.htm
- Cliquez sur OK.


### Copie du projet sous IIS

Copiez le dossier du projet WCF "PWManagerWCF" dans le dossier suivant :
C:/inetpub/wwwroot.
Si le dossier inetpub/wwwroot n'existe pas allez sur IIS et vérifiez que
le "Default Web Site" existe. Si ce n'est pas le cas allez dans la section
DEPANNAGE "inetpub/wwwroot does not exist". Retournez sur IIS et
déployez le dropdown de "sites" puis celui de "Default Website". Faites
ensuite un clic droit sur le projet "PWManagerWCF" qui vient d'être copié
puis cliquez sur "Convert to application". Laisser les paramètres tels quels
et cliquez sur OK. Cliquez ensuite sur 'Content View' en bas. Si vous
remarquez que vos fichier .svc ne sont pas reconnu vérifiez bien que
toutes les dépendances IIS et WCF sont bien cochés dans le menu
Programmes et fonctionnalités > Activer ou désactiver des fonctionnalités
Windows. Si c'est déjà le cas et que les ".svc" ne sont toujours pas
reconnus allez dans la section DEPANNAGE ".svc not recognized".


### Lancer le service sous IIS

Une fois que vous avez cliqué que 'Content View' vous devriez voir le 
fichier 'MainService.svc' qui correspond au service permettant de
communiquer avec la BDD. Cliquez dessus puis cliquez sur 'Browse' sur le
menu de droite. Une page web devrait s'ouvrir dans votre navigateur,
copiez le lien de l'url dans le presse papier.


### Liaison du service WCF avec l'interface WPF

Maintenant que nous avons déployé notre service WCF sur IIS nous allons
le lier avec le projet WPF. Pour se faire faites un clic droit sur le 
projet WPF puis cliquez sur "Add a service reference". Coller l'adresse
que vous avez auparavant copier dans le presse papier dans le textbox
"adress" puis cliquez sur "Go". Les services et les opérations devraient
apparaitre. Renommez le namespace en "MainService" puis cliquez sur "OK".
Cliquez sur Suivant puis sélectionnez "System.Collection.Generic.List"
pour le type de collection. Enfin cliquez sur "Terminer".

## Dépannage déploiement WCF (problèmes rencontrés)

### Troubleshooting "inetpub/wwwroot does not exist"

- open IIS Manager
- right click Sites node under your machine in the Connections tree on the left side and click Add Website
- enter "Default Web Site" as a Site name
- set Application pool back to DefaultAppPool!
- set Physical path to %SystemDrive%\inetpub\wwwroot
- leave Binding and everything else as is

### Troubleshooting ".svc not recognized"

Seulement si vous vous êtes assuré de bien avoir intégrer les bonnes dépendances
(.NET Framework 4.5 > WCF Services > HTTP Activation) utilisez la méthode suivante :
- Ajouter l'extension MIME suivante: .svc, MIME type: application/octet-stream
- Ajouter le Request path suivant: *.svc, Type: System.ServiceModel.Activation.HttpHandler, Name: svc-Integrated

### Troubleshooting "changement non pris en compte"

Essayez de regénérer le projet WCF.

### Troubleshooting "Échec de l'ouverture de session de l'utilisateur 'IIS APP POOL\DefaultApp Pool"

Si vous rencontrez une erreur de connection à la base de données SQL Server
essayez la méthode suivante:
- In SSMS, under the server, expand Security, then right click Logins and select "New Login...".
- In the New Login dialog, enter the app pool as the login name ("IIS APP POOL\DefaultApp Pool") and click "OK".
- You can then right click the login for the app pool, select Properties and select "User Mapping".
- Check the appropriate database, and the appropriate roles: db_datareader and db_datawriter.

## Déploiement projet WPF

Ouvrez la solution PWManager.sln, puis faites clic-droit sur le projet PWManager.
Cliquez sur "Publish" dans le menu contextuel. Cela générera l'executable que vous
pourrez ensuite lancer (par example en double-cliquant sur le .exe généré dans
l'explorateur de fichiers, dans bin/Release/netcoreapp3.1/publish/ ou
bin/Debug/netcoreapp3.1/publish/)

## Projet tests unitaires

Pour lancer les tests unitaires vous devez avoir au préalable déployer le projet WCF.
Les "connected services" doivent être connectés au WCF déployé, pour cela allez voir
la section 4 du déploiement WCF sous IIS.
Faites ensuite un clique droit sur le projet "PWManagerTests" et cliquez sur "Excecute
tests".

-- 
David Ghiassi, Rémy Dao, Xavier Facqueur, Alexandre Vermeulen
