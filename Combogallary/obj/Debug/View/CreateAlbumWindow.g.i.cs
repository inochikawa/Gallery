﻿#pragma checksum "..\..\..\View\CreateAlbumWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "94DFD706D585D1F2807B9D7D5AC0E072"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Combogallary {
    
    
    /// <summary>
    /// CreateAlbumWindow
    /// </summary>
    public partial class CreateAlbumWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\View\CreateAlbumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lsbPics;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\View\CreateAlbumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectFolder;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\View\CreateAlbumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAlbumName;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\View\CreateAlbumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtPicCount;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\CreateAlbumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chekBoxPublicAccess;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\View\CreateAlbumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreate;
        
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
            System.Uri resourceLocater = new System.Uri("/Combogallary;component/view/createalbumwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\CreateAlbumWindow.xaml"
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
            this.lsbPics = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.btnSelectFolder = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\View\CreateAlbumWindow.xaml"
            this.btnSelectFolder.Click += new System.Windows.RoutedEventHandler(this.btnSelectFolder_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtAlbumName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtPicCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.chekBoxPublicAccess = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.btnCreate = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\View\CreateAlbumWindow.xaml"
            this.btnCreate.Click += new System.Windows.RoutedEventHandler(this.btnCreate_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

