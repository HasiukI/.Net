   M S B A M L   `   `   `  ����B  >Notepad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:NotepadNotepad  5   	       Notepad.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Notepad     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � WindowStyle$
 None=�5
   	    � AllowsTransparency .  J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$	 New��5      6'     5   
   -   5        $
 Open��5      6(     5   
   -   5        $
 Save��5      6(     5   
   -   5        $ SaveAs��5      6*     5   
   -   5        $ Print��5      6)     5   
   $	 Cut��6     5   
   $ Paste��6     5   
   $
 Undo��6     5   
   -   5        $
 Find��5      6(     5   
   $ Replace��6     5   
   -   5        $ 	SelectAll��5      6-   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5#   
   �� -	   5        $��30��5#      $��0��6   ���#000a23._�5$      �  � ColumnDefinitions 5%      �� 5&      $��0.10* �6'   �� 5'      $��* �6'   �� 5(      $��0.20* �6'   .+�5-      �� $��5q�6   $��0��6$   $��40��65   ._�5.      �� $��images/Notepad.png��6   6:   5/      �� 50      -
   5        $��WindowPanel��50   F   $��1��6    �� Orientation$ 
Horizontal=�6-   6F   � 52      $��2��6    53      �� 54      �� 55      �� 56      .+�59      �� -   5        $��5 0 5 5q�59      $��0��6.   ._�5:      �� -   5         ��Name$ minwind��5:   +   $��5q�6    $��images/min.png��6<   6V   5;      �� 5<      -   5        $��5 0 5 5q�5<      $��1��6/   ._�5=      �� -   5        $ max_normwind��5=   +   $��5q�6    $��images/max_norm.png��6A   6`   5>      �� 5?      -   5        $��5 0 5 5q�5?      $��2��6/   ._�5@      �� -   5        $ 	closewind��5@   ,   $��5q�6!   $��images/close.png��6?   6[   5A      5B      5D      5E      �� 5I   
   -   5        $��1��5I      ���#1a1f2d._�5J      �  5K      �� 5L      $��0.12* �6'   �� 5M      $
��0.2* �6'   �� 5N      $
��0.2* �6'   �� 5O      .+�5R      �� -   5        $��5 0 0 1q�5R      $��0��6+   	 �� CornerRadius$	 5��6;   ._�5S      �� .+�5T      �� $��8q�6$   $��Center=�6/   $��Center=�6J   ���#ffffff$��14(�6|   $��Файл��6�   � 5U      -   5        
 �Name$
 	FilePopup��5U       $��Bottom=�63   $��Slide=�6F    � AllowsTransparency . .��5V      �� 
���   �$��1q�69   ._�5W   "   � ���#272727 5X   &   �� 5Y   *   �� 5Z   *   �� 5[   *   �� 5\   *   �� 5]   *   �� 5^   *   �� 5_   *   $
��auto �68   �� 5`   *   �� 5a   *   �� 5b   *   $
��auto �68   �� 5c   *   �� 5d   *   �� 5e   *   .+�5j   &   �� -   5        $��0��5j   .   $��3q�6;   $	 5��6�    5k   *     5l   .   -   5        $	 New��5l   =   6K   ._�5o   *   � $
��10 6q�6/    5p   .   �� 5q   2   �� 5r   2   $
��0.5* �6C   .+�5t   .   �� 
�������$��0��6K   $��0��6X   $��13(�6h   $
��Left=�6v   $��Center=�6�   $%��Створити вкладку��6�   �� 5u   .   ���#d2d2d2$��0��6M   $��1��6Z   $��12(�6j   $
��Left=�6y   $��Center=�6�   $��Ctrl+N��6�   5v   +   5w   '   �� 5y   &   -   5        $��1��5y   -   $��3q�6:   $	 5��6�   ._�5z   *   � $
��10 6q�6/    5{   .   �� 5|   2   �� 5}   2   $
��0.5* �6C   .+�5   .   �� 
�������$��0��6K   $��0��6X   $��13(�6h   $
��Left=�6v   $��Center=�6�   $!��Створити вікно��6�   �� 5�   .   ���#d2d2d2$��0��6M   $��1��6Z   $��12(�6j   $
��Left=�6y   $��Center=�6�   $��Ctrl+Shift+N��6�   5�   +   5�   '   �� 5�   &   -   5        $��2��5�   -   $��3q�6:   $	 5��6�    5�   *     5�   .   $
 Open��6=   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Відкрити��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+O��6�   5�   +   5�   '   �� 5�   &   -   5        $��3��5�   -   $��3q�6:   $	 5��6�    5�   *     5�   .   $
 Save��6=   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Зберегти��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+S��6�   5�   +   5�   '   �� 5�   &   -   5        $��4��5�   -   $��3q�6:   $	 5��6�    5�   *     5�   .   $ SaveAs��6=   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Зберегти як��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+Shift+S��6�   5�   +   5�   '   �� 5�   &   -   5        $��5��5�   -   $��3q�6:   $	 5��6�   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Зберегти все��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��
Ctrl+Alt+S��6�   5�   +   5�   '   � 5�   &   $��6��60   $��3q�6=   ���#3d3d3d � Points �   ���6Y   �� 5�   &   -   5        $��7��5�   -   $��3q�6:   $	 5��6�   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $)��#Параметри сторінки��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $�� ��6�   5�   +   5�   '   �� 5�   &   -   5        $��8��5�   -   $��3q�6:   $	 5��6�   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Друк��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+Р��6�   5�   +   5�   '   � 5�   &   $��9��60   $��3q�6=   ���#3d3d3d �   ���6Y   �� 5�   &   -   5        $��10��5�   -   $��3q�6;   $	 5��6�   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $#��Закрити вкладку��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+W��6�   5�   +   5�   '   �� 5�   &   -   5        $��11��5�   -   $��3q�6;   $	 5��6�   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Закрити вікно��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+Shift+W��6�   5�   +   5�   '   �� 5�   &   -   5        $��12��5�   -   $��3q�6;   $	 5��6�   ._�5�   *   � $
��10 6q�6/    5�   .   �� 5�   2   �� 5�   2   $
��0.5* �6C   .+�5�   .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��
Вийти��6�   �� 5�   .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $�� ��6�   5�   +   5�   '   5�   #   5�      5�      5�      5�      �� 5�      -    5        $	��3 0q�5�      $��1��6(   $	 5��68   ._�5�      �� .+�5�      �� $��8q�6$   $��Center=�6/   $��Center=�6J   ���#ffffff$��14(�6}   $��Редагувати��6�   � 5�      -!   5        $
 	EditPopup��5�       $��Bottom=�63   $��Slide=�6F    . .��5�      �� 
���   �$��1q�69   ._�5�   "   � ���#272727 5   &   �� 5  *   �� 5  *   $
��auto �68   �� 5  *   �� 5  *   �� 5  *   �� 5  *   �� 5  *   $
��auto �68   �� 5  *   �� 5	  *   �� 5
  *   �� 5  *   �� 5  *   $
��auto �68   �� 5  *   �� 5  *   �� 5  *   $
��auto �69   �� 5  *   .+�5  &   �� -"   5        $��0��5  -   $��3q�6:   $	 5��6�   ._�5  *   � $
��10 6q�6/    5  .   �� 5  2   �� 5  2   $
��0.5* �6C   .+�5  .   �� 
�������$��0��6K   $��0��6X   $��13(�6h   $
��Left=�6v   $��Center=�6�   $��Скасувати��6�   �� 5  .   ���#d2d2d2$��0��6M   $��1��6Z   $��12(�6j   $
��Left=�6y   $��Center=�6�   $��Ctrl+Z��6�   5  +   5   '   � 5"  &   $��1��60   $��3q�6=   ���#3d3d3d �   ���6Y   �� 5$  &   -#   5        $��2��5$  -   $��3q�6:   $	 5��6�   ._�5%  *   � $
��10 6q�6/    5&  .   �� 5'  2   �� 5(  2   $
��0.5* �6C   .+�5*  .   �� 
�������$��0��6K   $��0��6X   $��13(�6h   $
��Left=�6v   $��Center=�6�   $��Вирізати��6�   �� 5+  .   ���#d2d2d2$��0��6M   $��1��6Z   $��12(�6j   $
��Left=�6y   $��Center=�6�   $��Ctrl+X��6�   5,  +   5-  '   �� 5/  &   -$   5        $��3��5/  -   $��3q�6:   $	 5��6�   ._�50  *   � $
��10 6q�6/    51  .   �� 52  2   �� 53  2   $
��0.5* �6C   .+�55  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Копіювати��6�   �� 56  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+C��6�   57  +   58  '   �� 5:  &   -%   5        $��4��5:  -   $��3q�6:   $	 5��6�   ._�5;  *   � $
��10 6q�6/    5<  .   �� 5=  2   �� 5>  2   $
��0.5* �6C   .+�5@  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Вставити��6�   �� 5A  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+V��6�   5B  +   5C  '   �� 5E  &   -&   5        $��5��5E  -   $��3q�6:   $	 5��6�   ._�5F  *   � $
��10 6q�6/    5G  .   �� 5H  2   �� 5I  2   $
��0.5* �6C   .+�5K  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Видалити��6�   �� 5L  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $	��Del��6�   5M  +   5N  '   � 5P  &   $��6��60   $��3q�6=   ���#3d3d3d �   ���6Y   �� 5R  &   -'   5        $��7��5R  -   $��3q�6:   $	 5��6�   ._�5S  *   � $
��10 6q�6/    5T  .   �� 5U  2   �� 5V  2   $
��0.5* �6C   .+�5X  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��
Пошук��6�   �� 5Y  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+F��6�   5Z  +   5[  '   �� 5]  &   -(   5        $��8��5]  -   $��3q�6:   $	 5��6�   ._�5^  *   � $
��10 6q�6/    5_  .   �� 5`  2   �� 5a  2   $
��0.5* �6C   .+�5c  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Знайти далі��6�   �� 5d  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��F3��6�   5e  +   5f  '   �� 5h  &   -)   5        $��9��5h  -   $��3q�6:   $	 5��6�   ._�5i  *   � $
��10 6q�6/    5j  .   �� 5k  2   �� 5l  2   $
��0.5* �6C   .+�5n  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $%��Знайти попереднє��6�   �� 5o  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Shist+F3��6�   5p  +   5q  '   �� 5s  &   -*   5        $��10��5s  -   $��3q�6;   $	 5��6�   ._�5t  *   � $
��10 6q�6/    5u  .   �� 5v  2   �� 5w  2   $
��0.5* �6C   .+�5y  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Замінити��6�   �� 5z  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+H��6�   5{  +   5|  '   �� 5~  &   -+   5        $��11��5~  -   $��3q�6;   $	 5��6�   ._�5  *   � $
��10 6q�6/    5�  .   �� 5�  2   �� 5�  2   $
��0.5* �6C   .+�5�  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Перейти��6�   �� 5�  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+G��6�   5�  +   5�  '   � 5�  &   $��12��60   $��3q�6>   ���#3d3d3d �   ���6Z   �� 5�  &   -,   5        $��13��5�  -   $��3q�6;   $	 5��6�   ._�5�  *   � $
��10 6q�6/    5�  .   �� 5�  2   �� 5�  2   $
��0.5* �6C   .+�5�  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Вибрати все��6�   �� 5�  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��Ctrl+A��6�   5�  +   5�  '   �� 5�  &   --   5        $��14��5�  -   $��3q�6;   $	 5��6�   ._�5�  *   � $
��10 6q�6/    5�  .   �� 5�  2   �� 5�  2   $
��0.5* �6C   .+�5�  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��Дата й час��6�   �� 5�  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $��F5��6�   5�  +   5�  '   � 5�  &   $��15��60   $��3q�6>   ���#3d3d3d �   ���6Z   �� 5�  &   -.   5        $��16��5�  -   $��3q�6;   $	 5��6�   ._�5�  *   � $
��10 6q�6/    5�  .   �� 5�  2   �� 5�  2   $
��0.5* �6C   .+�5�  .   �� $��0 0 0 0q�68   
�������$��0��6\   $��0��6i   $��13(�6y   $
��Left=�6�   $��Center=�6�   $��
Шрифт��6�   �� 5�  .   $��0 0 0 0q�68   ���#d2d2d2$��0��6^   $��1��6k   $��12(�6{   $
��Left=�6�   $��Center=�6�   $�� ��6�   5�  +   5�  '   5�  #   5�     5�     5�     5�     �� 5�     -/   5        $	��3 0q�5�     $��2��6(   $	 5��68   ._�5�     �� .+�5�     �� $��8q�6$   $��Center=�6/   $��Center=�6J   ���#ffffff$��14(�6}   $��Переглянути��6�   � 5�     -0   5        $
 SeePopup��5�      $��Bottom=�62   $��Slide=�6E    . .��5�     �� 
���   �$��1q�69   ._�5�  "   � ���#272727 5�  &   �� 5�  *   �� 5�  *   �� 5�  *   .+�5�  &   �� -1   5        $��0��5�  -   $��3q�6:   $	 5��6�   ._�5�  *   � $
��10 6q�6/   $	��200��6=    5�  .   �� 5�  2   $
��0.2* �6C   �� 5�  2   �� 5�  2   $
��0.5* �6C   .+�5�  .   �� 
�������$��0��6K   $��1��6X   $��13(�6h   $
��Left=�6v   $��Center=�6�   $��Масштаб��6�   �� 5�  .   ���#d2d2d2$��0��6M   $��2��6Z   $��12(�6j   $��Right=�6y   $��Center=�6�   $��>��6�   � 5�  .   -2   5        $
 	SizePopup��5�  4   $��Right=�6G   $��Slide=�6Y    . .��5�  2   �� 
���   �$��1q�6M   ._�5�  6   � ���#272727 5�  :   �� 5�  >   �� 5�  >   �� 5�  >   .+�5�  :   �� -3   5        $��0��5�  A   $��3q�6N   $	 5��6�   ._�5�  >   � $
��10 6q�6C    5�  B   �� 5�  F   �� 5�  F   $
��0.2* �6W   .+�5�  B   �� 
�������$��0��6_   $��0��6l   $��13(�6|   $
��Left=�6�   $��Center=�6�   $��Збільшити��6�   �� 5�  B   ���#d2d2d2$��0��6a   $��1��6n   $��12(�6~   $
��Left=�6�   $��Center=�6�   $��Ctrl+плюс��6�   5�  ?   5�  ;   �� 5�  :   -4   5        $��1��5�  A   $��3q�6N   $	 5��6�   ._�5�  >   � $
��10 6q�6C    5�  B   �� 5�  F   �� 5�  F   $
��0.2* �6W   .+�5�  B   �� 
�������$��0��6_   $��0��6l   $��13(�6|   $
��Left=�6�   $��Center=�6�   $��Зменшити��6�   �� 5�  B   ���#d2d2d2$��0��6a   $��1��6n   $��12(�6~   $
��Left=�6�   $��Center=�6�   $��Ctrl+мінус��6�   5�  ?   5�  ;   �� 5�  :   -5   5        $��2��5�  A   $��3q�6N   $	 5��6�   ._�5�  >   � $
��10 6q�6C   $	��300��6Q    5�  B   �� 5�  F   �� 5�  F   $
��0.2* �6W   .+�5�  B   �� 
�������$��0��6_   $��0��6l   $��13(�6|   $
��Left=�6�   $��Center=�6�   $G��AВідновити масштаб за замовчуванням��6�   �� 5�  B   ���#d2d2d2$��0��6a   $��1��6n   $��12(�6~   $
��Left=�6�   $��Center=�6�   $��Ctrl+0��6�   5�  ?   5�  ;   5�  7   5�  3   5�  /   5�  +   5�  '   �� 5�  &   -6   5        $��1��5�  -   $��3q�6:   $	 5��6�   ._�5�  *   � $
��10 6q�6/    5�  .   �� 5�  2   $
��0.2* �6C   �� 5�  2   �� 5�  2   $
��0.5* �6C   .+�5  .   �� $��10��64   $��0��6?   $��images/tick.png��6O   �� 5  .   
�������$��0��6K   $��1��6X   $��13(�6h   $
��Left=�6v   $��Center=�6�   $��Рядок Стану��6�   5  +   5  '   �� 5  &   -7   5        $��2��5  -   $��3q�6:   $	 5��6�   ._�5  *   � $
��10 6q�6/    5	  .   �� 5
  2   $
��0.2* �6C   �� 5  2   �� 5  2   $
��0.5* �6C   .+�5  .   �� $��10��65   $��0��6@   $��images/tick.png��6P   �� 5  .   
�������$��0��6K   $��1��6X   $��13(�6h   $
��Left=�6v   $��Center=�6�   $&�� Перенос по словах��6�   5  +   5  '   5  #   5     5     5     5     � 5     -8   5        $
 poshukpopup��5  \   $��Bottom=�6   $��Slide=�6+    . .��5     �� 
���   �$��1q�61   ._�5     � $	��300��6   $��35��6+   ���#272727 5     �� 5  "   �� 5  "   $
��0.2* �63   �� 5   "   $
��0.2* �63   .+�5#     �� -9   5         ��Name$ poshuktextbox��5#  &   $��5q�6=   $��0��6H   ���#272727
�������$��13(�6�   6�   �� 5$     -:   5        $��5q�5$  %   $��1��60   ._�5%  "   �� $��5 0 5 0q�6-   
�������$��13(�6Q   $��Center=�6`   $��Center=�6{   x6�   5&     �� 5'     -;   5        $��5q�5'  %   $��2��60   ._�5(  "   �� $��5 0 5 0q�6-   
�������$��13(�6Q   $��Center=�6`   $��Center=�6{   ok6�   5)     5*     5+     5-     5.     5/     �� 53  
   -<   5        $ Notepad��53  -   $��10q�6   $��14(�6   $��2��6>    �� AcceptsReturn .  �� TextWrapping$
 Wrap=�6`   ���#272727
�������6�   �� 57  
   -=   5         ��Name$ BottomPanel��57     $��3��6&   ���#1a1f2d._�58     �  59     �� 5:     $��* �6'   �� 5;     $
��auto �6'   �� 5<     $
��0.5* �6'   �� 5=     $
��auto �6'   �� 5>     $
��0.5* �6'   �� 5?     $
��auto �6'   �� 5@     $
��0.5* �6'   .+�5C     � $��0��6    5D     �� 5E     $
��0.3* �6,   �� 5F     $
��0.2* �6,   �� 5G     $��0.35* �6,   �� 5H     .+�5K     �� $��0��6    
�������$��
Рядок��6G   �� 5L     ->   5         ��Name$ ryadok��5L      
�������$��1��6G   $��0,��6W   �� 5M     $��2��6    
�������$��стовбець��6G   �� 5N     -?   5        $ stovb��5N      
�������$��3��6F   $��0��6W   5O     � 5Q     $��1��6   $��3q�6,   ���#3d3d3d �    -16H   �� 5R     -@   5        $ Mashtabtext��5R     $��2��61   
�������$
��100%��6Y   � 5T     $��3��6   $��3q�6,   ���#3d3d3d �    -16H   �� 5U     $��4��6   
�������$��Windows(CRLF)��6D   � 5W     $��5��6   $��3q�6,   ���#3d3d3d �    -16H   �� 5X     $��6��6   
�������$��UTF-8��6D   5Z     5[     5_     5`     