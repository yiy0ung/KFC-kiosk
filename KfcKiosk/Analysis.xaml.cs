using Kfc.Core;
using KfcKiosk.DataSource;
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
	/// <summary>
	/// Analysis.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class Analysis : UserControl
	{
		delegate void Work();
		public delegate void OnSeatEventHandler(object sender, SeatArgs args);

		private Seat selectedSeat { get; set; }

		public Analysis()
		{
			InitializeComponent();
			UpdateTotalPrice();
		}

		private void UpdateTotalPrice()
		{
			StatDataSource sds = new StatDataSource();
			int total = sds.GetTotalPrice();
			totalPrice.Text = total.ToString();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//SeatCtrl sc = new SeatCtrl();
			//sc.ShowPage();
			this.Visibility = Visibility.Collapsed;
		}
	}
}
