﻿#pragma checksum "..\..\AdminPanel.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3262185F87ECDF5527A4A93A97825A9B3C353A511D9915CB4EDC296ED1D158A9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Library_Management_System;
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


namespace Library_Management_System {
    
    
    /// <summary>
    /// AdminPanel
    /// </summary>
    public partial class AdminPanel : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 53 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle listBook;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle addbook;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle updatebook;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle listUser;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle updateUser;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle deletebook;
        
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
            System.Uri resourceLocater = new System.Uri("/Library Management System;component/adminpanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AdminPanel.xaml"
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
            this.listBook = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 53 "..\..\AdminPanel.xaml"
            this.listBook.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.listBook_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.addbook = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 54 "..\..\AdminPanel.xaml"
            this.addbook.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.addbook_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.updatebook = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 55 "..\..\AdminPanel.xaml"
            this.updatebook.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.updatebook_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listUser = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 56 "..\..\AdminPanel.xaml"
            this.listUser.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.listUser_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.updateUser = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 57 "..\..\AdminPanel.xaml"
            this.updateUser.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.updateUser_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.deletebook = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 58 "..\..\AdminPanel.xaml"
            this.deletebook.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.deletebook_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

