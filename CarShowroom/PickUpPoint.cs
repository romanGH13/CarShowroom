using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    public  class PickUpPoint
    {
        static object locker = new object();

        ConcurrentDictionary<string, Client> WaitingClients = new();
        ConcurrentDictionary<string, Car> Cars = new();

        public void GiveCarToClient(Order order, Car car)
        {
            lock (locker)
            {
                bool isSucess = WaitingClients.TryRemove(order.ClientId, out Client? client);

                if (isSucess)
                {
                    client!.GetCarAndGoAway(car);
                }
                else
                {
                    Cars.AddOrUpdate(order.ClientId, car, (key, oldValue) => car);
                }
            }
            
        }

        public void MeetClient(Client client)
        {
            lock (locker)
            {
                bool isSucess = Cars.TryRemove(client.Id, out Car? car);

                if (isSucess)
                {
                    client.GetCarAndGoAway(car!);
                }
                else
                {
                    WaitingClients.AddOrUpdate(client.Id, client, (key, oldValue) => client);
                }
            }
        }
    }
}
