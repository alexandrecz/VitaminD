﻿#pragma checksum "D:\PortF\WP\VitaminD\VitaminD\Views\SourceView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AD9F6F2F9AB6931C407AD394486D7288"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace VitaminD.Views {
    
    
    public partial class SourceView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard StPlay;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock ViewTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Image ImageSource;
        
        internal System.Windows.Controls.Border border;
        
        internal System.Windows.Controls.TextBlock TextSource;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/VitaminD;component/Views/SourceView.xaml", System.UriKind.Relative));
            this.StPlay = ((System.Windows.Media.Animation.Storyboard)(this.FindName("StPlay")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ViewTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ViewTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.ImageSource = ((System.Windows.Controls.Image)(this.FindName("ImageSource")));
            this.border = ((System.Windows.Controls.Border)(this.FindName("border")));
            this.TextSource = ((System.Windows.Controls.TextBlock)(this.FindName("TextSource")));
        }
    }
}

