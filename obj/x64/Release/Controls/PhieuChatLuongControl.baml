   M S B A M L   `   `   `  ���� U  QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nulleHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf   XuatThuy-   'XuatThuy.Controls.PhieuChatLuongControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     cmaterialDesignHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf        MaterialDesignBody#���   $��Regular"�$��14(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � DataContext �� *PhieuChatLuongVM+  Locator �� Source# [ .��� .+�#   MaterialDesignThemes.Wpf.Card  $	��5 0q�$��Stretch=�$��Stretch=�.���  � RowDefinitions �� $��* ��� $	��100 �.+��� .+�&   System.Windows.Controls.DataGrid     AutoGenerateColumns .  $
��Auto=�$	��5 0q�$	��500��$	��Top=� �� SelectedItem �� *SelectedPhieuChatLuong+���� *PhieuChatLuongs+   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"   & "MaterialDesignDataGridColumnHeader	 �� BasedOn#	 [ .��� 
 �� Property
 � �� �� Value �#2196F3�� 
 � ��
 ������� 
 � ��$
 Bold"��� 
 � ��$	 100��   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��-   	dgrID��   CellTemplate �� .R��� $��Center=����� *ID+  $
 Tên��   MinWidth$	 100Q�-   
dgrTen�� �� .R��� $��Center=����� *Ten+   CellEditingTemplate �� .R��� $��Center=����� *Ten+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�  $ Ngày cấp��$	 130Q�-   
dgrNgayCap�� �� .R��� $��Center=����� *	NgayCap+ �� StringFormat$ 
dd-MM-yyyy�� �� .R�(  "System.Windows.Controls.DatePicker  $��Center=�   Text �� *	NgayCap+$ TwoWay=�$ PropertyChanged=�  -   dgrEdit�� �� .R��� ����  �� Path$' !DataContext.CmdEditPhieuChatLuong� �� RelativeSource ��  �� AncestorType"  ���� {��� *	CanDelete+.��Sửa  -   	dgrDelete�� �� .R��� 
���  ������ $) #DataContext.CmdDeletePhieuChatLuong� �� "  ���� {��� *	CanDelete+.��Xóa�� $
��Left=� �� Orientation$ 
Horizontal=�$	��0 5q�.+�� ����  �� ElementName$ dgrID��$ ActualWidth�� ���� $ dgrTen��$ ActualWidth�.+���   MaterialDesignTextBox#��[ $��5 0 0 0q����� *TenMoi+� ���� $ 
dgrNgayCap��$ ActualWidth�.+�  $	��5 0q�   SelectedDateFormat$ Short=�$��Bottom=�   SelectedDate �� *
NgayCapMoi+� ���� $ dgrEdit��$ ActualWidth�.+���   MaterialDesignRaisedButton#��[ $��12 0 0 0q�$��Center=����� *CmdAddPhieuChatLuong+.��Thêm