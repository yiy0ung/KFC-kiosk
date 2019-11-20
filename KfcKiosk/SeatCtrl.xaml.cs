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
using System.Net.Sockets;
using System.ComponentModel;

namespace KfcKiosk
{
    public class SeatArgs : EventArgs
    {
    }

    public partial class SeatCtrl : UserControl
    {
        delegate void Work();
        public delegate void OnSeatEventHandler(object sender, SeatArgs args);
        public event OnSeatEventHandler SeatEvent;

        private Seat selectedSeat { get; set; }

        public SeatCtrl()
        {
            InitializeComponent();
            Dispatcher.Invoke(DispatcherPriority.Normal, new Work(PutCurrentTime));
            this.Loaded += SeatCtrl_Loaded;
            orderCtrl.OrderEvent += OrderCtrl_OnPaymentEvent;
        }

        private void SeatCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSeatDataSource();
        }

        private void LoadSeatDataSource() // floor 데이터 바인딩
        {
            lvFloor.ItemsSource = App.floorData.lstFloor;
        }

        private void OrderCtrl_OnPaymentEvent(object sender, OrderArgs args) // order ctrl에 보내지는 이벤트
        {
            SeatPayPage_Toggle();
            OnOrderInfoHandler(args.selectedSeat.OrderInfo);
        }

        private void OnOrderInfoHandler(string orderInfo) // 주문 목록 업데이트
        {
            seatOrderInfo.Content = orderInfo;
        }

        private void PutCurrentTime() // 현재 시간 출력
        {
            currentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Work(PutCurrentTime));
        }

        private void LvFloor_SelectionChanged(object sender, SelectionChangedEventArgs e) // Floor Selection changed
        {
            Floor floor = (lvFloor.SelectedItem as Floor);

            UpdateSeat(floor.Idx);
        }

        private void LvSeat_SelectionChanged(object sender, SelectionChangedEventArgs e) // Seat Selection changed
        {
            if (lvSeat.SelectedIndex < 0)
            {
                lvSeat.SelectedIndex = 0;
            }

            Seat seat = lvSeat.SelectedItem as Seat;
            selectedSeat = seat;

            UpdateSeatViewInfo(seat);
        }

        private void UpdateSeat(int floorIdx) // 층을 선택했을 때 floorIdx를 인자로 받아 업데이트
        {
            List<Seat> SelectedSeat = App.seatData.FilterSeat(floorIdx);
            lvSeat.ItemsSource = SelectedSeat;
        }

        private void UpdateSeatViewInfo(Seat seat) // 자리 정보 뷰 업데이트
        {
            seatId.Text = seat.Id;
            seatOrderInfo.Content = seat.OrderInfo;

            seatCheckBtn.Visibility = Visibility.Visible;
            seatFoodBtn.Visibility = Visibility.Visible;
        }

        private void SeatCheckBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSeat.lstFood.Count == 0)
            {
                MessageBox.Show("주문한 메뉴가 없습니다!", "결제 실패", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult allowPayment = MessageBox.Show("결제하시겠습니까?", "결제 확인", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (allowPayment == MessageBoxResult.OK)
            {
                int totalPrice = 0;
                for (int i = 0; i< selectedSeat.lstFood.Count; i++)
                {
                    totalPrice += selectedSeat.lstFood[i].Price;
                }

                paymentSeatId.Text = selectedSeat.Id;
                paymentTotalPrice.Text = "총 결재 금액 : " + totalPrice.ToString();
                paymentOrderInfo.Text = selectedSeat.OrderInfo;

                paymentAlert.Visibility = Visibility.Visible;
            }
        }
        private void PayMenu(object sender, RoutedEventArgs e) // 메뉴 결제
        {
            SendPayInfo(); // 결제 정보 전송

            // 결제된 메뉴에 추가
            App.analysisData.AppendPaidFoods(selectedSeat.lstFood);

            // selectedSeat의 메뉴 초기화
            App.seatData.ClearSeat(selectedSeat.Id);
            OnOrderInfoHandler("");

            HiddenPaymentAlert();

            MessageBox.Show("결제 되었습니다", "결제 성공", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SendPayInfo() {
            int totalPrice = 0;
            string message = "";

            for (int i = 0; i < selectedSeat.lstFood.Count; i++)
            {
                Console.WriteLine(selectedSeat.lstFood[i].Price);
                totalPrice += selectedSeat.lstFood[i].Price;
            }
            
            message += selectedSeat.Id.ToString() + " " + totalPrice.ToString() + "원 결제 완료";

            App.client.Send(message);
        }

        public void UpdateLoginDate()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                displayConnectTime.Text = "최근 로그인 시간 " + App.client.lastLoginDate;
            }));
        }

        public void UpdateDisconnectDate()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                displayConnectTime.Text = "최근 서버접속종료 시간 " + App.client.lastConnectDate;
            }));
        }

        private void BtnPaymentCancel_Click(object sender, RoutedEventArgs e)
        {
            HiddenPaymentAlert();
        }

        private void SeatPayBtn_Click(object sender, RoutedEventArgs e)
        {
            HiddenPaymentAlert();
            SeatPayPage_Toggle();
        }

        private void HiddenPaymentAlert()
        {
            paymentAlert.Visibility = Visibility.Collapsed;
        }

        private void SeatPayPage_Toggle()
        {
            if (seatCtrl.Visibility == Visibility.Visible)
            {
                orderCtrl.SelectedSeat = selectedSeat;
                seatCtrl.Visibility = Visibility.Collapsed;
                orderCtrl.Visibility = Visibility.Visible;
            }
            else
            {
                seatCtrl.Visibility = Visibility.Visible;
                orderCtrl.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnStatView_Click(object sender, RoutedEventArgs e)
        {
            SeatArgs args = new SeatArgs();

            if (SeatEvent != null)
            {
                SeatEvent(this, args);
            }
        }
    }
}