using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    public class Order
    {
        public string ClientId { get; set; }

        public string CarColor { get; private set; }

        public string CarBrand { get; private set; }

        public Order(string clientId, string color, string brand)
        {
            ClientId = clientId;
            CarColor = color;   
            CarBrand = brand;
        }
    }
}
