﻿#pragma checksum "..\..\..\Views\ServiceRequestStatusPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "88A64B5E5666684E88F95E4B845524D8F8EA5A949E0103E98E5BD374EEDA8252"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels;
using ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views {
    
    
    /// <summary>
    /// ServiceRequestStatusPage
    /// </summary>
    public partial class ServiceRequestStatusPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Views\ServiceRequestStatusPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views.ServiceRequestStatusPage ServiceRequestPage;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Views\ServiceRequestStatusPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRequestId;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\ServiceRequestStatusPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbStatusFilter;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\ServiceRequestStatusPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbPriorityFilter;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Views\ServiceRequestStatusPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView dataGridRequests;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ST10096757_MoniqueJackson_MunicipalityApp_Part2;component/views/servicerequestst" +
                    "atuspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ServiceRequestStatusPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ServiceRequestPage = ((ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views.ServiceRequestStatusPage)(target));
            return;
            case 2:
            this.txtRequestId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cmbStatusFilter = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.cmbPriorityFilter = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.dataGridRequests = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

