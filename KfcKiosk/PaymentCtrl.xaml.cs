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
        public PaymentCtrl()
        {
            InitializeComponent();
            this.Loaded += PaymentCtrl_Loaded;
            this.IsVisibleChanged += PaymentCtrl_VisibleChanged;
        }

        private List<Food> orderList = new List<Food>();
        private int totalPrice = 0;
        private string orderTime = "";
        public delegate void OnPayEventHandler(object sender, PayArgs args);
        public event OnPayEventHandler PayEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public Seat SelectedSeat { get; set; }

        public int TotalPrice
        {
            get => totalPrice;
            set {
                totalPrice = value;
                NotifyPropertyChanged(nameof(TotalPrice));
            }
        }
        
        public string OrderTime
        {
            get => orderTime;
            set {
                orderTime = value;
                NotifyPropertyChanged(nameof(OrderTime));
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PaymentCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            vTableId.DataContext = this.SelectedSeat;

            LoadMenusByCategory();
        }

        private void PaymentCtrl_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //PaymentCtrl의 가시성 체크
            if ((bool)e.NewValue)
                LoadSeatInfo();
        }

        private void InitLvCategory() {
            lvCategory.SelectedItem = null;
            lvCategory.Items.Refresh();
        }

        private bool IsSelectedSeat(string seatId)
        {
            if (this.SelectedSeat.Id.Equals(seatId))
                return true;
            return false;
        }

        private void LoadSeatInfo()
        {
            if (this.SelectedSeat == null)
                return;

            vTableId.DataContext = this.SelectedSeat;
            InitLvCategory();

            foreach (Seat seat in App.seatData.lstSeat)
            {
                if (IsSelectedSeat(seat.Id))
                {
                    ClearTotalPrice();
                    OrderTime = seat.OrderTime;

                    orderList = seat.lstFood;
                    foreach (Food food in orderList)
                        TotalPrice += food.Price * food.Count;
                }
            }

            lvOrdered.ItemsSource = orderList;
            lvOrdered.Items.Refresh();
        }

        private void Prev_Ctrl(object sender, RoutedEventArgs e)
        {
            UpdateSeatInfo();

            PayArgs args = new PayArgs();
            args.selectedSeat = this.SelectedSeat;

            if (PayEvent != null)
                PayEvent(this, args);
        }

        private void UpdateSeatInfo()
        {
            string orderInfo = "";

            foreach (Seat seat in App.seatData.lstSeat)
            {
                if (IsSelectedSeat(seat.Id))
                {
                    foreach (Food food in seat.lstFood)
                    {
                        orderInfo += food.Name + "*" + food.Count + "\n";

                        seat.OrderInfo = orderInfo;
                        seat.OrderTime = OrderTime;
                    }
                }
            }
        }

        private void LoadMenusByCategory(string category = "All")
        {
            List<Food> lstMenuToShow = new List<Food>();

            foreach (Food food in App.foodData.lstMenu)
            {
                if (food.Category.ToString().Equals(category) ||
                    category.Equals("All"))
                {
                    lstMenuToShow.Add(food);
                }
            }

            lvMenu.ItemsSource = lstMenuToShow;
        }

        private void LvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCategory.SelectedItem == null) { LoadMenusByCategory(); return; }

            string category = ((ListBoxItem)(lvCategory.SelectedItem))
                                      .Content
                                      .ToString();

            LoadMenusByCategory(category);
        }

        private void LvMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food selectedMenu = ((Food)lvMenu.SelectedItem);

            if (selectedMenu == null)
                return;

            UpdateTime();
            if (!(orderList.Contains(selectedMenu)))
            {
                selectedMenu.Count = 1;
                orderList.Add(selectedMenu);

                TotalPrice += selectedMenu.Price;
            }

            lvOrdered.ItemsSource = orderList;
            lvOrdered.Items.Refresh();
            lvMenu.SelectedItem = null;
        }

        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy.MM.dd HH:mm");
        }

        private void UpdateTime()
        {
            OrderTime = GetCurrentTime();
        }

        private void Count_Btn_Click(object sender, RoutedEventArgs e)
        {
            UIElementCollection siblingEl = (((sender as FrameworkElement)
                                              .Parent) as Grid)
                                              .Children;

            string foodName = (siblingEl[1] as TextBlock).Text;
            string op = (sender as Button).Content.ToString();

            foreach (Food menu in orderList)
            {
                if (menu.Name.Equals(foodName))
                {
                    if (op.Equals("+"))
                    {
                        menu.Count++;
                        TotalPrice += menu.Price;
                    }

                    else if (op.Equals("-"))
                    {
                        menu.Count--;
                        TotalPrice -= menu.Price;

                        if (menu.Count < 1)
                            orderList.Remove(menu);
                    }

                    break;
                }
            }

            lvOrdered.Items.Refresh();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearOrderList();
        }

        private void ClearOrderList()
        {
            orderList.Clear();
            ClearTotalPrice();
            lvOrdered.Items.Refresh();
        }

        private void ClearTotalPrice()
        {
            TotalPrice = 0;
        }
    }
}
