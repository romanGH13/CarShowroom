using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    public class PneumaticTube
    {
        public BlockingCollection<Order> Queue { get; private set; }

        public PneumaticTube()
        {
            Queue = new BlockingCollection<Order>(new ConcurrentQueue<Order>());
        }

        public void SendOrder(Order order)
        {
            Queue.TryAdd(order);
        }

        public Order? ReceiveOrder()
        {
            bool isSucess = Queue.TryTake(out Order? order, Timeout.Infinite);

            if (isSucess)
                return order;

            return null;
        }
    }
}
