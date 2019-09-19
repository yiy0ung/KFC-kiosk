using KfcKiosk.DataSource;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KfcKiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static SeatDataSource seatData = new SeatDataSource();
        public static FoodDataSource foodData = new FoodDataSource();
    }
}
