   M S B A M L   `   `   `  ����j  fST10096757_MoniqueJackson_MunicipalityApp_Part2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2.Controls8ST10096757_MoniqueJackson_MunicipalityApp_Part2.Controls  5      q=clr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2/ST10096757_MoniqueJackson_MunicipalityApp_Part2  5	      H    BST10096757_MoniqueJackson_MunicipalityApp_Part2.Views.ReportIssues   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      OucFclr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2.Controls  5      Ilocal=clr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2  5	      $	��550��5
      $	��900��6,   
������ .��5      �   � ColumnDefinitions  5   
   �� 5      $	��350 �6   �� 5      $��* �6   .+�5   
   �� ���#72BF78 �� Padding$ 30q�6&    �� CornerRadius$ 	25 0 0 25��63   ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5"      ��  �� TextAlignment$ Center=�6   $��SemiBold"�63   $��28(�6I   $��Recently Added View:��5#      ���#FFFBE6$��0��5$      �� 5'      -   5        $��MediaPreviewItemsControl��5'      $��1��6?   ��5(      �� 5)      .R�5*      �� .+�5+   "   �� $	��100��6h   $	��100��6t   y�6(   �� *ImageVisible+��6L   �� *
FilePath+6t   r� 5,   "   $	��200��6o   $	��100��6{    r� LoadedBehavior$
 Play=�6�   y�6/   �� *VideoVisible+ r� Source 6S   �� *
FilePath+6�   �� 5-   "   $��Open Document��6)   y�6A   �� *DocumentVisible+��6h   �� *OpenDocumentCommand+5.      5/      51      �� 54      -   5         ��Name$ UserDataStackPanel��54      $��2��69   $��0,10,0,0q�6F   $y�	Collapsed=�6X   .+�55      �� $��
Location: ��6    $��20(�62   ���#FFFBE66@   �� 56      -   5         ��Name$ LocationTextBlock��56       $��16(�6;   ���#33372C6I   �� 57      $��
Category: ��6    $��20(�62   ���#FFFBE66@   �� 58      -   5        $ CategoryTextBlock��58       $��16(�6;   ���#33372C6I   �� 59      $��Description: ��6    $��20(�65   ���#FFFBE66C   �� 5:      -   5        $ DescriptionTextBlock��5:       $��16(�6>   ���#33372C6L   5;      �� 5>      $��3��6   $��Center=�6*   $	��Top=�6G   	 �� Orientation$	 Vertical=�6_   .+�5?      �� $
��Wrap=�6    $��20(�64   $ Center=�6B   ���#FFFBE6$��See All:��5@   -   $��0,0,5,0q�6=   �� 5A      -   5        $��
ViewButton��5A   a      buttonBlack#��[  6   .��5B      "  FontAwesome.WPF.ImageAwesome  
   Icon$
 
AngleRight=�6*   $��25��6<   $��25��6G      
Foreground �#FEFF9F$��3 0 0 0q�6h   5C      5D      5E      5F      �� 5I   
   -   5        $��1��5I      $ 20q�6!   ���#ffffff$ 	0 25 25 0��6`   ._�5J      �  5L      �� 5M      $
��Auto �6$   �� 5O      $
��Auto �6$   �� 5Q      $
��Auto �6$   �� 5S      $
��Auto �6$   �� 5U      $
��Auto �6$   �� 5W      $
��Auto �6$   �� 5Y      $
��Auto �6$     5\      �� 5]      $	��200 �6'   �� 5_      $��* �6'   .+�5d      �� $��0��6   $��2��6)   $��Report an Issue��5e      $��36(�5f      $��Black"�5g      $��Center=�5h      $��	0,10,0,20q�5i      ���#114232$��Bahnschrift Condensed)�5k      �� 5n      $��1��6   $��0��6%   $��	Location:��5o      $	��Top=�5p      $
��5,10q�5q      $��Bahnschrift Condensed)�5r      $��22(�5s      �� 5t      -   5        $��LocationTextBox��5u      $��1��5t      $��1��6'   $��14(�5w      $
��5,10q�5x      �� 5{      $��2��6   $��0��6%   $��	Category:��5|      $	��Top=�5}      $
��5,10q�5~      $��Bahnschrift Condensed)�5      $��22(�5�      �� 5�      -	   5        $��CategoryComboBox��5�      $��2��5�      $��1��6(   $��14(�5�      $
��5,10q�5�      .@�5�      �� $��
Sanitation��6#   �� 5�      $��Roads��6#   �� 5�      $��	Utilities��6#   �� 5�      $��Parks and Recreation��6#   �� 5�      $��Public Safety��6#   �� 5�      $��Traffic��6#   �� 5�      $��Street Lighting��6#   �� 5�      $��Maintenance��6#   5�      �� 5�      $��3��6   $��0��6%   $��Description:��5�      $	��Top=�5�      $
��5,10q�5�      $��Bahnschrift Condensed)�5�      $��22(�5�      �� 5�      -
   5        $��DescriptionRichTextBox��5�      $��3��5�      $��1��6+   $��5q�5�      
�������$
��5,10q�5�      $	��100��5�      �� 5�      $��4��6   $��0��6%   $��Attach Media:��5�      $	��Top=�5�      $
��5,10q�5�      $��Bahnschrift Condensed)�5�      $��22(�5�      �� 5�      -   5        $��AttachMediaButton��5�      $��4��5�      $��1��6&   $��Attach File��5�      $
��5,10q�5�      � 5�      -   5        $��ReportingProgressBar��5�      $��5��5�      $��20,10q�5�       � Minimum$ 0Q�5�       � Maximum$	 100Q�5�       � IsIndeterminate .  $��2��5�      $��12��5�      �� 5�      $��6��6   $	 
Horizontal=�5�      $��Center=�5�      $	��Top=�5�      $��0,20,0,0q�5�      $��2��5�      .+�5�      �� -   5        $��SubmitButton��5�      $��Submit��5�      $��5q�5�      �� 5�      -   5        $��
BackButton��5�      $��Back to Main Menu��5�      $��5q�5�      5�      5�      5�      5�      5�      