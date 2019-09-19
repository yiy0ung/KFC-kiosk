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
using System.Windows.Shapes;

namespace KfcKiosk
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
            this.Loaded += PaymentWindow_Loaded;
        }

        private void PaymentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.foodData.Load();
            LoadMenu("All");
        }

        private void Prev_Window(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void LoadMenu(string selectedCategory)
        {
            //Console.WriteLine(selectedCategory);
            if (selectedCategory == null) return;

            switch(selectedCategory)
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

            Console.WriteLine(selectedCategory);
            LoadMenu(selectedCategory);
        }
    }
}
