   M S B A M L   `   `   `  ����T  PLibrary Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Library_Management_SystemLibrary_Management_System  5   	   *    $Library_Management_System.DeleteBook   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Library_Management_System     5   	   -   5          � Title$  
DeleteBook��5   	   $	��749��6   $
��1000��6)   $
��1000��6L   $	��749��6\   $
��1000��6l   $	��749��6|   ��66   Z� .��5	      � ���	#FF284E70.+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5
      $	��600��6P   $��	0,0,400,0q�6\   6o   �� 5   
   -   5         ��Name$ DeleteButton��5      $��Kitabı Sil��6'   $
��Left=�6<   $��674,372,0,0q�6W   $	��Top=�6l   $	��216��6�   $��36(�6�   $��Roboto Mono Medium)�6�   ���	#FF284E70 K� ZIndex$ 2��6�    � Cursor$
 Hand��6  $��57��6!  ��6�   Z� ��6�   Z� ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5       �� 
TargetType" ��6.   .R�5   "   �� #��z��6)   ._�5   &   �� $��Center=�67   $��Center=�6T   5   #   	 �� Triggers	 5   "   P� 5   &   
 P� Property
 �@J�IsMouseOver P� Value . . �5   *   ��  � ��
 ���� 6G   5   '   5      5      5      5      �� 5   
   -   5         ��Name$ r_SearchButton��5      $
��Left=�6,   $��67��6G   $��660,367,0,0q�6S   $	��Top=�6h   $	��245��6�   ���	#FFE27882 �� RadiusX$ 15��6�    �� RadiusY$  15                        ��6�   $
 Hand��6�   �� 5    
   -   5         ��Name$ SearchTextBox��5        �� TextWrapping$
 Wrap=�6)   $	��294��6=   $��Roboto Mono)�6I   $��36(�6b   
������ ���	#00ABADB3���	#FF284E70$
��Left=�6�   $	��Top=�6�    �� TextAlignment$ Center=�6�    �� 	MaxLength$ 15��6  $ 2��6  $��635,277,0,0q�6"   �� MinLines$ 1��67  .��6l  ��5!      �� 5"      " ��6"   .R�5#      �� #��z��6   #��z��5$      #��z��5%      ._�5&      ��  ��Name$ PART_ContentHost��6'   5'      5(      .��5)       5*      �� 5+   
   -   5        $ r_searchtextbox��5+      ���	#FF444444$	��294��6@   $ 15��6L   $ 15��6Y   $
��Left=�6f   $	��Top=�6�   $��51��6�    J� IsHitTestVisible .  
�������$��635,275,0,0q�6�   �� 5,   
   -   5         ��Name$ lb_tb_bookname��5,      $��
Kitap Adı��6(   $	��Top=�6<   $��36(�6T   ���	#FF828282$��Roboto Mono Light)�6y    .  $ 1��6�   $��681,272,0,0q�6�   $
��Left=�6�   5.      5/      