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
    public class OrderArgs : EventArgs
    {
        public Seat selectedSeat { get; set; }
    }

    public partial class OrderCtrl : UserControl, INotifyPropertyChanged
    {
        public OrderCtrl()
        {
            InitializeComponent();
            this.Loaded += OrderCtrl_Loaded;
            this.IsVisibleChanged += OrderCtrl_VisibleChanged;
        }

        private List<Food> lstOrder = new List<Food>();
        private int totalPrice = 0;
        private string orderTime = "";
        public delegate void OnOrderEventHandler(object sender, OrderArgs args);
        public event OnOrderEventHandler OrderEvent;
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

        private void OrderCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            vTableId.DataContext = this.SelectedSeat;

            LoadMenusByCategory();
        }

        private void OrderCtrl_VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //OrderCtrl의 가시성 체크
            if ((bool)e.NewValue)
                LoadSeatInfo();
        }

        private void Prev_Btn_Click(object sender, RoutedEventArgs e)
        {
            UpdateSeatInfo();

            OrderArgs args = new OrderArgs();
            args.selectedSeat = this.SelectedSeat;

            if (OrderEvent != null)
                OrderEvent(this, args);
        }

        private void LvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //선택된 카테고리가 없을 경우 전체 메뉴 출력
            if (lvCategory.SelectedItem == null)
            {
                LoadMenusByCategory();
                return;
            }

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

            UpdateOrderTime();

            if (!(lstOrder.Contains(selectedMenu)))
                AddToLstOrder(selectedMenu);

            lvMenu.SelectedItem = null;
        }

        private void Count_Btn_Click(object sender, RoutedEventArgs e)
        {
            UIElementCollection siblingEls = (((sender as FrameworkElement)
                                              .Parent) as Grid)
                                              .Children;

            string menuName = (siblingEls[1] as TextBlock).Text;
            string op = (sender as Button).Content.ToString();

            foreach (Food menu in lstOrder)
            {
                if (menu.Name.Equals(menuName))
                {
                    CountMenu(menu, op);
                    break;
                }
            }

            lvOrdered.Items.Refresh();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(
                "전체 취소하시겠습니까?", "전체 취소", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes) {

                MessageBox.Show("전체 취소합니다", "전체 취소", 
                    MessageBoxButton.OK ,MessageBoxImage.Information);

                ClearOrderInfo();
            }
        }

        private void LoadMenusByCategory(string category = "All")
        {
            List<Food> lstMenuToShow = new List<Food>();

            foreach (Food food in App.foodData.lstMenu)
            {
                if (food.Category.ToString().Equals(category) ||
                    category.Equals("All")) {

                    lstMenuToShow.Add(food);
                }
            }

            lvMenu.ItemsSource = lstMenuToShow;
        }

        private void LoadSeatInfo()
        {
            if (this.SelectedSeat == null)
                return;

            InitLvCategory();
            vTableId.DataContext = this.SelectedSeat;

            foreach (Seat seat in App.seatData.lstSeat)
            {
                if (IsSelectedSeat(seat.Id))
                {
                    ClearTotalPrice();

                    OrderTime = seat.OrderTime;
                    lstOrder = seat.lstFood;

                    foreach (Food food in lstOrder)
                        TotalPrice += food.Price * food.Count;
                }
            }

            UpdateLvOrdered(lstOrder);
        }

        private void UpdateSeatInfo()
        {
            foreach (Seat seat in App.seatData.lstSeat)
            {
                if (IsSelectedSeat(seat.Id))
                {
                    seat.OrderTime = "";
                    seat.OrderInfo = "";
                    seat.lstFood = lstOrder;

                    foreach (Food food in seat.lstFood)
                    {
                        seat.OrderInfo += food.Name + "*" + food.Count + "\n";
                        seat.OrderTime = OrderTime;
                    }
                }
            }
        }

        private void AddToLstOrder(Food menu)
        {
            menu.Count = 1;
            lstOrder.Add(menu);

            TotalPrice += menu.Price;

            UpdateLvOrdered(lstOrder);
        }

        private void CountMenu(Food menu, string op)
        {
            UpdateOrderTime();
            switch (op)
            {
                case "+":
                    menu.Count++;
                    TotalPrice += menu.Price;

                    break;

                case "-":
                    menu.Count--;
                    TotalPrice -= menu.Price;

                    if (menu.Count < 1)
                        lstOrder.Remove(menu);

                    break;
            }
        }

        private void InitLvCategory()
        {
            lvCategory.SelectedItem = null;
            lvCategory.Items.Refresh();
        }

        private void UpdateLvOrdered(List<Food> lstOrder)
        {
            lvOrdered.ItemsSource = lstOrder;
            lvOrdered.Items.Refresh();
        }

        private bool IsSelectedSeat(string seatId)
        {
            if (this.SelectedSeat.Id.Equals(seatId))
                return true;

            return false;
        }

        private void UpdateOrderTime()
        {
            OrderTime = CurrentTime();
        }

        private string CurrentTime()
        {
            return DateTime.Now.ToString("yyyy.MM.dd HH시 mm분");
        }

        private void ClearOrderInfo()
        {
            lstOrder.Clear();
            ClearTotalPrice();
            lvOrdered.Items.Refresh();
        }

        private void ClearTotalPrice()
        {
            TotalPrice = 0;
        }
    }

}