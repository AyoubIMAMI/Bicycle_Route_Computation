﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoutingServer.ProxyCachCall {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProxyCachCall.IProxy")]
    public interface IProxy {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxy/GetStationsFromContract", ReplyAction="http://tempuri.org/IProxy/GetStationsFromContractResponse")]
        string GetStationsFromContract(string contract);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxy/GetStationsFromContract", ReplyAction="http://tempuri.org/IProxy/GetStationsFromContractResponse")]
        System.Threading.Tasks.Task<string> GetStationsFromContractAsync(string contract);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxy/GetContracts", ReplyAction="http://tempuri.org/IProxy/GetContractsResponse")]
        string GetContracts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProxy/GetContracts", ReplyAction="http://tempuri.org/IProxy/GetContractsResponse")]
        System.Threading.Tasks.Task<string> GetContractsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProxyChannel : RoutingServer.ProxyCachCall.IProxy, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProxyClient : System.ServiceModel.ClientBase<RoutingServer.ProxyCachCall.IProxy>, RoutingServer.ProxyCachCall.IProxy {
        
        public ProxyClient() {
        }
        
        public ProxyClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProxyClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProxyClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProxyClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetStationsFromContract(string contract) {
            return base.Channel.GetStationsFromContract(contract);
        }
        
        public System.Threading.Tasks.Task<string> GetStationsFromContractAsync(string contract) {
            return base.Channel.GetStationsFromContractAsync(contract);
        }
        
        public string GetContracts() {
            return base.Channel.GetContracts();
        }
        
        public System.Threading.Tasks.Task<string> GetContractsAsync() {
            return base.Channel.GetContractsAsync();
        }
    }
}
