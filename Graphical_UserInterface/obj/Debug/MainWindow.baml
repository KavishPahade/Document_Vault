   M S B A M L   `   `   `  ����  Graphical_User_Interface    DocumentVault.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��550��6   $��866.791��6)   .��5      � $��	10,20,0,0q�6    � ColumnDefinitions 5   
   �� 5      $	��64* �6   �� 5      $	��54* �6   .+�5
   
   �� -   5         ��Name$ Document_Vault��5
      $
��Left=�6-   $	��400��6H   $��150,60,50,50q�6U   $	��Top=�6k   $	��500��6�   $��3��6�   .@�5      �� -   5         ��Name$ Search_View��5      $��Search View��6+   $
��-2,0q�6@   .��5      � ���	#FFE5E5E5$��
0,-8,0,-37q�6.   $	��700��6B    5      �� 5      $	��50* �6+   �� 5      $	��99* �6+   �� 5      $	��12* �6+   �� 5      $	��20* �6+   �� 5      $	��27* �6+   �� 5      $	��18* �6+   �� 5      $��0* �6+   �� 5      $	��34* �6+   �� 5      $	��25* �6+   �� 5      $
��178* �6+   �� 5      $
��237* �6+   .+�5      �� -   5         ��Name$ Keyword_Label��5      $��Enter Keyword��63   $
��Left=�6K   $��	10,80,0,0q�6f   $	��Top=�6y   $��27��6�   $	��111��6�   $��2��6�    J� RenderTransformOrigin$ -0.03,4.333&�6�   �� 5      -   5        $ Category_Label��5      $��Select Categories��64   $
��Left=�6P   $��26,129,0,257q�6k   $��Center=�6�   $��27��6�   $	��101��6�   $��2��6�   �� 5      -   5         ��Name$ Keyword_Textbox��5      $��4��67   $
��Left=�6G   $��24��6b   $��9,89,0,0q�6n    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��153��6�   $ 0.5,0.5&�6�   $��6��6�   .��6  z�5      V� 5      .~�5    "   �� �� 5!   "   �� 5"   "    �� Angle$ -0.894Q�62   T� 5#   "   5$      .��5%   3   5&      	� 5'      -   5        	 	�Name$	 Partial_Radio��5'   "   $��Partial Search��69   $
��Left=�6R   $��	40,60,0,0q�6m   $	��Top=�6�   $ 
0.516,-0.2&�6�   $��1��6�   $��3��6�   	� 5(      -   5        $	 
Full_Radio��5(   "   $��Full Search��66   $��7��6L   $
��Left=�6\   $��0,60,0,0q�6w   $	��Top=�6�   $��3��6�   �� 5)      -   5        
 ��Name$
 Search��5)      $��Search��6-   $��Right=�6>   $��	0,281,9,0q�6Z   $	��Top=�6m   $	��129��6�   $��27��6�   $��7��6�   $ 0.465,0.963&�6�   $��1��6�   �� 5*      -	   5         ��Name$ Categories_Listbox��5*       �� SelectionMode$ Multiple=�6:   $��1��6S   $
��Left=�6c   $	��100��6~   $��	0,0,0,159q�6�   $��Bottom=�6�   $	��100��6�   $��2��6�   ]� 5+      $��9��6    $��1��64   $
��Left=�6D   $��29��6_   ��.  $��	19,10,0,0q�6�   $	��Top=�6�   $	��294��6�   .+�5,      	� -
   5        $	 
Text_Radio��5,   '   $��Text Search��6;   $
��Left=�6Q   $��	24,16,0,0q�6l   $	��Top=�6   $��32��6�   $	��120��6�   $��6��6�   $��1��6�   6�   	� 5-      -   5        $	 
Meta_Radio��5-   '   $��Metadata Search��6;   $
��Left=�6U   $��	10,16,0,0q�6p   $	��Top=�6�   $��40��6�   $	��110��6�   $��2��6�   $��6��6�   6�   5.      �� 5/      $+��%Files Containing the entered keywords��6   $��4��6L   $
��Left=�6\   $��
12,134,0,0q�6w   $	��Top=�6�   $ 0.5,0.5&�6�   $��6��6�   $	��219��6�   z�50      V� 51      .~�52   "   �� �� 53   "   �� 54   "   $ -0.447Q�62   T� 55   "   56      58      �� 59      -   5        $ results��59      $��4��6/   $
��Left=�6?   $	��100��6Z   $��
10,154,0,0q�6g   $	��Top=�6{   $	��211��6�   $��6��6�   5;      5<      �� 5=      -   5        $ Information_View��5=      $��Information View��60   .��5>      � ���	#FFE5E5E5$��0,-9,-206,-39q�6.   $	��400��6E   $	��700��6R    5?      �� 5@      $��23 �6+   �� 5A      $
��136* �6+   �� 5B      $	��48* �6+   �� 5C      $	��26* �6+   �� 5D      $
��467* �6+   .+�5F      �� -   5        $ Child��5F      $��4��6-   $
��Left=�6=   $	��100��6X   $��
35,245,0,0q�6e   $	��Top=�6y   $��98��6�   6�   �� 5G      -   5        $ Parents��5G      $��1��6/   $
��Left=�6?   $	��100��6Z   $��
53,245,0,0q�6g   $	��Top=�6{   $	��100��6�   $��2��6�   6�   �� 5H      $��Current File��6   $
��Left=�63   $��16,5,0,0q�6N   $	��Top=�6`   $��2��6x   $ 0.5,0.5&�6�   z�5I      V� 5J      .~�5K   "   �� �� 5L   "   �� 5M   "   $	 0.7Q�62   T� 5N   "   5O      5Q      �� 5R      $��File Contents��6   $��1��64   $
��Left=�6D   $��	19,38,0,0q�6_   $	��Top=�6r   $��86��6�   �� 5S      $��Metadata Contents��6   $��4��68   $
��Left=�6H   $��	23,38,0,0q�6c   $	��Top=�6v   $ 0.579,-0.346&�6�   $	��120��6�   �� 5T      $��Parents��6   $��1��6.   $
��Left=�6>   $��
53,224,0,0q�6Y   $	��Top=�6m   $��30��6�   $��52��6�   �� 5U      $��Children��6   $��4��6/   $
��Left=�6?   $��
35,224,0,0q�6Z   $	��Top=�6n   $ -0.342,0.115&�6�   �� 5X      $��4��6#   $
��Left=�63   $	��143��6N   $��	23,64,0,0q�6[   $	��Top=�6n   $	��188��6�   .��5Y      �� -   5         ��Name$ Metadata_Content��5Y   &   ��. $��Visible=�6e    �� 
IsReadOnly .  �� IsReadOnlyCaretVisible . $��1��6�   $
��Left=�6�   $	��140��6�   $	��Top=�6   $	��167��6  . �5Z      /� .T�5[   "   J� .*�6,   .*�5\   &   �� $� ��6*   5]   #   5^      5_      5`      �� 5a      $��1��6#   $
��Left=�63   $	��149��6N   $��	19,60,0,0q�6[   $	��Top=�6n   $	��134��6�   $��2��6�   �� 5b      -   5        $" InfoView_CurrentFile_Textbox��5b      $��3��6D   $��1��6X   $
��Left=�6h   $��23��6�   $��	84,10,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   �� 5c      -   5        $ TextBox��5c   "    . ��. $
��Auto=�6j   $
��Left=�6�   $	��147��6�   $	��Top=�6�   $	��185��6�   $��3��6�   $��1��6�   $��	19,60,0,0q�6
  . �5d      /� .T�5e      J� 5f      5g      5i      5j      �� 5k      -   5        $ Category_Tab��5k      $��Category View��6,   $��24��6C   $	��Top=�6O   .��5l      � ���	#FFE5E5E5$��0,-9,0,9q�6.   $	��800��6@    5m      �� 5n      $	��19* �6+   �� 5o      $	��19* �6+   �� 5p      $	��21* �6+   �� 5q      $	��30* �6+   �� 5r      $	��12* �6+   �� 5s      $��9* �6+   �� 5t      $	��20* �6+   �� 5u      $	��35* �6+   �� 5v      $	��37* �6+   �� 5w      $	��38* �6+   �� 5x      $	��12* �6+   �� 5y      $	��24* �6+   �� 5z      $	��21* �6+   �� 5{      $
��423* �6+   �� 5|      $	��80* �6+   .+�5~      �� $��
Categories��6   $��5��61   $
��Left=�6A   $��3,48,0,0q�6\   $	��Top=�6n   $��4��6�   $��77��6�   �� 5      $��Files��6   $��13��6,   $
��Left=�6=   $��5,48,0,0q�6X   $	��Top=�6j   $��70��6�   �� 5�      -   5        $ Category_Listbox��5�      $ Single=�68   $��4��6O   $��6��6c   $
��Left=�6s   $	��141��6�   $��1,79,0,0q�6�   $	��Top=�6�   $	��119��6�   6�   �� 5�      -   5        $ CatFile_Listbox��5�      $ Multiple=�67   $��12��6P   $
��Left=�6a   $	��141��6|   $��9,79,0,0q�6�   $	��Top=�6�   $	��133��6�   $��2��6�   5�      5�      �� 5�      $��Upload��6   $��24��6&   $��Bottom=�62   $
��-2,0q�6M   .��5�      � ���	#FFE5E5E5$��0,-4,0,4q�6.    5�      �� 5�      $	��23* �6+   �� 5�      $	��26* �6+   �� 5�      $
��314* �6+   �� 5�      $
��131* �6+   .+�5�      �� $��Selected File for ��6   $��1��69   $
��Left=�6I   $��7,7,0,0q�6d   $	��Top=�6u   $ 0.446,0.965&�6�   $��79��6�   $��2��6�   z�5�      V� 5�      .~�5�   "   �� �� 5�   "   �� 5�   "   $ 1.287Q�62   T� 5�   "    T� X$ -0.269Q�65    T� Y$ -0.097Q�6@   5�      5�      �� 5�      -   5        $��Browse��5�      $��2��6.   $
��Left=�6>   $��
233,10,0,0q�6Y   $	��Top=�6m   $	��105��6�   $ 	1.6,-0.85&�6�   $��2��6�   �� 5�      $��Description Text��6   $��2��67   $
��Left=�6G   $��
10,156,0,0q�6b   $	��Top=�6v   $	��113��6�   �� 5�      $��Child Dependencies��6   $��2��69   $
��Left=�6I   $��	0,195,0,0q�6d   $	��Top=�6w   $ -0.079,-0.308&�6�   �� 5�      $��Keywords��6   $��2��6/   $
��Left=�6?   $��
10,242,0,0q�6Z   $	��Top=�6n   $ -0.105,-0.538&�6�   �� 5�      -   5        $ childs��5�      $��2��6.   $
��Left=�6>   $��23��6Y   $��144,195,0,0q�6e   $
 Wrap=�6z   $�� ��6�   $	��Top=�6�   $	��139��6�   �� 5�      -   5        $ keywords��5�      $��2��60   $
��Left=�6@   $��23��6[   $��144,245,0,0q�6g   $
 Wrap=�6|   $�� ��6�   $	��Top=�6�   $	��139��6�   $ -0.033,-0.391&�6�   �� 5�      -   5        $ File_Textbox��5�      $��2��64   $
��Left=�6D   $��48��6_   $��	65,10,0,0q�6k   $
 Wrap=�6~   $�� ��6�   $	��Top=�6�   $	��136��6�   �� 5�      -   5        $
 Upload��5�      $��Upload��6-   $��2��6>   $
��Left=�6N   $��144,283,0,0q�6i   $	��Top=�6~   $��97��6�   $��33��6�   6�   �� 5�      -   5         ��Name$ dependency_status��5�       $��2��6<   $
��Left=�6L   $��30��6g   $��	0,321,0,0q�6s   $	��Top=�6�   $	��377��6�   $��2��6�   .@�5�      �� $��0,1,0,0q�6,   .��5�   "   �� -   5        $��
dependency��5�   ,   5�      5�      �� 5�      -   5        $ status_upload��5�       $��2��68   $
��Left=�6H   $��30��6c   $��	0,321,0,0q�6o   $	��Top=�6�   $	��377��6�   $��2��6�   .@�5�      �� $��0,1,0,0q�6(   .��5�      �� -   5        $��status��5�   (   5�      5�      �� 5�      $8��2(Comma Separated filenames dependent on this file)��6   $��2��6Y   $
��Left=�6i   $��	3,213,0,0q�6�   $	��Top=�6�   $	��315��6�   $ 0.5,0.5&�6�   $��10(�6�   $��2��6�   z�5�      V� 5�      .~�5�   "   �� �� 5�   "   �� 5�   "   $ -0.235Q�62   T� 5�   "   5�      5�      �� 5�      $��Category��6   $��1��6/   $
��Left=�6?   $��	10,63,0,0q�6Z   $	��Top=�6m   $��78��6�   $��2��6�   �� 5�      -   5        $ category_list��5�      $ Multiple=�65   $��2��6N   $
��Left=�6^   $��79��6y   $��	69,72,0,0q�6�   $	��Top=�6�   $��65��6�   �� 5�      $ ��(Comma Separated keywords)��6   $��2��6A   $
��Left=�6Q   $��	0,260,0,0q�6l   $	��Top=�6   $	��189��6�   $��32��6�   $��10(�6�   �� 5�      -    5        $ description��5�      $��2��63   $
��Left=�6C   $��23��6^   $��144,159,0,0q�6j   $
 Wrap=�6   $�� ��6�   $	��Top=�6�   $	��139��6�   6�   �� 5�      -!   5        $��Check_Dependencies��5�      $��3��6:   $
��Left=�6J   $��
11,195,0,0q�6e   $	��Top=�6y   $	��110��6�   $ 
0.147,-0.5&�6�   $��26��6�   6�   5�      5�      �� 5�      -"   5        $ MyTab��5�      $��Metadata Edit��6%   $
��Left=�6<   $��20��6W   $	��Top=�6c   $��95��6{   $��1,0,-1,0q�6�   .��5�      � ���	#FFE5E5E5$��
0,-12,0,12q�6.    5�      �� 5�      $
��223* �6+   �� 5�      $	��62* �6+   �� 5�      $	��76* �6+   �� 5�      $
��133* �6+   .+�5�      �� $
��Left=�6#   $	��189��6>   $��	73,36,0,0q�6K   $	��Top=�6^   $	��227��6v   $��3��6�   .��5�      �� -#   5        $ Metadata_File��5�   &   ��. $
��Auto=�6b   $��2��6�   $
��Left=�6�   $	��189��6�   $	��Top=�6�   $	��207��6�   $��2��6�   . �5�      /� .T�5�   "   J� 5�      5�      5�      �� 5�      $��	Enter Tag��6   $
��Left=�60   $��
47,234,0,0q�6K   $	��Top=�6_   $��99��6w   �� 5�      $��Enter Contents��6   $
��Left=�65   $��
47,278,0,0q�6P   $	��Top=�6d   $��89��6|   $ 0.157,0.615&�6�   �� 5�      -$   5        $ Tags_textbox��5�      $
��Left=�64   $��23��6O   $��165,237,0,0q�6[   $
 Wrap=�6p   $�� ��6�   $	��Top=�6�   $	��120��6�   $��2��6�   �� 5�      -%   5        $ Contents_textbox��5�      $
��Left=�68   $��23��6S   $��165,284,0,0q�6_   $
 Wrap=�6t   $�� ��6�   $	��Top=�6�   $	��120��6�   $��2��6�   �� 5�      -&   5        $

 Edit��5�      $
��Edit��6+   $��2��6:   $
��Left=�6J   $��
65,278,0,0q�6e   $	��Top=�6y   $��86��6�   $��2��6�   $��29��6�   6�   �� 5�      $��Current File��6   $
��Left=�63   $��	22,10,0,0q�6N   $	��Top=�6a   $��95��6y   �� 5�      -'   5        $ Current_File_TextBox��5�      $��2��6<   $
��Left=�6P   $��23��6k   $��
117,13,0,0q�6w   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   �� 5�      -(   5        $ edit_status��5�       $
��Left=�65   $��30��6P   $��
73,328,0,0q�6\   $	��Top=�6p   $	��377��6�   $��4��6�   .@�5�      �� $��	0,-9,0,10q�6(   .��5�      �� -)   5        $��	edit_text��5�   (   5�      5�      5�      5�      5�      5�      5�      