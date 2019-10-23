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
  public class AnalysisArgs : EventArgs
  {

  }
	public partial class Analysis : UserControl
	{
		public delegate void OnAnalysisEventHandler(object sender, AnalysisArgs args);
        public event OnAnalysisEventHandler AnalysisEvent;

		public Analysis()
		{
			InitializeComponent();
            this.Loaded += AnalysisCtrl_Loaded;
		}

		private void AnalysisCtrl_Loaded(object sender, RoutedEventArgs e)
		{
		    LoadPaidFoodPrice();
		}

		private void LoadPaidFoodPrice()
		{
			lvPaidFood.ItemsSource = App.foodData.lstMenu;
		}

		public void refreshViewData()
		{
			refreshTotalPrice();
		}

		private void refreshTotalPrice()
		{
			int total = App.foodData.GetTotalPrice();
			totalPrice.Text = total.ToString();
		}

		private void BtnSeatView_Click(object sender, RoutedEventArgs e)
		{
			AnalysisArgs args = new AnalysisArgs();

			if (AnalysisEvent != null)
			{
				AnalysisEvent(this, args);
			}
			else
			{
				String errorText = "로딩에 실패했습니다.";
				btnSeatView.Content = errorText;
			}
		}

	}
}

