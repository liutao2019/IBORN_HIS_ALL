//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.4984
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FSSOC.HISFC.PE.Components.Service
{
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebSvsTester", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="tjwebforhisSoap", Namespace="http://tempuri.org/")]
    public partial class tjwebforhis : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetItemChargeStatusAllOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetItemChargeStatusPartOperationCompleted;
        
        /// <remarks/>
        public tjwebforhis() {
            this.Url = "http://192.168.18.65:8080/tjwebForhis.asmx";
        }
        
        /// <remarks/>
        public event GetItemChargeStatusAllCompletedEventHandler GetItemChargeStatusAllCompleted;
        
        /// <remarks/>
        public event GetItemChargeStatusPartCompletedEventHandler GetItemChargeStatusPartCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetItemChargeStatusAll", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetItemChargeStatusAll(string as_jzh, string as_patient_id, string as_charge_status) {
            object[] results = this.Invoke("GetItemChargeStatusAll", new object[] {
                        as_jzh,
                        as_patient_id,
                        as_charge_status});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetItemChargeStatusAllAsync(string as_jzh, string as_patient_id, string as_charge_status) {
            this.GetItemChargeStatusAllAsync(as_jzh, as_patient_id, as_charge_status, null);
        }
        
        /// <remarks/>
        public void GetItemChargeStatusAllAsync(string as_jzh, string as_patient_id, string as_charge_status, object userState) {
            if ((this.GetItemChargeStatusAllOperationCompleted == null)) {
                this.GetItemChargeStatusAllOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetItemChargeStatusAllOperationCompleted);
            }
            this.InvokeAsync("GetItemChargeStatusAll", new object[] {
                        as_jzh,
                        as_patient_id,
                        as_charge_status}, this.GetItemChargeStatusAllOperationCompleted, userState);
        }
        
        private void OnGetItemChargeStatusAllOperationCompleted(object arg) {
            if ((this.GetItemChargeStatusAllCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetItemChargeStatusAllCompleted(this, new GetItemChargeStatusAllCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetItemChargeStatusPart", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetItemChargeStatusPart(string as_jzh, string as_patient_id, string as_charge_code, string as_charge_status) {
            object[] results = this.Invoke("GetItemChargeStatusPart", new object[] {
                        as_jzh,
                        as_patient_id,
                        as_charge_code,
                        as_charge_status});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetItemChargeStatusPartAsync(string as_jzh, string as_patient_id, string as_charge_code, string as_charge_status) {
            this.GetItemChargeStatusPartAsync(as_jzh, as_patient_id, as_charge_code, as_charge_status, null);
        }
        
        /// <remarks/>
        public void GetItemChargeStatusPartAsync(string as_jzh, string as_patient_id, string as_charge_code, string as_charge_status, object userState) {
            if ((this.GetItemChargeStatusPartOperationCompleted == null)) {
                this.GetItemChargeStatusPartOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetItemChargeStatusPartOperationCompleted);
            }
            this.InvokeAsync("GetItemChargeStatusPart", new object[] {
                        as_jzh,
                        as_patient_id,
                        as_charge_code,
                        as_charge_status}, this.GetItemChargeStatusPartOperationCompleted, userState);
        }
        
        private void OnGetItemChargeStatusPartOperationCompleted(object arg) {
            if ((this.GetItemChargeStatusPartCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetItemChargeStatusPartCompleted(this, new GetItemChargeStatusPartCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebSvsTester", "1.0.0.0")]
    public delegate void GetItemChargeStatusAllCompletedEventHandler(object sender, GetItemChargeStatusAllCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebSvsTester", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetItemChargeStatusAllCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetItemChargeStatusAllCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebSvsTester", "1.0.0.0")]
    public delegate void GetItemChargeStatusPartCompletedEventHandler(object sender, GetItemChargeStatusPartCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("WebSvsTester", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetItemChargeStatusPartCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetItemChargeStatusPartCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}