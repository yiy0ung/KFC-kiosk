using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KfcKiosk.DataSource
{
    public class AnalysisDataSource
    {
        public List<Food> lsPaidFood = new List<Food>();


        public void AppendPaidFoods(List<Food> lsSoldFood)
        {
            foreach (Food soldFood in lsSoldFood)
            {
                bool isAppended = false;

                foreach (Food paidFood in lsPaidFood)
                {
                    if (paidFood.Name == soldFood.Name)
                    {
                        isAppended = true;
                        paidFood.Count += soldFood.Count;
                    }
                }

                if (isAppended == false)
                {
                    lsPaidFood.Add(soldFood);
                }
            }
        }

        public int GetFoodTotalPrice()
        {
            int totalPrice = 0;

            foreach (Food food in lsPaidFood)
            {
                totalPrice += food.Price * food.Count;
            }

            return totalPrice;
        }
    }
}
