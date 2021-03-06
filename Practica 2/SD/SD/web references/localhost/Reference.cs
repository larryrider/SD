﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace SD.localhost {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ObjetoRemotoSoap11Binding", Namespace="http://master")]
    public partial class ObjetoRemoto : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getVolumenOperationCompleted;
        
        private System.Threading.SendOrPostCallback getLuzOperationCompleted;
        
        private System.Threading.SendOrPostCallback logOperationCompleted;
        
        private System.Threading.SendOrPostCallback getUltimaFechaOperationCompleted;
        
        private System.Threading.SendOrPostCallback getFechaOperationCompleted;
        
        private System.Threading.SendOrPostCallback setLuzOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ObjetoRemoto() {
            this.Url = global::SD.Properties.Settings.Default.SD_localhost_ObjetoRemoto;
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
        public event getVolumenCompletedEventHandler getVolumenCompleted;
        
        /// <remarks/>
        public event getLuzCompletedEventHandler getLuzCompleted;
        
        /// <remarks/>
        public event logCompletedEventHandler logCompleted;
        
        /// <remarks/>
        public event getUltimaFechaCompletedEventHandler getUltimaFechaCompleted;
        
        /// <remarks/>
        public event getFechaCompletedEventHandler getFechaCompleted;
        
        /// <remarks/>
        public event setLuzCompletedEventHandler setLuzCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:getVolumen", RequestNamespace="http://master", ResponseNamespace="http://master", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string getVolumen([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ip, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string usu) {
            object[] results = this.Invoke("getVolumen", new object[] {
                        ip,
                        usu});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getVolumenAsync(string ip, string usu) {
            this.getVolumenAsync(ip, usu, null);
        }
        
        /// <remarks/>
        public void getVolumenAsync(string ip, string usu, object userState) {
            if ((this.getVolumenOperationCompleted == null)) {
                this.getVolumenOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetVolumenOperationCompleted);
            }
            this.InvokeAsync("getVolumen", new object[] {
                        ip,
                        usu}, this.getVolumenOperationCompleted, userState);
        }
        
        private void OngetVolumenOperationCompleted(object arg) {
            if ((this.getVolumenCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getVolumenCompleted(this, new getVolumenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:getLuz", RequestNamespace="http://master", ResponseNamespace="http://master", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string getLuz([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ip, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string usu) {
            object[] results = this.Invoke("getLuz", new object[] {
                        ip,
                        usu});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getLuzAsync(string ip, string usu) {
            this.getLuzAsync(ip, usu, null);
        }
        
        /// <remarks/>
        public void getLuzAsync(string ip, string usu, object userState) {
            if ((this.getLuzOperationCompleted == null)) {
                this.getLuzOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetLuzOperationCompleted);
            }
            this.InvokeAsync("getLuz", new object[] {
                        ip,
                        usu}, this.getLuzOperationCompleted, userState);
        }
        
        private void OngetLuzOperationCompleted(object arg) {
            if ((this.getLuzCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getLuzCompleted(this, new getLuzCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:log", RequestNamespace="http://master", OneWay=true, Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void log([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string accion, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ip, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string usuario) {
            this.Invoke("log", new object[] {
                        accion,
                        ip,
                        usuario});
        }
        
        /// <remarks/>
        public void logAsync(string accion, string ip, string usuario) {
            this.logAsync(accion, ip, usuario, null);
        }
        
        /// <remarks/>
        public void logAsync(string accion, string ip, string usuario, object userState) {
            if ((this.logOperationCompleted == null)) {
                this.logOperationCompleted = new System.Threading.SendOrPostCallback(this.OnlogOperationCompleted);
            }
            this.InvokeAsync("log", new object[] {
                        accion,
                        ip,
                        usuario}, this.logOperationCompleted, userState);
        }
        
        private void OnlogOperationCompleted(object arg) {
            if ((this.logCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.logCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:getUltimaFecha", RequestNamespace="http://master", ResponseNamespace="http://master", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string getUltimaFecha([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ip, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string usu) {
            object[] results = this.Invoke("getUltimaFecha", new object[] {
                        ip,
                        usu});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getUltimaFechaAsync(string ip, string usu) {
            this.getUltimaFechaAsync(ip, usu, null);
        }
        
        /// <remarks/>
        public void getUltimaFechaAsync(string ip, string usu, object userState) {
            if ((this.getUltimaFechaOperationCompleted == null)) {
                this.getUltimaFechaOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUltimaFechaOperationCompleted);
            }
            this.InvokeAsync("getUltimaFecha", new object[] {
                        ip,
                        usu}, this.getUltimaFechaOperationCompleted, userState);
        }
        
        private void OngetUltimaFechaOperationCompleted(object arg) {
            if ((this.getUltimaFechaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getUltimaFechaCompleted(this, new getUltimaFechaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:getFecha", RequestNamespace="http://master", ResponseNamespace="http://master", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public string getFecha([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ip, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string usu) {
            object[] results = this.Invoke("getFecha", new object[] {
                        ip,
                        usu});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getFechaAsync(string ip, string usu) {
            this.getFechaAsync(ip, usu, null);
        }
        
        /// <remarks/>
        public void getFechaAsync(string ip, string usu, object userState) {
            if ((this.getFechaOperationCompleted == null)) {
                this.getFechaOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetFechaOperationCompleted);
            }
            this.InvokeAsync("getFecha", new object[] {
                        ip,
                        usu}, this.getFechaOperationCompleted, userState);
        }
        
        private void OngetFechaOperationCompleted(object arg) {
            if ((this.getFechaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getFechaCompleted(this, new getFechaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:setLuz", RequestNamespace="http://master", OneWay=true, Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void setLuz([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string luzNueva, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string ip, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string usu) {
            this.Invoke("setLuz", new object[] {
                        luzNueva,
                        ip,
                        usu});
        }
        
        /// <remarks/>
        public void setLuzAsync(string luzNueva, string ip, string usu) {
            this.setLuzAsync(luzNueva, ip, usu, null);
        }
        
        /// <remarks/>
        public void setLuzAsync(string luzNueva, string ip, string usu, object userState) {
            if ((this.setLuzOperationCompleted == null)) {
                this.setLuzOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsetLuzOperationCompleted);
            }
            this.InvokeAsync("setLuz", new object[] {
                        luzNueva,
                        ip,
                        usu}, this.setLuzOperationCompleted, userState);
        }
        
        private void OnsetLuzOperationCompleted(object arg) {
            if ((this.setLuzCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.setLuzCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getVolumenCompletedEventHandler(object sender, getVolumenCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getVolumenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getVolumenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getLuzCompletedEventHandler(object sender, getLuzCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getLuzCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getLuzCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void logCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getUltimaFechaCompletedEventHandler(object sender, getUltimaFechaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getUltimaFechaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getUltimaFechaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getFechaCompletedEventHandler(object sender, getFechaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getFechaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getFechaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void setLuzCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591