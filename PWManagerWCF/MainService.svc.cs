using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using PWManagerWCF.Models;

namespace PWManagerWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "MainService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez MainService.svc ou MainService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class MainService : IMainService
    {
        private EntityModel ents;

        public MainService()
        {
            this.ents = new EntityModel();
        }

        public MainService(EntityModel ents)
        {
            this.ents = ents;
        }

 
        public List<service_credentials> GetAllServiceCredentials()
        {
            return ents.ServiceCredentials.ToList();
        }

        public bool LoginExists(string login)
        {
            return ents.Users.FirstOrDefault(user => user.login.Equals(login)) != null;
        }

        public bool IsCredentialsCorrect(string login, string password)
        {
            return ents.Users.FirstOrDefault(user => user.login.Equals(login) && user.password.Equals(password)) != null;
        }

        public bool CreateUser(string login, string password)
        {
            try
            {
                string hash = Cryptography.GetHashString(password);
                ents.Users.Add(new users
                {
                    login = login,
                    password = hash
                });
                ents.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }

}
