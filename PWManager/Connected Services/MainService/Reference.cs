﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//
//     Les changements apportés à ce fichier peuvent provoquer un comportement incorrect et seront perdus si
//     le code est regénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MainService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MainService.IMainService")]
    public interface IMainService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/GetAllServiceCredentials", ReplyAction="http://tempuri.org/IMainService/GetAllServiceCredentialsResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<bool, System.Collections.Generic.List<PWManagerWCF.Models.service_credentials>>> GetAllServiceCredentialsAsync(long user_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/LoginExists", ReplyAction="http://tempuri.org/IMainService/LoginExistsResponse")]
        System.Threading.Tasks.Task<bool> LoginExistsAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/GetUserFromCrdentials", ReplyAction="http://tempuri.org/IMainService/GetUserFromCrdentialsResponse")]
        System.Threading.Tasks.Task<long> GetUserFromCrdentialsAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/GetAllUsersLogin", ReplyAction="http://tempuri.org/IMainService/GetAllUsersLoginResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetAllUsersLoginAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/CreateUser", ReplyAction="http://tempuri.org/IMainService/CreateUserResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<long, string>> CreateUserAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/CreateService", ReplyAction="http://tempuri.org/IMainService/CreateServiceResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<bool, string>> CreateServiceAsync(string name, string url, string login, string password, long user_id, string category);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/ChangeFavoriteStatus", ReplyAction="http://tempuri.org/IMainService/ChangeFavoriteStatusResponse")]
        System.Threading.Tasks.Task<System.ValueTuple<bool, string>> ChangeFavoriteStatusAsync(long service_id, bool is_favorite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/GetCategoryFromId", ReplyAction="http://tempuri.org/IMainService/GetCategoryFromIdResponse")]
        System.Threading.Tasks.Task<string> GetCategoryFromIdAsync(short id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/UpdateService", ReplyAction="http://tempuri.org/IMainService/UpdateServiceResponse")]
        System.Threading.Tasks.Task<bool> UpdateServiceAsync(long id, string name, string url, string login, string password, string category, long user_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/DeleteService", ReplyAction="http://tempuri.org/IMainService/DeleteServiceResponse")]
        System.Threading.Tasks.Task<bool> DeleteServiceAsync(long id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IMainServiceChannel : MainService.IMainService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class MainServiceClient : System.ServiceModel.ClientBase<MainService.IMainService>, MainService.IMainService
    {
        
        /// <summary>
        /// Implémentez cette méthode partielle pour configurer le point de terminaison de service.
        /// </summary>
        /// <param name="serviceEndpoint">Point de terminaison à configurer</param>
        /// <param name="clientCredentials">Informations d'identification du client</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public MainServiceClient() : 
                base(MainServiceClient.GetDefaultBinding(), MainServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IMainService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MainServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(MainServiceClient.GetBindingForEndpoint(endpointConfiguration), MainServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MainServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(MainServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MainServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(MainServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MainServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<bool, System.Collections.Generic.List<PWManagerWCF.Models.service_credentials>>> GetAllServiceCredentialsAsync(long user_id)
        {
            return base.Channel.GetAllServiceCredentialsAsync(user_id);
        }
        
        public System.Threading.Tasks.Task<bool> LoginExistsAsync(string login)
        {
            return base.Channel.LoginExistsAsync(login);
        }
        
        public System.Threading.Tasks.Task<long> GetUserFromCrdentialsAsync(string login, string password)
        {
            return base.Channel.GetUserFromCrdentialsAsync(login, password);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetAllUsersLoginAsync()
        {
            return base.Channel.GetAllUsersLoginAsync();
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<long, string>> CreateUserAsync(string login, string password)
        {
            return base.Channel.CreateUserAsync(login, password);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<bool, string>> CreateServiceAsync(string name, string url, string login, string password, long user_id, string category)
        {
            return base.Channel.CreateServiceAsync(name, url, login, password, user_id, category);
        }
        
        public System.Threading.Tasks.Task<System.ValueTuple<bool, string>> ChangeFavoriteStatusAsync(long service_id, bool is_favorite)
        {
            return base.Channel.ChangeFavoriteStatusAsync(service_id, is_favorite);
        }
        
        public System.Threading.Tasks.Task<string> GetCategoryFromIdAsync(short id)
        {
            return base.Channel.GetCategoryFromIdAsync(id);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateServiceAsync(long id, string name, string url, string login, string password, string category, long user_id)
        {
            return base.Channel.UpdateServiceAsync(id, name, url, login, password, category, user_id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteServiceAsync(long id)
        {
            return base.Channel.DeleteServiceAsync(id);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMainService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Le point de terminaison nommé \'{0}\' est introuvable.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMainService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:57161/MainService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Le point de terminaison nommé \'{0}\' est introuvable.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return MainServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IMainService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return MainServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IMainService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IMainService,
        }
    }
}
