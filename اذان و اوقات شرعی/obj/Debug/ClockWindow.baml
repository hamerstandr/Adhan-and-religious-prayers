   M S B A M L   `   `   `  ����Z  Vاذان و اوقات شرعی, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:اذان_و_اوقات_شرعیاذان_و_اوقات_شرعی  5   	   1    +اذان_و_اوقات_شرعی.ClockWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:اذان_و_اوقات_شرعی     5   	     � Title$  Clock��5   	   $��2q�6   $	��327��6"   $	��311��6/    � AllowsTransparency .  � WindowStyle$
 None=�5
   	   
������  � WindowStartupLocation$ CenterScreen=�65    � Icon$ Resources/clock.ico��5   	    � 
ResizeMode$ NoResize=�5   	    � Topmost .   J� Opacity$ 1Q�6/    � ShowInTaskbar .  .��5      � -   5        $	��300��5      $	��300��6   .+�5   
   �� $��Resources/backGround.png��6   �� 5   
   -   5        $��christianityCalendar��5      
�������$��0, 0, 0, 52q�5   	   $��Center=�6   $��Bottom=�5   	   $
��Auto��6$   $
��Auto��5   
   .��6   Value5      �� 5   
   -   5        $��persianCalendar��5      
�������$��Bottom=�5   	   $
��Auto��6$   $
��Auto��5   	   $��Center=�6   $��	0,0,0,196q�63   .��6F   Value5       �� 5#   
   -   5        $��rectangleSecond��5#   +   $��150,0,149,150q�6   
���  ��$	��120��5%      $��Bottom=�6#   z�5&      �� 5'      -   5        
secondHand��	 �� CenterX$	 0Q�5'   6   
 �� CenterY$	
 120Q�5)       �� Angle$ 0Q�6   5+      �� 5/   
   -   5        $��rectangleMinute��5/   ,   $��150,49,149,151q�6   
������z�52      �� 53      -   5        
minuteHand��$	 0Q�53   6   $	
 100Q�55      $ 0Q�6   57      �� 5T   
   -   5        $��rectangleHour��5T   ,   $��150,80,149,150q�6   
�������z�5W      �� 5X      -	   5        hourHand��$	 0Q�5X   4   $
 70Q�5Z      $ 0Q�6   5\      5_      5`      