   M S B A M L   `   `   `  ����  WpfAppNeeeeeee'    !WpfAppNeeeeeee.ScheduleEditWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #Событие расписания��5   	   $	��350��6$   $	��410��61    � WindowStartupLocation$ CenterOwner=�6=    � 
ResizeMode$ NoResize=�6a   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Описание:��6   $��0��6%   �� 5   
   -   5         ��Name$ DescriptionTextBox��5      $��0��6.   $��	100,0,0,5q�6;   $	��250��6N   �� 5   
   $��Дата и время:��6   $��1��6)   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
DatePicker��5      $��1��6)   $��	100,0,0,5q�66   $	��120��6I   �� 5   
   -   5        $ TimeTextBox��5      $��1��6'   $��	230,0,0,5q�64   $��60��6G    � ToolTip$ 	ЧЧ:ММ��6R   �� 5   
   $��Место:��6   $��2��6"   �� 5   
   -   5        $ LocationTextBox��5      $��2��6+   $��	100,0,0,5q�68   $	��250��6K   �� 5   
   $!��Преподаватель:��6   $��3��6*   �� 5   
   -   5         ��Name$ TeacherComboBox��5      $��3��6,   $��	100,0,0,5q�69   $	��250��6L    �� DisplayMemberPath$
 Name��6X   	 �� SelectedValuePath$	 ID��6q   �� 5   
   $��5��6   
 �� Orientation$
 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�6W   .+�5      �� -   5         ��Name$ 
SaveButton��5      $��Сохранить��6)   $	��100��6=   $��0,0,10,0q�6I   6[   �� 5      -   5        $ CancelButton��5      $��Отмена��6+   $��80��6<   6G   5      5      5       