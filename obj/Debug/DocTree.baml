   M S B A M L   `   `   `  ����\  XWindows10FileplusSettingsTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Windows10FileplusSettingsTransfer!Windows10FileplusSettingsTransfer  5   	   /    )Windows10FileplusSettingsTransfer.DocTree   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:Windows10FileplusSettingsTransfer     5   	   -   5          � Title$   ��5      $��720.976��6   $��689.329��6"    � Icon$ 
folder.png��6I    � 	Resources 5	      >   8Windows10FileplusSettingsTransfer.ImageToHeaderConverter   img��%   &	          5
   
   6'   5      .��5      � $��0,0,0,2q�6   .+�5   
   R� -   5        $��folders��5      $��16(�6N   $��
0,0,342,85q�6\    5       �� 
TargetType �� Property �� Value �� Orientation d� 	IsChecked �� Path	 �� Mode
 �� RelativeSource �� AncestorLevel �� AncestorType �� Mode �� 	Converter��%�  'Q�       7   �� 5      " Q�6   .�5      ��  � �� 5      �� 5      .R�5   "   �� $ 
Horizontal=�6-   .+�5   &   �� $��checkBoxTree��6/   -   5         5   C   �� $ 	IsChecked�$	 TwoWay=�6�   �� 5   &   $��25��6,   $��15��67   ��6C   �� $	 Tag�
 �� $ 1��$ TreeViewItemL�$ FindAncestor=�8   6�   �� 5   &   ��60   �� 6C   5   #   5      5      5      5      5      �� 5   
   -   5         ��Name$ transfer_button��5      $��Start Transfer��6*   $��Right=�6C   $��Bottom=�6_   $��	0,0,10,10q�6z   $	��110��6�    J� RenderTransformOrigin$ -0.18,0.436&�6�   $��33��6�   � 5   
   -   5        $��	progress1��5      $��Stretch=�6'   $��22��6E   $��
10,0,10,53q�6Q   $��Bottom=�6e   6�   �� 5    
   -   5         ��Name$ label1��5       $�� ��6    $
��Left=�6+   $��37��6F   $��	384,0,0,7q�6R   $��Bottom=�6e   $	��165��6�   $ 0.5,0.5&�6�   z�5!      V� 5"      .~�5#      �� �� 5$       �� AngleX$ -12.265Q�6$   �� 5%      T� 5&       T� X$ -2.283Q�6)   5'      5)      �� 5*   
   -   5        $ label2��5*      $�� ��6    $��Center=�6+   $��43��6H   $��86,625,396,0q�6T   $��Bottom=�6j   $	��199��6�   �� 5+   
   $��0��6   $��5��6'   $��10��68   $��Right=�6D   $��Stretch=�6`   �� 5,   
   $��0��6   $��44��6$   $��Stretch=�60   $��Bottom=�6N   $��	0,0,0,-34q�6i   �� 5.   
   -   5         ��Name$ dirList��5.      $��0q�5/      $
��Left=�50      $	��Top=�68   $��0��51      $��0��6&   $��Visible=�52      $��
344,0,0,85q�54       �� SelectedItem 53      �� $ DirViewVM.CurrentItem�$	 OneWayToSource=���55      �� 56      .R�57      �  � Orientation$ 
Horizontal=�6     � 	ItemWidth$	 220��69   6K   58      5:      �� 5;   
   -	   5        $ labeltransfer��5;      $
��Left=�6'   $��40��6B   $��0,693,0,-45q�6N   $	��Top=�6c   $	��681��6{   5<      5=      