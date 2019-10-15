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
    public class PayArgs : EventArgs
    {
        public Seat selectedSeat { get; set; }
    }
    public partial class PaymentCtrl : UserControl, INotifyPropertyChanged
    {
        public delegate void OnPayEventHandler(object sender, PayArgs args);
        public event OnPayEventHandler PayEvent;

        public Seat SelectedSeat { get; set; }
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
            LoadMenu("All");
            totalPrice.DataContext = this;
        }

        public void LoadOrderList()
        {
            if (this.SelectedSeat == null)
            {
                Console.WriteLine("ERR: THIS_SELECTEDSEAT_NULL");
                return;
            }

            foreach (Seat seat in App.seatData.lstSeat)
            {
                //주문 화면의 테이블 넘버와 일치할 때
                if (this.SelectedSeat.Id.Equals(seat.Id))
                {
                    Console.WriteLine(this.SelectedSeat.Id + "OrderList_Loaded");

                    orderList = seat.lstFood;
                    ClearTotal();

                    foreach (Food food in orderList)
                        Total += food.Price * food.Count;
                }
            }

            lvOrdered.ItemsSource = orderList;
            lvOrdered.Items.Refresh();
        }

        private void Prev_Ctrl(object sender, RoutedEventArgs e)
        {
            // App.SeatDataSource에 선택된 Seat에 orderInfo 업데이트

            PayArgs args = new PayArgs();
            args.selectedSeat = this.SelectedSeat;

            if (PayEvent != null)
                PayEvent(this, args);

            UpdateOrderInfo();
        }

        private void LoadMenu(string selectedCategory)
        {
            if (selectedCategory == null)
                selectedCategory = "All";

            switch (selectedCategory)
            {
                case "Burger":
                    SetMenu("Burger");
                    break;

                case "Chicken":
                    SetMenu("Chicken");
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
                if (food.Category.ToString().Equals(selectedCategory) || 
                    selectedCategory.Equals("All"))
                {
                    foodList.Add(food);
                }
            }

            lvMenu.ItemsSource = foodList;
        }

        private void LvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory = ((ListBoxItem)(lvCategory.SelectedItem))
                                      .Content
                                      .ToString();
            LoadMenu(selectedCategory);
        }

        private void LvMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food selectedMenu = ((Food)lvMenu.SelectedItem);

            if (selectedMenu == null) return;

            ResetTime();

            if (!(orderList.Contains(selectedMenu)))
            {
                selectedMenu.Count = 1;
                orderList.Add(selectedMenu);

                Total += selectedMenu.Price;
            }

            lvOrdered.ItemsSource = orderList;
            lvOrdered.Items.Refresh();

            lvMenu.SelectedItem = null;

            tableId.DataContext = this.SelectedSeat;
        }

        private void ResetTime()
        {
            leastOrderTime.Text = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
        }

        private void Count_Btn_Click(object sender, RoutedEventArgs e)
        {
            UIElementCollection siblingEl = (((sender as FrameworkElement)
                                              .Parent) as Grid)
                                              .Children;

            string foodName = (siblingEl[1] as TextBlock).Text;
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

            lvOrdered.Items.Refresh();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearOrderInfo();
        }

        private void ClearOrderInfo()
        {
            orderList.Clear();
            ClearTotal();
            lvOrdered.Items.Refresh();
        }

        private void ClearTotal()
        {
            Total = 0;
        }

        private void UpdateOrderInfo()
        {
            String orderInfo = "";

            foreach (Seat seat in App.seatData.lstSeat)
            {
                //주문 화면의 테이블 넘버와 일치할 때
                if (this.SelectedSeat.Id.Equals(seat.Id))
                {
                    foreach (Food food in seat.lstFood)
                    {
                        orderInfo += food.Name + "*" + food.Count + "\n";
                        seat.OrderInfo = orderInfo;
                    }

                    Console.WriteLine(seat.OrderInfo);
                }   
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
