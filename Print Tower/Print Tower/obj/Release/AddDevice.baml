   M S B A M L   `   `   `  ���� F  BPrint Tower, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Print_TowerPrint_Tower      Print_Tower.AddDevice   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Print_Tower       � Title$'  !Добави устройство��$	��450��$	��500�� � 
ResizeMode$ NoResize=�.����  �� Padding$ 10q�._��� .+��� $*��$Име на устройството��$��18(� �� Padding$ 5q��� -   $��DeviceName_Block��$��30��
��������� $��IP aдрес��$��18(�$ 5q��� -   $��IP_Block��$��30��
��������� $0��*Полета на устройството��$��18(�$ 5q�&   System.Windows.Controls.DataGrid  -   $��DevicePropsGrid��   AutoGenerateColumns .  $	��160��   CanUserDeleteRows .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Име��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� 
 �� Path$
 PropName�  $ Стойност��$ * 	 �� $
 PropOID��� $ 20q�._��  � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��AddProperty_Button��$
��Bold"�
����������#228B22
�������$��Добави поле��$��1��$	��5,0q�$��40���� -   $��
Add_Button��$
��Bold"�
����������#228B22
�������$��Добави устр.��$��2��$	��5,0q��� -   $��Cancel_Button��$
��Bold"�
����������#228B22
�������$��
Отказ��$��3��$��5,0,0,0q�