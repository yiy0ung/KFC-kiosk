using KfcKiosk.DataSource;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;
using System.Text;
using Kfc.Core;

namespace KfcKiosk
{
    public partial class App : Application
    {
        public static FloorDataSource floorData = new FloorDataSource();
        public static SeatDataSource seatData = new SeatDataSource();
        public static FoodDataSource foodData = new FoodDataSource();
        public static AnalysisDataSource analysisData = new AnalysisDataSource();

        public static TCPClient tc = new TCPClient();

    }
}
