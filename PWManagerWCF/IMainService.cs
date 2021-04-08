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
        List<service_credentials> GetAllServiceCredentials();

        [OperationContract]
        bool LoginExists(string login);

        [OperationContract]
        bool IsCredentialsCorrect(string login, string password);

        [OperationContract]
        bool CreateUser(string login, string password);
    }
}
