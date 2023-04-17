﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace FS.SOC.Local.OutpatientFee.fosiweb {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="FosiWebSoap", Namespace="http://tempuri.org/")]
    public partial class FosiWeb : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback NetTestOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetServiceTimeOperationCompleted;
        
        private System.Threading.SendOrPostCallback IDCardCheckOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPatInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback JudgeCanMedicareOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateCardPatInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback OPRegistOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public FosiWeb() {
            this.Url = global::FS.SOC.Local.OutpatientFee.Properties.Settings.Default.SOC_Local_OutpatientFee_fosiweb_FosiWeb;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event NetTestCompletedEventHandler NetTestCompleted;
        
        /// <remarks/>
        public event GetServiceTimeCompletedEventHandler GetServiceTimeCompleted;
        
        /// <remarks/>
        public event IDCardCheckCompletedEventHandler IDCardCheckCompleted;
        
        /// <remarks/>
        public event GetPatInfoCompletedEventHandler GetPatInfoCompleted;
        
        /// <remarks/>
        public event JudgeCanMedicareCompletedEventHandler JudgeCanMedicareCompleted;
        
        /// <remarks/>
        public event CreateCardPatInfoCompletedEventHandler CreateCardPatInfoCompleted;
        
        /// <remarks/>
        public event OPRegistCompletedEventHandler OPRegistCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NetTest", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string NetTest() {
            object[] results = this.Invoke("NetTest", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void NetTestAsync() {
            this.NetTestAsync(null);
        }
        
        /// <remarks/>
        public void NetTestAsync(object userState) {
            if ((this.NetTestOperationCompleted == null)) {
                this.NetTestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnNetTestOperationCompleted);
            }
            this.InvokeAsync("NetTest", new object[0], this.NetTestOperationCompleted, userState);
        }
        
        private void OnNetTestOperationCompleted(object arg) {
            if ((this.NetTestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.NetTestCompleted(this, new NetTestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetServiceTime", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetServiceTime() {
            object[] results = this.Invoke("GetServiceTime", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetServiceTimeAsync() {
            this.GetServiceTimeAsync(null);
        }
        
        /// <remarks/>
        public void GetServiceTimeAsync(object userState) {
            if ((this.GetServiceTimeOperationCompleted == null)) {
                this.GetServiceTimeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetServiceTimeOperationCompleted);
            }
            this.InvokeAsync("GetServiceTime", new object[0], this.GetServiceTimeOperationCompleted, userState);
        }
        
        private void OnGetServiceTimeOperationCompleted(object arg) {
            if ((this.GetServiceTimeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetServiceTimeCompleted(this, new GetServiceTimeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IDCardCheck", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string IDCardCheck(string RequestXml) {
            object[] results = this.Invoke("IDCardCheck", new object[] {
                        RequestXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void IDCardCheckAsync(string RequestXml) {
            this.IDCardCheckAsync(RequestXml, null);
        }
        
        /// <remarks/>
        public void IDCardCheckAsync(string RequestXml, object userState) {
            if ((this.IDCardCheckOperationCompleted == null)) {
                this.IDCardCheckOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIDCardCheckOperationCompleted);
            }
            this.InvokeAsync("IDCardCheck", new object[] {
                        RequestXml}, this.IDCardCheckOperationCompleted, userState);
        }
        
        private void OnIDCardCheckOperationCompleted(object arg) {
            if ((this.IDCardCheckCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IDCardCheckCompleted(this, new IDCardCheckCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPatInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetPatInfo(string RequestXml) {
            object[] results = this.Invoke("GetPatInfo", new object[] {
                        RequestXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetPatInfoAsync(string RequestXml) {
            this.GetPatInfoAsync(RequestXml, null);
        }
        
        /// <remarks/>
        public void GetPatInfoAsync(string RequestXml, object userState) {
            if ((this.GetPatInfoOperationCompleted == null)) {
                this.GetPatInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPatInfoOperationCompleted);
            }
            this.InvokeAsync("GetPatInfo", new object[] {
                        RequestXml}, this.GetPatInfoOperationCompleted, userState);
        }
        
        private void OnGetPatInfoOperationCompleted(object arg) {
            if ((this.GetPatInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPatInfoCompleted(this, new GetPatInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/JudgeCanMedicare", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string JudgeCanMedicare(string RequestXml) {
            object[] results = this.Invoke("JudgeCanMedicare", new object[] {
                        RequestXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void JudgeCanMedicareAsync(string RequestXml) {
            this.JudgeCanMedicareAsync(RequestXml, null);
        }
        
        /// <remarks/>
        public void JudgeCanMedicareAsync(string RequestXml, object userState) {
            if ((this.JudgeCanMedicareOperationCompleted == null)) {
                this.JudgeCanMedicareOperationCompleted = new System.Threading.SendOrPostCallback(this.OnJudgeCanMedicareOperationCompleted);
            }
            this.InvokeAsync("JudgeCanMedicare", new object[] {
                        RequestXml}, this.JudgeCanMedicareOperationCompleted, userState);
        }
        
        private void OnJudgeCanMedicareOperationCompleted(object arg) {
            if ((this.JudgeCanMedicareCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.JudgeCanMedicareCompleted(this, new JudgeCanMedicareCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateCardPatInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateCardPatInfo(string RequestXml) {
            object[] results = this.Invoke("CreateCardPatInfo", new object[] {
                        RequestXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CreateCardPatInfoAsync(string RequestXml) {
            this.CreateCardPatInfoAsync(RequestXml, null);
        }
        
        /// <remarks/>
        public void CreateCardPatInfoAsync(string RequestXml, object userState) {
            if ((this.CreateCardPatInfoOperationCompleted == null)) {
                this.CreateCardPatInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateCardPatInfoOperationCompleted);
            }
            this.InvokeAsync("CreateCardPatInfo", new object[] {
                        RequestXml}, this.CreateCardPatInfoOperationCompleted, userState);
        }
        
        private void OnCreateCardPatInfoOperationCompleted(object arg) {
            if ((this.CreateCardPatInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateCardPatInfoCompleted(this, new CreateCardPatInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/OPRegist", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string OPRegist(string RequestXml) {
            object[] results = this.Invoke("OPRegist", new object[] {
                        RequestXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void OPRegistAsync(string RequestXml) {
            this.OPRegistAsync(RequestXml, null);
        }
        
        /// <remarks/>
        public void OPRegistAsync(string RequestXml, object userState) {
            if ((this.OPRegistOperationCompleted == null)) {
                this.OPRegistOperationCompleted = new System.Threading.SendOrPostCallback(this.OnOPRegistOperationCompleted);
            }
            this.InvokeAsync("OPRegist", new object[] {
                        RequestXml}, this.OPRegistOperationCompleted, userState);
        }
        
        private void OnOPRegistOperationCompleted(object arg) {
            if ((this.OPRegistCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.OPRegistCompleted(this, new OPRegistCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void NetTestCompletedEventHandler(object sender, NetTestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NetTestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal NetTestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetServiceTimeCompletedEventHandler(object sender, GetServiceTimeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetServiceTimeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetServiceTimeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void IDCardCheckCompletedEventHandler(object sender, IDCardCheckCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IDCardCheckCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IDCardCheckCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetPatInfoCompletedEventHandler(object sender, GetPatInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPatInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPatInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void JudgeCanMedicareCompletedEventHandler(object sender, JudgeCanMedicareCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class JudgeCanMedicareCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal JudgeCanMedicareCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void CreateCardPatInfoCompletedEventHandler(object sender, CreateCardPatInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateCardPatInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateCardPatInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void OPRegistCompletedEventHandler(object sender, OPRegistCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class OPRegistCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal OPRegistCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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

#pragma warning restore 1591