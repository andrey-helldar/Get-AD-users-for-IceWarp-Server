   M S B A M L   `   `   `  ����   Get AD users    Get_AD_users.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  
MainWindow��$��Center=�$��Center=� � 
ResizeMode$ NoResize=� � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ CenterScreen=�.��� $��Center=�$��Center=� � ColumnDefinitions �� $
��207* ��� $
��310* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Host:��$
��Left=�$��
10,10,10,1q�$��Center=��� $��Administrator username:��$
��Left=�$��
10,10,10,0q�$��1��$��Center=��� $��Administrator password:��$
��Left=�$��
10,10,10,1q�$��2��$��Center=��� $��Domain:��$
��Left=�$��
10,10,10,1q�$��3��$��Center=��� $��Object:��$
��Left=�$��
10,10,10,1q�$��4��$��Center=��� -    ��Name$ tbHost��$��1��$��
10,10,10,0q� �� TextWrapping$
 Wrap=�$��Center=�$	��200��$��192.168.1.1���� -   $ 
tbUsername��$��1��$��
10,10,10,0q�$��1��$
 Wrap=�$��Center=�$	��200��$��Admin��G� -    G�Name$ 
pbPassword��$��1��$��
10,10,10,0q�$��2��$��Center=�$	��200��	 G� Password$	 admin���� -   $ tbDomain��$��1��$��
10,10,10,0q�$��3��$
 Wrap=�$��Center=�$	��200��$��domain.local���� -   $ tbObject��$��1��$��
10,10,10,0q�$��4��$
 Wrap=�$��Center=�$	��200��$��Users���� -   
 ��Name$

 bGet��$	��Get��$��Center=�$��10q�$��5��$��Center=�$
��30,5q����	#FF0068FF
�������$��1����Z� �� -    ��Name$ lbLog��$��2��$��6��$��10q�$	��150���� $
��Left=�$��10q�$��5��$
��Wrap=�$��Center=��� .�Default user psw: �� -    ��Name$ userPassword��$
��Bold"�.�---