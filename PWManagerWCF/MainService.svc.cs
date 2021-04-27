using System;
using System.Collections.Generic;
using System.Linq;
using Elskom.Generic.Libs;
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


        public (bool, List<service_credentials>) GetAllServiceCredentials(long user_id)
        {
            var services = ents.ServiceCredentials.Where(x => x.user_id == user_id).ToList();

            var user = ents.Users.FirstOrDefault(users => users.id == user_id);

            if (user == null)
                return (false, null);

            string masterPW = user.password;
            BlowFish blowfish = new BlowFish(masterPW);

            foreach (var service in services)
            {
                service.password = blowfish.DecryptECB(service.password);
            }

            return (true, services);
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

        public (bool, string) CreateService(string name, string url, string login, string password, long user_id, string category)
        {
            try
            {
                var user = ents.Users.FirstOrDefault(users => users.id == user_id);
                string masterPW = user.password;
                BlowFish blowfish = new BlowFish(masterPW);

                ents.ServiceCredentials.Add(new service_credentials
                {
                    name = name,
                    url = url,
                    login = login,
                    password = blowfish.EncryptECB(password),
                    user_id = user_id,
                    category_id = GetIdFromCategoryString(category),
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

        public string GetCategoryFromId(short id)
        {
            try
            {
                var category = ents.Categories.FirstOrDefault(c => c.id == id);
                if (category == null)
                    return "";

                return category.name;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public short GetIdFromCategoryString(string category)
        {
            var category_row = ents.Categories.FirstOrDefault(x => x.name.Equals(category));
            short category_id = 6;
            if (category_row == null)
            {
                var tmp = ents.Categories.FirstOrDefault(x => x.name.Equals("Other"));
                if (tmp != null)
                {
                    category_id = tmp.id;
                }
            }
            else
            {
                category_id = category_row.id;
            }

            return category_id;
        }

        public bool UpdateService(long id, string name, string url, string login, string password, string category, long user_id)
        {
            try
            {
                var serv = ents.ServiceCredentials.FirstOrDefault(e => e.id == id);

                if (serv == null)
                    return false;

                var user = ents.Users.FirstOrDefault(users => users.id == user_id);
                string masterPW = user.password;
                BlowFish blowfish = new BlowFish(masterPW);

                serv.name = name;
                serv.url = url;
                serv.login = login;
                serv.password = blowfish.EncryptECB(password);
                serv.category_id = GetIdFromCategoryString(category);

                ents.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteService(long id)
        {
            try
            {
                var serv = ents.ServiceCredentials.FirstOrDefault(e => e.id == id);

                if (serv == null)
                {
                    return false;
                }

                ents.ServiceCredentials.Remove(serv);
                ents.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
