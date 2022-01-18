using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    internal class Mechanic : Employee
    {
        private readonly PneumaticTube PneumaticTube;
        private readonly PickUpPoint PickUpPoin;

        public int BuildTime { get; private set; }

        public Mechanic(string name, int buildTime, PickUpPoint pickUpPoin, PneumaticTube pneumaticTube) : base(name)
        {
            BuildTime = buildTime;
            PneumaticTube = pneumaticTube;
            PickUpPoin = pickUpPoin;
        }

        public Car BuildCar(string color, string brand)
        {
            Thread.Sleep(BuildTime);

            return new Car(color, brand);
        }

        protected override void DoWork()
        {
            Order? order = PneumaticTube.ReceiveOrder();

            if (order == null)
                return;

            Car car = BuildCar(order.CarColor, order.CarBrand);

            PickUpPoin.GiveCarToClient(order, car);
        }
    }
}
