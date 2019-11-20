using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
            seatCtrl.SeatEvent += MainView_Loaded;
            analysisCtrl.AnalysisEvent += MainView_Loaded;

            App.client.OnConnect += Tc_OnConnect;
            App.client.OnSocketError += Tc_OnSocketError;
            App.client.OnLogin += Tc_OnLogin;
        }

        private const string USERID = "@2208";

        private void Tc_OnLogin(object sender, EventArgs args)
        {
            MessageBox.Show("로그인 성공");

            App.client.lastLoginDate = DateTime.Now.ToString();
            seatCtrl.UpdateLoginDate();
        }

        private void Tc_OnConnect(object sender, EventArgs args)
        {
            App.client.LogIn(USERID); // 로그인
        }

        private void Tc_OnSocketError(object sender, SocketErrorArgs args)
        {
            MessageBox.Show(args.errMessage, "요청 실패", MessageBoxButton.OK, MessageBoxImage.Warning);

            App.client.lastConnectDate = DateTime.Now.ToString();
            seatCtrl.UpdateDisconnectDate();

            MessageBoxResult result = MessageBox.Show("재연결하시겠습니까?", "재연결", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                ConnectionServer();
            
            else
                return;
        }

        private async void setTimeout(Action action, int timeMilisec)
        {
            await Task.Delay(timeMilisec);
            action();
        }

        private void MainView_Loaded(object sender, EventArgs args)
        {
            ToggleMainView();
            analysisCtrl.refreshViewData();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            App.seatData.Load();
            App.floorData.Load();
            App.foodData.Load();
            ConnectionServer();

            setTimeout(() =>
            {
                onLoadFinish();
            }, 2000);
        }

        private void ConnectionServer() // 서버 연결 요청
        {
            App.client.Connect();
        }

        private void onLoadFinish()
        {
            loadingView.Visibility = Visibility.Collapsed;
            seatCtrl.Visibility = Visibility.Visible;
        }

        private void ToggleMainView()
        {
            if (seatCtrl.Visibility == Visibility.Visible)
            {
                seatCtrl.Visibility = Visibility.Collapsed;
                analysisCtrl.Visibility = Visibility.Visible;
            }
            else
            {
                seatCtrl.Visibility = Visibility.Visible;
                analysisCtrl.Visibility = Visibility.Collapsed;
            }
        }
    }
}
