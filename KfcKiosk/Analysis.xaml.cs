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
using System.Net.Sockets;

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
			lvPaidFood.ItemsSource = App.analysisData.lsPaidFood;
		}

		public void refreshViewData()
		{
			refreshTotalPrice();
            LoadMenuAnalysisByCategory();

        }

		private void refreshTotalPrice()
		{
			int total = App.analysisData.GetFoodTotalPrice();
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

        private void LvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCategory.SelectedItem == null) { LoadMenuAnalysisByCategory(); return; }

            string category = ((ListBoxItem)(lvCategory.SelectedItem))
                                      .Content
                                      .ToString();

            LoadMenuAnalysisByCategory(category);
        }

        private void LoadMenuAnalysisByCategory(string category = "All")
        {
            int categoryPrice = 0;
            int categoryCount = 0;
            List<Food> lstMenuToShow = new List<Food>();

            foreach (Food food in App.analysisData.lsPaidFood)
            {
                if (food.Category.ToString().Equals(category) ||
                    category.Equals("All"))
                {
                    lstMenuToShow.Add(food);
                    categoryPrice += food.TotalPrice;
                    categoryCount += food.Count;
                }
            }

            if (category == "All")
            {
                categoryTitle.Text = "전체 매출";
            }
            else
            {
                categoryTitle.Text = category + " 매출";
            }

            categoryTotalPrice.Text = categoryPrice.ToString() + " 원";
            categoryTotalCount.Text = categoryCount.ToString();
            lvPaidFood.ItemsSource = lstMenuToShow;
        }

        private void BtnSaveAnalysis_Click(object sender, RoutedEventArgs e)
        {
            int totalPrice = App.analysisData.GetFoodTotalPrice();

            SendAnalyInfo(totalPrice);

            MessageBox.Show("저장을 성공하였습니다.", "저장 성공", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SendAnalyInfo(int totalPrice)
        {
            int total = totalPrice;
            string message = "";

            message += "하루 전체 매출액: " + total.ToString() + " 원";

            App.client.SendAll(message);
        }
    }
}

