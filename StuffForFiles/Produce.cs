using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekSixDataBaseTextAssignment.StuffForFiles
{
    internal class Produce
    {
        //Learned from Assignment #5
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string UoM { get; set; }
        public DateTime SellByDate { get; set; }

        public Produce(string name, string location, decimal price, string uom, DateTime sellByDate)
        {
            Name = name;
            Location = location;
            Price = price;
            UoM = uom;
            SellByDate = sellByDate;
        }

        public override string ToString()
        {
            return $"{Name}|{Location}|{Price.ToString("0.00")}|{UoM}|{SellByDate.ToString("MM-dd-yyyy")}";
        }
    }
}
