   M S B A M L   `   `   `  ����E  Apactometro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:pactometro
pactometro  5   	       pactometro.CDTablas   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:pactometro     5   	     � Title$  CDTablas��5   	   $	��250��6   $	��600��6'   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      $
��Auto �6   .+�5   
   �� -   5        $��tablaElecciones��5   G   $��10q�6^   $��0��5      $��4��5       �� View 5      �� 5      .P�5      �� $��ELECCION��6%    �� Width$
 Auto��67    �� DisplayMemberBinding 5      �� *	Título+�� 5      $��FECHA��6%   $
 Auto��64    5      �� *Fecha+�� 5      $��Número de Escaños��6%   $
 Auto��6@    5      �� *TotalEscaños+�� 5      $��Mayoría absoluta��6%   $
 Auto��6?    5      �� *
Mayoría+5      5      �� 5!   
   -   5        $��tablaResultados��5!      $��10q�6*   $��1��5"      $��4��5#       5$      �� 5%      .P�5&      �� $��PARTIDO��6%   $
 Auto��66    5'      �� *	Partido+�� 5(      $��Escaños��6%   $
 Auto��66    5)      �� *
Escaños+5*      5,      �� 5.   
    �� Orientation$ 
Horizontal=�6   $��2��6.   .+�5/      �� -   5         ��Name$ btn_C_eleccion��5/   -   $��2q�6   $��10q�6!   $��Añadir elección��6E   $��Center=�6_   6z   �� 50      -   5        $ btn_U_eleccion��50   -   $��2q�6   $��10q�6!   $��Modificar elección��6E   $
��Auto��6b   {�.  6�   51      52      53      