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

 
        public List<service_credentials> GetAllServiceCredentials(long user_id)
        {
            return ents.ServiceCredentials.Where(x => x.user_id == user_id).ToList();
        }

        public bool LoginExists(string login)
        {
            return ents.Users.FirstOrDefault(user => user.login.Equals(login)) != null;
        }

        public List<string> GetAllUsersLogin()
        {
            var query = from users in ents.Users select users.login;
            return query.ToList();
        }

        public long GetUserFromCrdentials(string login, string password)
        {
            var user = ents.Users.FirstOrDefault(u => u.login.Equals(login) && u.password.Equals(password));
            if (user == null)
                return -1;

            return user.id;
        }

        public (long, string) CreateUser(string login, string password)
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

                return (ents.Users.FirstOrDefault(x => x.login == login && x.password == hash).id, null);
            }
            catch (Exception e)
            {
                return (-1, e.Message);
            }
        }

        public (bool, string) CreateService(string name, string url, string login, string password, long user_id, short category_id)
        {
            try
            {
                ents.ServiceCredentials.Add(new service_credentials
                {
                    name = name,
                    url = url,
                    login = login,
                    password = password,
                    user_id = user_id,
                    category_id = category_id,
                    is_favorite = false 
                });
                ents.SaveChanges();
                return (true, null);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public (bool, string) ChangeFavoriteStatus(long service_id, bool is_favorite)
        {
            try
            {
                var service = ents.ServiceCredentials.FirstOrDefault(serv => serv.id == service_id).is_favorite = !is_favorite;
                ents.SaveChanges();
                return (true, null);
            } catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }

}
