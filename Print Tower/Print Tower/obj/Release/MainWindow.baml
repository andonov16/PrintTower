   M S B A M L   `   `   `  ���� F  BPrint Tower, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Print_TowerPrint_Tower      Print_Tower.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Print_Tower       � Title$  Print Tower��$	��450��$	��800��
�������.����  �� Padding$ 10q�._��  � ColumnDefinitions �� $��* ��� $��* �.+��� $��2��.+��  �� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $��6* �.+��� -   $��AddDevice_Button��
�������$��18(����#228B22
�������$
��Bold"�$��0��$��55��$��Добави���� -   $��DeleteDevice_Button��$
��Bold"�
�������$��18(����#228B22
�������$��1��$��Изтрий���� -   $��Settings_Button��$
��Bold"�
�������$��18(����#228B22
�������$��3��$��Настройки��&   System.Windows.Controls.DataGrid  -   $��PrintersList��$��1��$
��0,10q�   AutoGenerateColumns .     CanUserDeleteRows .  $	��333��   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"   �� Triggers �� 	 �� Binding	 �� *Online+
 �� Value	
 false.a���  �� Property � �� �� Value
 �\\���� 	 �� *Online+
 true.a���  � ��
 ����   Columns 0  *System.Windows.Controls.DataGridTextColumn     FontSize$ 16Q�,  &System.Windows.Controls.DataGridColumn   Header$ Име��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ 
DeviceName�  $ 16Q�$ IP адрес��$ *  �� $ IP�  $ На линия��$ *  �� $ Online��� $��0,0,10,0q�$��5q�._��� -   $��ExplorePanel��.+��� -   $��DeviceName_Block��$��26(�$��0���� $*��$Име на устройството��$��0,0,0,20q��� -   $��DeviceIP_Block��$��26(��� $��IP адрес��$��0,0,0,20q��  �� $��2* ��� $��* ��� $��* �.+��� -   $��AddProp��$��
-10,0,10,0q�$
��Bold"�
����������#228B22
�������$��30��$��Добави поле��$��1���� -	   $��
DeleteProp��$
��Bold"�
����������#228B22
�������$��Изтрий поле��$��30��$��2��  -
   $��DevPropsGrid��$��Stretch=�$��Stretch=� .  $	��222��   $ Име на полето��$ *  �� $ PropName�  $ OID на полето��$ *  �� $ PropOID�  $ Стойност��$ *  �� $ 	PropValue�