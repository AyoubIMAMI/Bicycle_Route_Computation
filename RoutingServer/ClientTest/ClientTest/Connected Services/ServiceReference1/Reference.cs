﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientTest.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/RoutingServer")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IItinerary")]
    public interface IItinerary {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItinerary/GetItinerary", ReplyAction="http://tempuri.org/IItinerary/GetItineraryResponse")]
        string GetItinerary(string destinationAddress, string originAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItinerary/GetItinerary", ReplyAction="http://tempuri.org/IItinerary/GetItineraryResponse")]
        System.Threading.Tasks.Task<string> GetItineraryAsync(string destinationAddress, string originAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItinerary/TestJCD", ReplyAction="http://tempuri.org/IItinerary/TestJCDResponse")]
        string TestJCD();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItinerary/TestJCD", ReplyAction="http://tempuri.org/IItinerary/TestJCDResponse")]
        System.Threading.Tasks.Task<string> TestJCDAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItinerary/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IItinerary/GetDataUsingDataContractResponse")]
        ClientTest.ServiceReference1.CompositeType GetDataUsingDataContract(ClientTest.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IItinerary/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IItinerary/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<ClientTest.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(ClientTest.ServiceReference1.CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IItineraryChannel : ClientTest.ServiceReference1.IItinerary, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ItineraryClient : System.ServiceModel.ClientBase<ClientTest.ServiceReference1.IItinerary>, ClientTest.ServiceReference1.IItinerary {
        
        public ItineraryClient() {
        }
        
        public ItineraryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ItineraryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ItineraryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ItineraryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetItinerary(string destinationAddress, string originAddress) {
            return base.Channel.GetItinerary(destinationAddress, originAddress);
        }
        
        public System.Threading.Tasks.Task<string> GetItineraryAsync(string destinationAddress, string originAddress) {
            return base.Channel.GetItineraryAsync(destinationAddress, originAddress);
        }
        
        public string TestJCD() {
            return base.Channel.TestJCD();
        }
        
        public System.Threading.Tasks.Task<string> TestJCDAsync() {
            return base.Channel.TestJCDAsync();
        }
        
        public ClientTest.ServiceReference1.CompositeType GetDataUsingDataContract(ClientTest.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<ClientTest.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(ClientTest.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
    }
}