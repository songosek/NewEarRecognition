   M S B A M L   `   `   `  ����I  EEarRecognition, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:EarRecognitionEarRecognition  5   	       EarRecognition.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:EarRecognition     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��900��6)   .��5	      � .+�5
   
   �� -   5        $��canvas��5
   '   $
��Left=�65   $	��480��6P   $	��319��6]   $��	35,23,0,0q�6i   $	��Top=�6|   .+�5      �� -   5        $��image��5      5      �� 5   
   -   5        $��learning��5      
���   �$��1q�65   $
��Left=�6I   $	��246��6d   $��
520,33,0,0q�6q   $	��Top=�6�   $	��344��6�   ._�5      �� $��	0,0,0,-10q�6   .+�5      �� $��dodaj zdjecie��6   ��61   �� *
AddImage+�� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $��Podaj imię:��6   �� 5      -   5        $��learningName��5      $��Center=�62   $	��100��6M    �� TextWrapping$
 Wrap=�6\   5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Podaj nazwisko:��6   �� 5      -   5        $��learningSurname��5      $��Center=�65   $	��100��6P   $
 Wrap=�6_   5      �� 5      $��Punkty krańcowe��6   �� 5      $ 
Horizontal=�6   .+�5      �� $��Skrajny punkt po lewej:��6C   ��6   �� *LeftLearningPoint+6C   �� 5      -   5        $��leftLearningPoint��5   2   $��Współrzędne��6   62   5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Skrajny punkt po prawej:��6D   ��6   �� *RightLearningPoint+6D   �� 5      -   5        $��rightLearningPoint��5   2   $��Współrzędne��6   62   5       �� 5!      $ 
Horizontal=�6   .+�5"      �� $��Skrajny punkt na górze:��6B   ��6   �� *TopLearningPoint+6B   �� 5#      -   5        $��topLearningPoint��5#   2   $��Współrzędne��6   62   5$      �� 5%      $ 
Horizontal=�6   .+�5&      �� $��Skrajny punkt na dole:��6E   ��6   �� *BottomLearningPoint+6E   �� 5'      -	   5        $��bottomLearningPoint��5'   2   $��Współrzędne��6   62   5(      �� 5*      $��wyczyść punkty��69   ��6   �� *ClearPoints+69   �� 5+      $��dodaj��6;   ��6   �� *AddToDatabase+6;   5-      5.      �� 50   
   -
   5        $
��mode��50      $��Rozpoznawanie��6K   $
��Left=�6c   $��520,292,0,0q�6~   $	��Top=�6�    J� RenderTransformOrigin$ 1.676,0.533&�6�   �� 52   
   -   5        $��recognition��52      
���   �$��1q�68   $
��Left=�6L   $	��236��6g   $��520,312,0,0q�6t   $	��Top=�6�   $	��344��6�   ._�53      �� .+�54      �� $��dodaj zdjecie��6   ��61   �� *
AddImage+�� 55      $��Punkty krańcowe��6   �� 56      $ 
Horizontal=�6   .+�57      �� $��Skrajny punkt po lewej:��6C   ��6   �� *LeftLearningPoint+6C   �� 58      -   5        $��leftRecognitionPoint��58   2   $��Współrzędne��6   62   59      �� 5:      $ 
Horizontal=�6   .+�5;      �� $��Skrajny punkt po prawej:��6D   ��6   �� *RightLearningPoint+6D   �� 5<      -   5        $��rightRecognitionPoint��5<   2   $��Współrzędne��6   62   5=      �� 5>      $ 
Horizontal=�6   .+�5?      �� $��Skrajny punkt na górze:��6B   ��6   �� *TopLearningPoint+6B   �� 5@      -   5        $��topRecognitionPoint��5@   2   $��Współrzędne��6   62   5A      �� 5B      $ 
Horizontal=�6   .+�5C      �� $��Skrajny punkt na dole:��6E   ��6   �� *BottomLearningPoint+6E   �� 5D      -   5        $��bottomRecognitionPoint��5D   2   $��Współrzędne��6   62   5E      �� 5F      $��wyczyść punkty��69   ��6   �� *ClearPoints+69   �� 5G      $��	Rozpoznaj��67   ��6   �� *	Recognize+67   �� 5H      -   5        $��recognizedName��5H   )   $��(imię)��6   6)   �� 5I      -   5        $��recognizedSurname��5I   -   $��
(nazwisko)��6   6-   5J      5K      �� 5L   
   $��Stwórz bazę��64   $
��Left=�6J   $��420,115,0,0q�6e   $	��Top=�6z   $��75��6�   ��6   �� *CreateDatabase+6�   �� 5M   
   $��Otwórz bazę��62   $
��Left=�6H   $��420,153,0,0q�6c   $	��Top=�6x   $��75��6�   ��6   �� *OpenDatabase+6�   �� 5N   
   $��Aktualna baza:��6   $
��Left=�6)   $��420,192,0,0q�6D   $	��Top=�6Y   $ -0.474,0.423&�6q   �� 5O   
   -   5        $��databaseName��5O      $
��Left=�6$   $��441,218,0,0q�6?   $	��Top=�6T   5P      5Q      