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

namespace KfcKiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            UpdateData();
        }

        private void LoadData()
        {
            App.seatData.Load();
            App.floorData.Load();
            App.foodData.Load();
        }

        private void UpdateData()
        {
            UpdateFloor();
        }

        private void UpdateFloor()
        {
            lvFloor.ItemsSource = App.floorData.lstFloor;
        }

        private void UpdateSeat()
        {
            lvSeat.ItemsSource = App.seatData.lstSeat;
        }

        private void UpdateSeat(int floorIdx)
        {
            List<Seat> SelectedSeat = App.seatData.FilterSeat(floorIdx);
            lvSeat.ItemsSource = SelectedSeat;
        }

        private void UpdateInfo(Seat seat)
        {
            seatId.Text = seat.Id;
            seatOrderInfo.Text = seat.OrderInfo;

            seatPayBtn.Visibility = Visibility;
            seatFoodBtn.Visibility = Visibility;
        }

        private void LvFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Floor floor = (lvFloor.SelectedItem as Floor);

            UpdateSeat(floor.Idx);
        }

        private void LvSeat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvSeat.SelectedIndex < 0)
            {
                lvSeat.SelectedIndex = 0;
            }

            Seat seat = lvSeat.Items[lvSeat.SelectedIndex] as Seat;

            UpdateInfo(seat);
        }
    }
}
