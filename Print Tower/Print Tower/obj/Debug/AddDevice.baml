   M S B A M L   `   `   `  ����F  BPrint Tower, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Print_TowerPrint_Tower  5   	       Print_Tower.AddDevice   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Print_Tower     5   	     � Title$'  !Добави устройство��5   	   $	��450��6#   $	��500��60    � 
ResizeMode$ NoResize=�6<   .��5      ��  �� Padding$ 10q�6   ._�5   
   �� .+�5      �� $*��$Име на устройството��6   $��18(�63    �� Padding$ 5q�6A   �� 5      -   5        $��DeviceName_Block��5      $��30��6.   
�������6S   �� 5      $��IP aдрес��6   $��18(�6(   $ 5q�66   �� 5      -   5        $��IP_Block��5      $��30��6&   
�������6K   �� 5      $0��*Полета на устройството��6   $��18(�66   $ 5q�6D   &   System.Windows.Controls.DataGrid  5      -   5        $��DevicePropsGrid��5         AutoGenerateColumns .  $	��160��6J      
IsReadOnly .    CanUserDeleteRows .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Име��6)   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 66   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6@   ��  �� Path$ PropName�6f     5      $ Стойност��6)   $	 * 6;   
 6E   �� $ PropOID�6i   5      �� 5      $ 20q�6   ._�5      �  � ColumnDefinitions 5      �� 5      $��* �6+   �� 5      $��* �6+   �� 5       $��* �6+   �� 5!      $��* �6+   .+�5$      �� -   5        $��AddProperty_Button��5$      $
��Bold"�67   
����������#228B22
�������$��Добави поле��6�   $��1��6�   $	��5,0q�6�   $��40��6�   6�   �� 5%      -   5        $��
Add_Button��5%      $
��Bold"�6/   
����������#228B22
�������$��Добави устр.��6}   $��2��6�   $	��5,0q�6�   6�   �� 5&      -   5        $��Cancel_Button��5&      $
��Bold"�62   
����������#228B22
�������$��
Отказ��6�   $��3��6�   $��5,0,0,0q�6�   6�   5'      5(      5)      5*      5+      