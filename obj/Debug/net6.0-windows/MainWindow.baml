   M S B A M L   `   `   `  ????G  CLetsBowl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:LetsBowl.WPFLetsBowl.WPF  5   	       LetsBowl.WPF.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:LetsBowl.WPF     5   	     ? Title$  
MainWindow??5   	   $	??450??6   $	??800??6)   .??5	      ?  ? RowDefinitions 5
   
   ?? 5      ?? 5      $??80 ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dg??5         AutoGenerateColumns$ False??6      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly$
 True??6%      Header$ Frame??6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   ?? *	FrameNo+6S     5      $ Ball 1??6?    6%   ?? *Ball1+6?     5      $ Ball 2??6?    6%   ?? *Ball2+6?     5      $ Ball 3??6?    6%   ?? *Ball3+6?     5      $
 True??6%   $ Frame Score??6Q    67   ?? *Score+6Q     5      $
 True??6%   $ Running Score??6X    67   ?? *RunningScore+6X     5      $
 True??6%   $ Strike???6U    67   ?? *	IsAStrike+6U     5      $
 True??6%   $ Spare???6T    67   ?? *
IsASpare+6T   5      ?? 5   
   $??1??6    ?? Orientation$ 
Horizontal=?6"   .+?5      ?? -   5        $??Update totals??5      $	??120??6-   6P   ?? 5      -   5        $??	Get Score??5      $	??120??6)   65   ?? 5      $
??Wrap=?6   $??16q?6,   $????Click in the Ball 1, Ball 2 or Ball 3 cell to edit the scores.  The grid does not always refresh so the refresh button is there until that bug is fixed??68   $	??528??6?   $??56??6?   6?   5      5      5       