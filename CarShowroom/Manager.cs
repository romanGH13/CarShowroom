using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    internal class Manager : Employee
    {
        private List<string> AvailableColours = new List<string>() { "red", "green", "blue", "black" };
        private List<string> AvailableBrands = new List<string>() { "bmw", "honda", "mazda", "ford" };

        private BlockingCollection<Client> Clients;

        private PneumaticTube PneumaticTube;
        private PickUpPoint PickUpPoint;

        public int OrderProcessingTime { get; private set; }

        public Manager(string name, int orderProcessingTime, BlockingCollection<Client> clients, PneumaticTube pneumaticTube, PickUpPoint pickUpPoint) : base(name)
        {
            OrderProcessingTime = orderProcessingTime;
            Clients = clients;
            PneumaticTube = pneumaticTube;
            PickUpPoint = pickUpPoint;
        }

        public Order MakeOrder(Client client)
        {
            Thread.Sleep(OrderProcessingTime);

            Random random = new Random();

            int colourId = random.Next(AvailableColours.Count);
            int brandId = random.Next(AvailableBrands.Count);

            return new Order(client.Id, AvailableColours[colourId], AvailableBrands[brandId]);
        }

        protected override void DoWork()
        {
            bool isSuccess = Clients.TryTake(out Client? client, Timeout.Infinite);

            if (isSuccess)
            {
                Order order = MakeOrder(client!);

                PneumaticTube.SendOrder(order);

                client!.MoveToPickUpPoint(PickUpPoint);

                Console.WriteLine($"Client processed by {Name}!");
            }
        }
    }
}
