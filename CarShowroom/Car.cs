using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    public class Car
    {
        public string Color { get; private set; }

        public string Brand { get; private set; }

        public Car(string color, string brand)
        {
            Color = color;
            Brand = brand;
        }
    }
}
