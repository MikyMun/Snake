   M S B A M L   `   `   `  ���� @  <Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SnakeSnake      Snake.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Snake     -     � Title$  Sanke��$	��500��$	��800��$	��300��$	��400��   BackGroundColor#��[    	TextColor#��[   MainFont#��[  � WindowStartupLocation$ CenterScreen=� � Icon$ SnakeAssets/newIcon.ico��.��(� .��� $	��761������  j� Stretch$ Uniform=� �� ImageSource$ SnakeAssets/image.png�� j� 
AlignmentX$
 Left=� j� 
AlignmentY$ Center=� � RowDefinitions �� $
��auto ��� $��* �.+��� -    ��Name$ 	ScoreText��$
��Left=�$	��Top=�$��	Score : 0��$��22(�$
��Bold"�$��
622,92,0,0q�$��1���� -   	 ��Name$	 
GridBorder��$��1��  BorderColor#��[ $��10q�$��Center=�$��Center=�
 �� EdgeMode$
 Aliased=�$��20q�._�A� -    A�Name$ GameGrid��$	��400��$	��400��  GridBackGroundColor#��[  J� SnapsToDevicePixels$
 True�� �� BitmapScalingMode$ HighQuality=��� -   $	 Overlay��$��1��  OverlayColor#��[ $��171,0,171,0q�$
 Aliased=�$��Center=�����  �� Path$ ActualHeight� �� ElementName$ 
GridBorder��._��� -   $ OverlayText��$��Press Any Key To Start��$��26(�$��Center=�$��Center=�$
��Wrap=� � Triggers :�  :� RoutedEvent$ TextBlock.Loaded��.[��� .���� .��W�  �� 
TargetName$ OverlayText�� �� TargetProperty$ (TextBlock.Opacity)� W� From 1.0	 W� To 0.0 g� Duration$ 0:0:2D� g� AutoReverse$
 True�� g� RepeatBehavior$ Forever��