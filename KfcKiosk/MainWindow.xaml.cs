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
        }

        public void ToggleMainPayment()
        {
            Debug.WriteLine("main window toggle");
            if (mainCtrl.Visibility == Visibility.Visible)
            {
                Debug.WriteLine("main window visible");
                mainCtrl.Visibility = Visibility.Collapsed;
                paymentCtrl.Visibility = Visibility.Visible;
                
                Debug.WriteLine("main: " + mainCtrl.Visibility.ToString());
                Debug.WriteLine("pay: " + paymentCtrl.Visibility.ToString());
            }
            else
            {
                Debug.WriteLine("main window collap");
                mainCtrl.Visibility = Visibility.Visible;
                paymentCtrl.Visibility = Visibility.Collapsed;
            }

            Debug.WriteLine("main2: " + mainCtrl.Visibility.ToString());
            Debug.WriteLine("pay2: " + paymentCtrl.Visibility.ToString());
        }
    }
}
