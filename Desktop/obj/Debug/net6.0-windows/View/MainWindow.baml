   M S B A M L   `   `   `  ����B  >Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DesktopDesktop  5   	   5clr-namespace:Desktop.ViewModelDesktop.ViewModel  6-       Desktop.View.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   !localclr-namespace:Desktop  5   	   /	viewmodelclr-namespace:Desktop.ViewModel  6-     � Title$  FoodOrderDataBase��5	   	   $	��700��6#   $	��759��60    � WindowStyle$ SingleBorderWindow=�6<    � 
ResizeMode$ NoResize=�6]    � WindowStartupLocation$ CenterScreen=�6s   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5
      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ ContentRendered��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6$   �� *RefreshCommand+5      .��5      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��Kijelentkezés��69    m� Command 6   �� *LogoutCommand+69   �� 5      m� 5      $��Frissítés��6:    6   �� *RefreshCommand+6:   �� 5      m� 5      $��Új cikk hozzáadása��6:    6   �� *NewItemCommand+6:   5      � 5   
   	 � RowDefinitions	 5      �� 5      $
��Auto �6    �� 5      $��2* �6    �� 5      $
��Auto �6    �� 5      $��3* �6    .+�5      �� $��0��6   ��5       �� 5!      $��	Szűrők:��6   .��5#      � 	 5$      �� 5%      $
��Auto �6(   �� 5&      $
��Auto �6(   �� 5'      $
��Auto �6(   �� 5(      $
��Auto �6(   �� 5)      $
��Auto �6(   �� 5*      $
��Auto �6(   
 � ColumnDefinitions
 5,      �� 5-      $
��Auto �6+   �� 5.      $
��Auto �6+   .+�50      	� $��0��6"   $��0��6/    	� 	GroupName$ filter��6?    d� 	IsChecked$
 TrueY�6R   $��1��6�   ��6c   �� *FilterModeCommand+.��6�   	Összes6�   	� 51      $��1��6"   $��0��6/   $ filter��6?   $��2��6x   ��6R   �� *FilterModeCommand+.��6�   Teljesített6�   	� 52      $��2��6"   $��0��6/   $ filter��6?   $��3��6x   ��6R   �� *FilterModeCommand+.��6�   Teljesítetlen6�   �� 53      $��3��6   $��0��6)   $��Név: ��69   �� 54      $��3��6   $��1��6+   $	��300��6;   ��6J   �� *
FilterName+�� 55      $��4��6   $��0��6)   $��Cím: ��69   �� 56      $��4��6   $��1��6+   $	��300��6;   $��0,2,0,0q�6i   ��6J   �� *FilterAddress+6i   �� 57      $��5��6   $��0��6*   $��Szűrés��6;   $��20��6n   $��50��6z   $
��Left=�6�   $��	10,5,0,10q�6�   ��6L   �� *FilterCommand+6�   58      59      �� 5:      $��1��6   ��5;      �� 5<      $��Rendelések:��6   .��5>      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGrid��5>      $��Stretch=�6-      
IsReadOnly$
 True��5?   :      AutoGenerateColumns$ False��5@      ��5?      �� *Orders+ �� SelectedItem 5A      �� *SelectedOrder+ 5B        5C      $ SelectionChanged��6)   . 5D         64   �� *SelectCommand+5E         Columns 5G      0  *System.Windows.Controls.DataGridTextColumn  5H      ,  &System.Windows.Controls.DataGridColumn   Header$ Leadás időpontja��6-   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *TimeOfOrder+  5I      $ Teljesítés időpontja��6-    6K   �� *TimeOfCompletion+  5J      $
 Név��6-    6:   �� *Name+  5K      $
 Cím��6-    6:   �� *	Address+  5L      $ Telefonszám��6-    6B   �� *PhoneNumber+  5M      $ Összeg��6-    6=   �� *
TotalPrice+5O      5P      �� 5R      $��2��6    �� Orientation$ 
Horizontal=�6&   .+�5S      �� $��Teljesítés��6   $��50��6�   $��20��6�   $
��Left=�6�   $��	15,5,0,10q�6�   ��6.   �� *CompleteOrderCommand+��6W   �� *SelectedOrder+6�   5T      �� 5U      $��3��6   ��5V      �� 5W      $��Ételek/italok:��6   .��5Y        -   5        $ 	CartItems��5Z      $��Stretch=�5Y      $
 True��5Z   Q   $ False��5[      ��5Z   /   �� *	CartItems+ 5\      �� *CurrentItem+ 5]        5^      $ Id��6-    69   �� *	Item.Id+  5_      $
 Name��6-    6;   �� *	Item.Name+  5`      $ Price��6-    6<   �� *
Item.Price+  5a      $ Description��6-      MaxWidth$	 350Q�6g      MinWidth$	 200Q�6v      Width5	  /System.Windows.Controls.DataGridLengthConverter$	 200	 6�    6B   �� *Item.Description+   ElementStyle 5b      �� 5c   "    �� 
TargetType" ��6(   .�5d   &   ��  �� Property � �� �� Value$
 Wrap=�6E   5e   #   5g      4
  .System.Windows.Controls.DataGridCheckBoxColumn
  5h      $ Spicy��61    6@   �� *
Item.Spicy+
  5i      $ Vegan��61    6@   �� *
Item.Vegan+  5j      $ 
OrderedCnt��6-    6A   �� *Item.OrderedCnt+  5k      $ Category��6-    6?   �� *Item.Category.Name+  5l      $ Count��6-    6<   �� *
Quantity+5n      5o      5p      5q      5r      