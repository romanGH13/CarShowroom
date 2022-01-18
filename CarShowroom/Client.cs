using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    public class Client
    {
        public string Id { get; set; }

        public int MoveTime { get; private set; }

        public Client(int moveTime)
        {
            Id = Guid.NewGuid().ToString();
            MoveTime = moveTime;
        }

        public void MoveToPickUpPoint(PickUpPoint pickUpPoint)
        {
            Thread.Sleep(MoveTime);
            pickUpPoint.MeetClient(this);
        }

        public void GetCarAndGoAway(Car car)
        {
            Console.WriteLine($"Client get the {car.Color} {car.Brand}!");
        }
    }
}
