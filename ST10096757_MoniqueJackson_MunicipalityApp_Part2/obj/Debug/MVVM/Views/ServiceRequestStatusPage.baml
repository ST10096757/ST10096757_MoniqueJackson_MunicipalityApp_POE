   M S B A M L   `   `   `  ����j  fST10096757_MoniqueJackson_MunicipalityApp_Part2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}Cclr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views5ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views  5      �Hclr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels:ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels  5      T    NST10096757_MoniqueJackson_MunicipalityApp_Part2.Views.ServiceRequestStatusPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      UlocalCclr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2.Views     5      WvmHclr-namespace:ST10096757_MoniqueJackson_MunicipalityApp_Part2.ViewModels     5      -   5          >�Name$  ServiceRequestPage��5      $	��600��5      $	��900��6   .��5	      � ���#FBF8EF.+�5   
   �� -   5        $
��Back��5      $
��Left=�6    $	��Top=�6;   $��75��5      $��30��6   $��	10,10,0,0q�6(   ��5      �� *BackCommand+61   �� 5   
   $��Service Request Status��6   $��24(�5      $
��Bold"�5      $��Center=�5      $	��Top=�5      $��0,10,0,0q�5      ���#062C305      �� 5   
   -   5         ��Name$ txtRequestId��5      $	��250��6(   $��25��64   $	��Top=�6@   $
��Left=�6X   $��	10,60,0,0q�6s   $��10q�5   T   ���#CDE8E5��6   �� *SearchQuery+ �� UpdateSourceTrigger$ PropertyChanged=�6a   �� 5   
   $��Generate MST��6   $
��Left=�5       $	��Top=�6,   $��280,60,10,0q�6D   $	��120��5!      $��30��6   ���#4CAF50
����������#4CAF50��5   (   �� *GenerateMSTCommand+5!   Q   �� 5$   
   $��Display Graph��6   $
��Left=�5%      $	��Top=�6,   $��420,60,10,0q�6D   $	��120��5&      $��30��6   ���#2196F3
����������#2196F3��5$   )   �� *DisplayGraphCommand+5&   Q   �� 5)   
    �� 
IsReadOnly . $	��250��6F   $��50��6R   $
��Left=�5*      $	��Top=�6-   $��560,60,10,0q�6E   $��5q�6Z   ��5)      �� *GraphDisplayText+5*   Z   �� 5-   
    �� Orientation$ Vertical=�6   $
��Left=�6,   $	��Top=�6G   $��10,120,10,0q�6_   .+�5.      �� $��Status Filter��6   $
��Left=�6,   �� 5/      -   5         ��Name$ cmbStatusFilter��5/      $	��200��60   $	��Top=�6<   $��5q�50   N    �� SelectedItem 6   �� *SelectedCategory+ �� Mode$ TwoWay=���51      �� 52       �� Path$ 
Categories�6   54      55      �� 58   
   $ Vertical=�6   $
��Left=�6,   $	��Top=�6G   $��220,120,10,0q�6_   .+�59      �� $��Priority Filter��6   $
��Left=�6.   �� 5:      -   5        $ cmbPriorityFilter��5:      $	��200��62   $	��Top=�6>   $��5q�5;   N    6   �� *SelectedPriority+$ TwoWay=���5<      �� 5=      $ 
Priorities�6   5?      5@      �� 5C   
   $��Sort by Submission Date��6   $
��Left=�5D      $	��Top=�6,   $��650,120,10,0q�6D   $	��160��5E      $��30��6   ���#FF9800
����������#FF9800��5C   3   �� *SortByDateCommand+5E   Q   �� 5H   
   -   5        	 ��Name$	 dataGridRequests��5H      $��10,180,10,10q�5I   I   
 �� AlternationCount$
 2��5J      
����������#ddd$��1q�6N   ��5H   -   �� *FilteredServiceRequests+ 5I      �� *SelectedRequest+$ TwoWay=� �� View 5K      �� 5L      .P�5M      �� $��ID��6%    �� Width$ 60��6\    �� DisplayMemberBinding 61   �� *	RequestId+6\   �� 5N      $��Resident Name��6%   $	 150��6j    6<   �� *ResidentName+6j   �� 5O      $��Contact Details��6%   $	 150��6n    6>   �� *ContactDetails+6n   �� 5P      $��Request Type��6%   $	 120��6h    6;   �� *RequestType+6h   �� 5Q      $��Description��6%   $	 200��6g    6:   �� *Description+6g   �� 5R      $��Status��6%   $	 100��6]    65   �� *Status+6]   �� 5S      $��Request Date��6%   $	 120��6�    6;   �� *SubmissionDate+ �� StringFormat$ {0:yyyy-MM-dd}��6�   �� 5T      $��Priority��6%   $	 100��6a    67   �� *
Priority+6a   �� 5U      $��Assigned Staff��6%   $	 120��6l    6=   �� *AssignedStaff+6l   �� 5V      $��Location��6%   $	 120��6a    67   �� *
Location+6a   �� 5W      $��Progress Logs��6%   $	 180��6p    6<   �� *ProgressLogsString+6p   5X      5Z      5[      5\      