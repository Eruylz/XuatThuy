﻿#pragma checksum "..\..\..\View\PhieuCanWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "658A6314F64B257C497065BC419EED607D639E5B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
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
using XuatThuy.PLCs;
using XuatThuy.Utils;
using XuatThuy.ViewModel;


namespace XuatThuy.ViewModel {
    
    
    /// <summary>
    /// PhieuCanWindow
    /// </summary>
    public partial class PhieuCanWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle recPLC;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMSGH;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTLXuat;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDonVi;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxLoaiXM;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxBenNhanHang;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLaiTau;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSoTau;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSoDuoiTau;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSoPhieuChatLuong;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dptNgayCapPCL;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxNVXuatHang;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxNVBaoVe;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxHinhThucCan;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxSoLoXM;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\View\PhieuCanWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgrPhieuCanChiTiet;
        
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
            System.Uri resourceLocater = new System.Uri("/XuatThuy;component/view/phieucanwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\PhieuCanWindow.xaml"
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
            this.recPLC = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 2:
            this.txtMSGH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtTLXuat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtDonVi = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cbxLoaiXM = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.cbxBenNhanHang = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.txtLaiTau = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtSoTau = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtSoDuoiTau = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.txtSoPhieuChatLuong = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.dptNgayCapPCL = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 12:
            this.cbxNVXuatHang = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 13:
            this.cbxNVBaoVe = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 14:
            this.cbxHinhThucCan = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 15:
            this.cbxSoLoXM = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 16:
            this.dgrPhieuCanChiTiet = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

