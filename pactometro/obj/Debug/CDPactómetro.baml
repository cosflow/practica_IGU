   M S B A M L   `   `   `  ����E  Apactometro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:pactometro
pactometro  5   	       pactometro.CDPactómetro   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:pactometro     5   	     � Title$  CDPactómetro��5   	   $	��450��6   $	��800��6+   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   ��  �� Orientation$ Vertical=�6   $��1��6,   $��0��69   $��Center=�6I   $��Center=�6f   $��5q�6�   .+�5      �� -   5         ��Name$ btn_AñadirResultado��5      $��Añadir Resultado��62   $��2q�6M   $��5q�6Y   {�.  6v   �� 5      -   5         ��Name$ resultadosAñadidos_Title��5      $��1��6:   $��Resultados añadidos:��6G   $��Center=�6c   $��5q�6~   5      �� 5   
   -   5        $ btn_EliminarResultado��5      $��Borrar Resultado��60   $��2q�6K   $��5q�6W   {�.  $��4��6t   6�   �� 5   
   -   5        $��tablaResultados��5   G   $��10q�6^   $��0��6k    �� View 5      �� 5      .P�5      �� $��PARTIDO��6%    �� Width$
 Auto��66    �� DisplayMemberBinding 6C   �� *	Partido+�� 5      $��Escaños��6%   $
 Auto��66    6C   �� *
Escaños+5      5       �� 5!   
   -   5        $��tablaResultadosAñadidos��5!   O   $��10q�6n   $��2��6{   $��0��6�    5"      �� 5#      .P�5$      �� $��PARTIDO��6%   $
 Auto��66    6C   �� *	Partido+�� 5%      $��Escaños��6%   $
 Auto��66    6C   �� *
Escaños+5&      5(      �� 5)   
   -   5        $ txt_Mayoría��5)      $��
Mayoría: ��6)   $��5q�6:   $��Center=�6E   $��Center=�6`   $��1��6}   $��1��6�   $��20(�6�   $
��Wrap=�6�   �� 5*   
   -   5        $ txt_ResultadosRestantes��5*      $��5q�65   $*��$Resultados restantes para mayoría: ��6@   $��Center=�6k   $��Center=�6�   $��1��6�   $��2��6�   $��20(�6�   $
��Wrap=�6�   5+      5,      