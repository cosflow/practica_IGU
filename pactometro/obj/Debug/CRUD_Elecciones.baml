   M S B A M L   `   `   `  ����E  Apactometro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:pactometro
pactometro  5   	        pactometro.CRUD_Elecciones   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:pactometro     5   	     � Title$  CRUD_Elecciones��5   	   $	��400��6!   $	��450��6.   .��5	      �� .��5
   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� 
���   �$��1q�6)   $��5q�6=   $��2��6H   ._�5      �� -   5         ��Name$ txt_Instrucciones��5       �� TextAlignment$ Justify=�67   $��5q�6P    �� Padding$ 5q�6[   
�������$
��Wrap=�6z   5      �� 5      
����p��$��1q�60   $��5q�6D   $��1��6O   ._�5      ��  �� Orientation$ Vertical=�6   $��5q�64   .+�5      �� $��5q�6    $��	Resultado��6+   6>   �� 5      
���  ��$��1q�6/   $��5q�6C   $��1��6N   ._�5      �� $ 
Horizontal=�6%   $��5q�6>   .+�5      �� -   5        $ txt_Resultado_Partido��5   3   $��2q�6(   $��Partido:��6R   $��Center=�6b   $��Center=�6}   �� 5       -   5         ��Name$ txtBox_Partido��5    &   $��2q�6>   $��1��6I   $��1��6V   $	��100��6f   5!      5"      �� 5$      
��� � �$��1q�61   $��5q�6E   $��2��6P   ._�5%      �� $ 
Horizontal=�6%   $��5q�6>   .+�5&      �� -   5        $ txt_Resultado_Escaños��5&   3   $��2q�6(   $��	Escaños:��6R   $��Center=�6b   $��Center=�6}   �� 5'      -   5        $ txtBox_Escaños��5'   &   $��2q�6>   $��2��6I   $��1��6V   $	��100��6f   5(      5)      �� 5*      -   5         ��Name$ btn_AñadePartido��5*   (   $��5q�6   $��Guardar resultado��6B   $��Center=�6^   $��Stretch=�6{   $��4q�6�   $��3��6�   6�   5+      5,      �� 5.      
���� ��$��1q�6*   $��5q�6>   $��4��6I   ._�5/      �� $ 
Horizontal=�6   $��5q�66   .+�50      �� -   5        $ txt_Tipo��50   8   $��2q�6    $��4��6+   $$��Tipo (Generales/Autonómicas):��6J   $��Center=�6o   �� 51      -   5        $ txtBox_Tipo��51      $��2q�63   $��4��6>   $��1��6K   $	��100��6[   52      53      �� 55      
�����@�$��1q�6-   $��5q�6A   $��5��6L   ._�56      �� $ 
Horizontal=�6   $��5q�66   .+�57      �� -	   5        $ 	txt_Fecha��57   9   $��2q�6    $��5��6,   $��Fecha (DD/MM/AAAA):��6L   $��Center=�6g   �� 58      -
   5        $ txtBox_Fecha��58      $��2q�64   $��5��6?   $��1��6L   $	��100��6\   59      5:      �� 5<      
��� ���$��1q�6*   $��5q�6>   $��6��6I   ._�5=      �� $ 
Horizontal=�6   $��5q�66   .+�5>      �� -   5        $ txt_Parlamento��5>   9   $��2q�6    $��6��6,   $;��5Parlamento (Cortes Generales/Parlamento autonómico):��6Q   $��Center=�6�   �� 5?      -   5        $ txtBox_Parlamento��5?      $��2q�69   $��6��6D   $��1��6Q   $��90��6a   5@      5A      �� 5B      $ 
Horizontal=�6   $��7��62   $��10q�6?   $��Center=�6K   .+�5C      �� -   5        $ btn_Guardar��5C      $��Guardar y salir��6.   $��Center=�6I   $��10q�6f   $��2q�6r    �� 	IsDefault . 6�   5D      5E      5F      5G      