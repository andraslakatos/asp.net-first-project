   M S B A M L   `   `   `  ����  Desktop    Desktop.View.LoginWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Bejelentkezés��5   	   $	��150��6   $	��250��6,   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6   $��Azonosító:��6-   �� 5   
   -   5        $��txtName��5   :   $��0��6   $��1��6   $��5q�6/   ��5      �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��1��6   $��0��6   $��Jelszó:��6-   G� 5   
   -   5        $��txtPassword��5   3   $��1��6   $��1��6#   $��5q�6F   �� 5   
   $��2��6   $��	Belépés��6   $
��Left=�5      $	��Top=�6,   $��2q�5       �� 	IsDefault$
 True��5      $��1��5   F   $��98��6V   $��61,1,0,0q�6a   ��5      �� *LoginCommand+��5      ��  �� ElementName$ txtPassword��6a   5      5      