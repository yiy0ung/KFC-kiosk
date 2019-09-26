using Kfc.Core;
using System;
using System.Collections.Generic;
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
    //public class SeatArgs : EventArgs
    //{
    //    public string TableId { get; set; }
    //}

    public partial class SeatCtrl : UserControl
    {
        delegate void Work();
        //public delegate void OnSeatCompleteHandler(object sender, SeatArgs args);
        //public event OnSeatCompleteHandler SeatComplete;

        private Seat selectedSeat { get; set; }

        public SeatCtrl()
        {
            InitializeComponent();
            Dispatcher.Invoke(DispatcherPriority.Normal, new Work(PutCurrentTime));
            this.Loaded += SeatCtrl_Loaded;
        }

        private void SeatCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFloor();
        }
        private void UpdateFloor()
        {
            lvFloor.ItemsSource = App.floorData.lstFloor;
        }

        private void PutCurrentTime()
        {
            currentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Work(PutCurrentTime));
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

            seatCheckBtn.Visibility = Visibility.Visible;
            seatFoodBtn.Visibility = Visibility.Visible;
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
            selectedSeat = seat;

            UpdateInfo(seat);
        }

        private void SeatPayBtn_Click(object sender, RoutedEventArgs e)
        {
            //SeatArgs args = new SeatArgs();
            //args.TableId = selectedSeat.Id;

            //if (SeatComplete != null)
            //{
            //    SeatComplete(this, args);
            //}
            paymentCtrl.SelectedSeat = selectedSeat;
            seatCtrl.Visibility = Visibility.Collapsed;
            paymentCtrl.Visibility = Visibility.Visible;
        }
    }
}
