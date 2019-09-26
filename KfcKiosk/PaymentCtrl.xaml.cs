using Kfc.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PaymentCtrl.xaml
    /// </summary>
    public partial class PaymentCtrl : UserControl, INotifyPropertyChanged
    {
        private List<Food> orderList = new List<Food>();

        private int total = 0;
        public int Total
        {
            get => total;
            set
            {
                total = value;
                NotifyPropertyChanged(nameof(Total));
            }
        }

        public PaymentCtrl()
        {
            InitializeComponent();
            this.Loaded += PaymentCtrl_Loaded;
        }

        private void PaymentCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            App.foodData.Load();
            LoadMenu("All");
            ResetTime();
            totalPrice.DataContext = this;
        }

        private void Prev_Window(object sender, RoutedEventArgs e)
        {
            //((MainWindow)System.Windows.Application.Current.MainWindow).ToggleMainPayment();
        }

        private void LoadMenu(string selectedCategory)
        {
            if (selectedCategory == null) selectedCategory = "All";

            switch (selectedCategory)
            {
                case "Burger":
                    SetMenu("Burger");
                    break;

                case "Chickin":
                    SetMenu("Chickin");
                    break;

                case "Drink":
                    SetMenu("Drink");
                    break;

                case "Snack":
                    SetMenu("Snack");
                    break;

                default:
                    SetMenu("All");
                    break;
            }
        }

        private void SetMenu(string selectedCategory)
        {
            List<Food> foodList = new List<Food>();

            foreach (Food food in App.foodData.lstMenu)
            {
                if (food.Category.ToString().Equals(selectedCategory) || selectedCategory.Equals("All"))
                {
                    foodList.Add(food);
                }
            }

            menuList.ItemsSource = foodList;
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory = ((ListBoxItem)(categoryList.SelectedItem)).Content.ToString();
            LoadMenu(selectedCategory);
        }

        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food selectedMenu = ((Food)menuList.SelectedItem);

            if (selectedMenu == null) return;

            ResetTime();

            if (!(orderList.Contains(selectedMenu)))
            {
                selectedMenu.Count = 1;
                orderList.Add(selectedMenu);

                Total += selectedMenu.Price;
            }

            orderedList.ItemsSource = orderList;
            orderedList.Items.Refresh();

            menuList.SelectedItem = null;
        }

        private void ResetTime()
        {
            leastTime.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            UIElementCollection parentTextBlock = (((((sender as FrameworkElement).Parent) as FrameworkElement).Parent) as Grid).Children;
            string foodName = (parentTextBlock[0] as TextBlock).Text;
            string content = (sender as Button).Content.ToString();

            foreach (Food menu in orderList)
            {
                if (menu.Name.Equals(foodName))
                {
                    if (content.Equals("+"))
                    {
                        menu.Count++;
                        Total += menu.Price;
                    }
                    else if (content.Equals("-"))
                    {

                        menu.Count--;
                        Total -= menu.Price;

                        if (menu.Count < 1) orderList.Remove(menu);
                    }
                    break;
                }
            }
            orderedList.Items.Refresh();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            orderList.Clear();
            Total = 0;
            orderedList.Items.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
