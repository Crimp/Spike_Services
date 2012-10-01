﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FormsAuthenticationConsoleClient.AuthenticationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AuthenticationService.IAuthenticationService")]
    public interface IAuthenticationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/ValidateUser", ReplyAction="http://tempuri.org/IAuthenticationService/ValidateUserResponse")]
        string ValidateUser(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/ValidateUser", ReplyAction="http://tempuri.org/IAuthenticationService/ValidateUserResponse")]
        System.Threading.Tasks.Task<string> ValidateUserAsync(string userName, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthenticationServiceChannel : FormsAuthenticationConsoleClient.AuthenticationService.IAuthenticationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticationServiceClient : System.ServiceModel.ClientBase<FormsAuthenticationConsoleClient.AuthenticationService.IAuthenticationService>, FormsAuthenticationConsoleClient.AuthenticationService.IAuthenticationService {
        
        public AuthenticationServiceClient() {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ValidateUser(string userName, string password) {
            return base.Channel.ValidateUser(userName, password);
        }
        
        public System.Threading.Tasks.Task<string> ValidateUserAsync(string userName, string password) {
            return base.Channel.ValidateUserAsync(userName, password);
        }
    }
}
