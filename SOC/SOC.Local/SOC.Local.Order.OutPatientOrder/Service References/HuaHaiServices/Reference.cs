﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FS.SOC.Local.Order.OutPatientOrder.HuaHaiServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HuaHaiServices.IConnectHISService")]
    public interface IConnectHISService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IConnectHISService/InputParameter", ReplyAction="http://tempuri.org/IConnectHISService/InputParameterResponse")]
        int InputParameter(string paramType, string parValue);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IConnectHISServiceChannel : FS.SOC.Local.Order.OutPatientOrder.HuaHaiServices.IConnectHISService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConnectHISServiceClient : System.ServiceModel.ClientBase<FS.SOC.Local.Order.OutPatientOrder.HuaHaiServices.IConnectHISService>, FS.SOC.Local.Order.OutPatientOrder.HuaHaiServices.IConnectHISService {
        
        public ConnectHISServiceClient() {
        }
        
        public ConnectHISServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConnectHISServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConnectHISServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConnectHISServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int InputParameter(string paramType, string parValue) {
            return base.Channel.InputParameter(paramType, parValue);
        }
    }
}
