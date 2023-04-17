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

namespace SOC.Local.Gyzl.AppointPlatForm.AppointmentService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="AppointmentServiceSoap", Namespace="FS.his")]
    public partial class AppointmentService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback stopRegOperationCompleted;
        
        private System.Threading.SendOrPostCallback refundPayOperationCompleted;
        
        private System.Threading.SendOrPostCallback printRegInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback cancelOrderbyHisOperationCompleted;
        
        private System.Threading.SendOrPostCallback payOrderByHisOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AppointmentService() {
            this.Url = global::SOC.Local.Gyzl.AppointPlatForm.Properties.Settings.Default.SOC_Local_Gyzl_AppointPlatForm_AppointmentService_AppointmentService;
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
        public event stopRegCompletedEventHandler stopRegCompleted;
        
        /// <remarks/>
        public event refundPayCompletedEventHandler refundPayCompleted;
        
        /// <remarks/>
        public event printRegInfoCompletedEventHandler printRegInfoCompleted;
        
        /// <remarks/>
        public event cancelOrderbyHisCompletedEventHandler cancelOrderbyHisCompleted;
        
        /// <remarks/>
        public event payOrderByHisCompletedEventHandler payOrderByHisCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("FS.his/stopReg", RequestNamespace="FS.his", ResponseNamespace="FS.his", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string stopReg(string deptId, string doctorId, string beginDate, string endDate, string timeFlag, string reason) {
            object[] results = this.Invoke("stopReg", new object[] {
                        deptId,
                        doctorId,
                        beginDate,
                        endDate,
                        timeFlag,
                        reason});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void stopRegAsync(string deptId, string doctorId, string beginDate, string endDate, string timeFlag, string reason) {
            this.stopRegAsync(deptId, doctorId, beginDate, endDate, timeFlag, reason, null);
        }
        
        /// <remarks/>
        public void stopRegAsync(string deptId, string doctorId, string beginDate, string endDate, string timeFlag, string reason, object userState) {
            if ((this.stopRegOperationCompleted == null)) {
                this.stopRegOperationCompleted = new System.Threading.SendOrPostCallback(this.OnstopRegOperationCompleted);
            }
            this.InvokeAsync("stopReg", new object[] {
                        deptId,
                        doctorId,
                        beginDate,
                        endDate,
                        timeFlag,
                        reason}, this.stopRegOperationCompleted, userState);
        }
        
        private void OnstopRegOperationCompleted(object arg) {
            if ((this.stopRegCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.stopRegCompleted(this, new stopRegCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("FS.his/refundPay", RequestNamespace="FS.his", ResponseNamespace="FS.his", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string refundPay(string orderId, string refundType, string refundTime, string returnFee, string refundReason) {
            object[] results = this.Invoke("refundPay", new object[] {
                        orderId,
                        refundType,
                        refundTime,
                        returnFee,
                        refundReason});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void refundPayAsync(string orderId, string refundType, string refundTime, string returnFee, string refundReason) {
            this.refundPayAsync(orderId, refundType, refundTime, returnFee, refundReason, null);
        }
        
        /// <remarks/>
        public void refundPayAsync(string orderId, string refundType, string refundTime, string returnFee, string refundReason, object userState) {
            if ((this.refundPayOperationCompleted == null)) {
                this.refundPayOperationCompleted = new System.Threading.SendOrPostCallback(this.OnrefundPayOperationCompleted);
            }
            this.InvokeAsync("refundPay", new object[] {
                        orderId,
                        refundType,
                        refundTime,
                        returnFee,
                        refundReason}, this.refundPayOperationCompleted, userState);
        }
        
        private void OnrefundPayOperationCompleted(object arg) {
            if ((this.refundPayCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.refundPayCompleted(this, new refundPayCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("FS.his/printRegInfo", RequestNamespace="FS.his", ResponseNamespace="FS.his", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string printRegInfo(string orderId, string infoSeq, string InfoTime) {
            object[] results = this.Invoke("printRegInfo", new object[] {
                        orderId,
                        infoSeq,
                        InfoTime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void printRegInfoAsync(string orderId, string infoSeq, string InfoTime) {
            this.printRegInfoAsync(orderId, infoSeq, InfoTime, null);
        }
        
        /// <remarks/>
        public void printRegInfoAsync(string orderId, string infoSeq, string InfoTime, object userState) {
            if ((this.printRegInfoOperationCompleted == null)) {
                this.printRegInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnprintRegInfoOperationCompleted);
            }
            this.InvokeAsync("printRegInfo", new object[] {
                        orderId,
                        infoSeq,
                        InfoTime}, this.printRegInfoOperationCompleted, userState);
        }
        
        private void OnprintRegInfoOperationCompleted(object arg) {
            if ((this.printRegInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.printRegInfoCompleted(this, new printRegInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("FS.his/cancelOrderbyHis", RequestNamespace="FS.his", ResponseNamespace="FS.his", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string cancelOrderbyHis(string orderId, string cancelTime, string cancelReason) {
            object[] results = this.Invoke("cancelOrderbyHis", new object[] {
                        orderId,
                        cancelTime,
                        cancelReason});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void cancelOrderbyHisAsync(string orderId, string cancelTime, string cancelReason) {
            this.cancelOrderbyHisAsync(orderId, cancelTime, cancelReason, null);
        }
        
        /// <remarks/>
        public void cancelOrderbyHisAsync(string orderId, string cancelTime, string cancelReason, object userState) {
            if ((this.cancelOrderbyHisOperationCompleted == null)) {
                this.cancelOrderbyHisOperationCompleted = new System.Threading.SendOrPostCallback(this.OncancelOrderbyHisOperationCompleted);
            }
            this.InvokeAsync("cancelOrderbyHis", new object[] {
                        orderId,
                        cancelTime,
                        cancelReason}, this.cancelOrderbyHisOperationCompleted, userState);
        }
        
        private void OncancelOrderbyHisOperationCompleted(object arg) {
            if ((this.cancelOrderbyHisCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.cancelOrderbyHisCompleted(this, new cancelOrderbyHisCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("FS.his/payOrderByHis", RequestNamespace="FS.his", ResponseNamespace="FS.his", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string payOrderByHis(string orderId, string payAmout, string payTime) {
            object[] results = this.Invoke("payOrderByHis", new object[] {
                        orderId,
                        payAmout,
                        payTime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void payOrderByHisAsync(string orderId, string payAmout, string payTime) {
            this.payOrderByHisAsync(orderId, payAmout, payTime, null);
        }
        
        /// <remarks/>
        public void payOrderByHisAsync(string orderId, string payAmout, string payTime, object userState) {
            if ((this.payOrderByHisOperationCompleted == null)) {
                this.payOrderByHisOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpayOrderByHisOperationCompleted);
            }
            this.InvokeAsync("payOrderByHis", new object[] {
                        orderId,
                        payAmout,
                        payTime}, this.payOrderByHisOperationCompleted, userState);
        }
        
        private void OnpayOrderByHisOperationCompleted(object arg) {
            if ((this.payOrderByHisCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.payOrderByHisCompleted(this, new payOrderByHisCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void stopRegCompletedEventHandler(object sender, stopRegCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class stopRegCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal stopRegCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void refundPayCompletedEventHandler(object sender, refundPayCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class refundPayCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal refundPayCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void printRegInfoCompletedEventHandler(object sender, printRegInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class printRegInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal printRegInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void cancelOrderbyHisCompletedEventHandler(object sender, cancelOrderbyHisCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class cancelOrderbyHisCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal cancelOrderbyHisCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void payOrderByHisCompletedEventHandler(object sender, payOrderByHisCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class payOrderByHisCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal payOrderByHisCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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