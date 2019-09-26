using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KfcKiosk
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            //seatCtrl.SeatComplete += SeatCtrl_OnSeatComplete;
        }

        //private void SeatCtrl_OnSeatComplete(object sender, SeatArgs args)
        //{
        //    //Debug.WriteLine(args.TableId);
            
        //    //seatCtrl.Visibility = Visibility.Collapsed;
        //    //paymentCtrl.Visibility = Visibility.Visible;
        //}

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            App.seatData.Load();
            App.floorData.Load();
            App.foodData.Load();
        }
    }
}
