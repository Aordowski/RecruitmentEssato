   M S B A M L   `   `   `  ����N  JZadanieRekrutacyjne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ZadanieRekrutacyjneZadanieRekrutacyjne  5   	   $    ZadanieRekrutacyjne.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:ZadanieRekrutacyjne     5   	     � Title$   Simple Data Management App��5   	   $	��581��6,   $	��820��69   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridView��5
   %      
IsReadOnly$
 True��6   $��
0,63,0,146q�6f   �� 5   
   -   5        $%��Add new 3 Day Prognosis Entries��5      $
��Left=�6;   $��	10,0,0,10q�6V   $��Bottom=�6i   $��47��6�   $	��277��6�   6�   �� 5   
   -   5         ��Name$ SelectPageBox��5      $
��Left=�6*   $��	10,29,0,0q�6E   $	��Top=�6X   $	��120��6p   6|   �� 5   
   -   5        $��List All entries��5      $
��Left=�6,   $��
313,0,0,28q�6G   $��Bottom=�6[   $��47��6v   $��97��6�   6�   �� 5   
   -   5        $��Search��5      $
��Left=�6"   $��
160,18,0,0q�6=   $	��Top=�6Q   $��33��6i   $	��143��6u   6�   �� 5   
   -   5        $(��"Add new Entry With Current Weather��5      $
��Left=�6>   $��	10,0,0,62q�6Y   $��Bottom=�6l   $��47��6�   $	��277��6�   6�   �� 5   
   -   5        $ SearchWeatherTypeBox��5      $
��Left=�61   $��
325,24,0,0q�6L   $	��Top=�6`   $	��143��6x   �� 5   
   $��Page Select��6   $
��Left=�6&   $��35,6,0,0q�6A   $	��Top=�6S   �� 5   
   -   5         ��Name$ 
TempMinBox��5      $
��Left=�6&   $��
492,24,0,0q�6A    �� TextWrapping$
 Wrap=�6U   $	��-10��6i   $	��Top=�6t   $	��120��6�   �� 5   
   -	   5        $ 
TempMaxBox��5      $
��Left=�6&   $��
678,24,0,0q�6A   $
 Wrap=�6U   $	��999��6i   $	��Top=�6t   $	��120��6�   �� 5   
   $��Temperature Min��6   $
��Left=�6*   $��	502,0,0,0q�6E   $	��Top=�6X   �� 5   
   $��Temperature Max��6   $
��Left=�6*   $��	678,0,0,0q�6E   $	��Top=�6X   �� 5   
   $��Weather Conditions��6   $
��Left=�6-   $��	330,0,0,0q�6H   $	��Top=�6[   5      5      