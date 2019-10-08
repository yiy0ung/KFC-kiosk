using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KfcKiosk.DataSource
{
    public class StatDataSource
    {
        List<Food> lstPaidFood = new List<Food>();

        public void AppendPaidFoods(List<Food> paidFood)
        {
            lstPaidFood.AddRange(paidFood);
        }


        public int GetTotalPrice()
        {
            int totalPrice = 0;

            foreach (Food food in lstPaidFood)
            {
                totalPrice += food.Price;
            }

            return totalPrice;
        }

        // 메뉴별 판매 금액 조회
        //public List<Food> GetPriceByType()
        //{

        //}
    }
}
