   M S B A M L   `   `   `  ����  Desktop     Desktop.View.NewItemWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	     � Title$  TodoList - Item editor��5   	   $
��Auto��6(   $	��400��66    � 
ResizeMode$ NoResize=�5	   	    � SizeToContent$ Height=�6   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5
      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ Closed��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6$   �� *CancelNewItemCommand+5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6   $��Name:��6-   $��Center=�6=   �� 5    
   $��0��6   $��1��6   $��5q�6/   ��5!      �� *NewItem.Name+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 5#   
   $��1��6   $��0��6   $��Price:��6-   $��Center=�6>   �� 5$   
   $��1��6   $��1��6   $��5q�6/   ��5%      �� *NewItem.Price+$	 PropertyChanged=��� 5'   
   $��2��6   $��0��6   $��Description:��6-   $��Center=�6D   �� 5(   
   $��2��6   $��1��6   $��5q�6/   
 �� TextWrapping$

 Wrap=�6:    �� MaxLines$ 2��6N   " �� VerticalScrollBarVisibility$
 Auto=�6[   ��5)      �� *NewItem.Description+�� 5+   
   $��3��6   $��0��6   $��Spicy:��6-   $��Center=�6>   �� 5,   
   -   5         ��Name$ SpicyCheckbox��5,   0   $��3��6   $��1��6    $��Center=�6k   $��5,0,0,0q�6�    d� 	IsChecked 6G   �� *NewItem.Spicy+6�   �� 5.   
   $��4��6   $��0��6   $��Vegan:��6-   $��Center=�6>   �� 5/   
   -   5        $ VeganCheckbox��5/   0   $��4��6   $��1��6    $��Center=�6k   $��5,0,0,0q�6�    6G   �� *NewItem.Vegan+6�   �� 51   
   $��5��6   $��0��6   $��OrderedCount:��6-   $��Center=�6E   �� 52   
   $��5��6   $��1��6   $��5q�6/   $

 Wrap=�6:   $ 2��6N   $
 Auto=�6[   ��53      �� *NewItem.OrderedCount+�� 56   
   $��6��6   $��0��6   $��	Category:��6-   $��Center=�6A   �� 57   
   $��6��6   $��1��6    $��5,5,95,5q�60    �� SelectedValuePath$ Id��5:       �� DisplayMemberPath$
 Name��5;      ��58      �� *
Categories+ �� SelectedValue 59      �� *NewItem.CategoryId+5;      �� 5=   
   $��7��6   $��0��6   $��Cancel��6.   $
��Left=�5>      $��Center=�6,   $��73��5?      $��21��6   $��5q�6*    �� IsCancel$
 True��5@      ��5A      �� *CancelNewItemCommand+�� 5C   
   $��7��6   $��1��6   $
��Save��6.   $��Right=�5D      $��Center=�6-   $��73��5E      $��21��6   $��5q�6*    �� 	IsDefault$
 True��5F      ��5G      �� *SaveNewItemCommand+��5H      �� *	NewItem+5I      5J      