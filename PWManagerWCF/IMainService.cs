using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PWManagerWCF.Models;

namespace PWManagerWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IMainService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IMainService
    {
        [OperationContract]
        List<service_credentials> GetAllServiceCredentials(long user_id);

        [OperationContract]
        bool LoginExists(string login);

        [OperationContract]
        long GetUserFromCrdentials(string login, string password);

        [OperationContract]
        List<string> GetAllUsersLogin();

        [OperationContract]
        (long, string) CreateUser(string login, string password);

        [OperationContract]
        (bool, string) CreateService(string name, string url, string login, string password, long user_id, string category);

        [OperationContract]
        (bool, string) ChangeFavoriteStatus(long service_id, bool is_favorite);

        [OperationContract]
        string GetCategoryFromId(short id);

        [OperationContract]
        bool UpdateService(long id, string name, string url, string login, string password, string category);

        [OperationContract]
        bool DeleteService(long id);

    }
}
