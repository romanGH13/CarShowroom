using System;
using System.Collections.Concurrent;

namespace CarShowroom
{
    internal class Program
    {
        // Настройка количества клиентов и сотрудников
        public static int ClientsCount = 1000;
        public static int ManagersCount = 4;
        public static int MechanicsCount = 5;

        // Настройка времени выполнения процесса
        public static int tOrder = 1;
        public static int tClient = 1;
        public static int tBuild = 1;

        static BlockingCollection<Client> Clients = new BlockingCollection<Client>(new ConcurrentQueue<Client>());

        static List<Mechanic> Mechanics = new List<Mechanic>();
        static List<Manager> Managers = new List<Manager>();

        static PneumaticTube PneumaticTube = new PneumaticTube();
        static PickUpPoint PickUpPoint = new PickUpPoint();

        static DateTime StartedTime;
        static DateTime FinishedTime;

        static void Main(string[] args)
        {
            StartedTime = DateTime.Now;

            FillData();

            StartWork();

            Console.ReadLine();

            StopWork();

            FinishedTime = DateTime.Now;

            Thread.Sleep(10);

            Console.WriteLine("Client processed count: " + (ClientsCount - Clients.Count));
            Console.WriteLine("In time: " + (FinishedTime - StartedTime));

        }

        static void StartWork()
        {
            foreach (var manager in Managers)
            {
                Thread myThread = new Thread(manager.StartWork);
                myThread.Start();
            }

            foreach (var mechanic in Mechanics)
            {
                Thread myThread = new Thread(mechanic.StartWork);
                myThread.Start();
            }
        }

        static void StopWork()
        {
            foreach (var manager in Managers)
            {
                manager.IsWorkTime = false;
            }

            foreach (var mechanic in Mechanics)
            {
                mechanic.IsWorkTime = false;
            }
        }

        static void FillData()
        {

            for (int i = 0; i < ManagersCount; i++)
            {
                Managers.Add(new Manager($"Manager #{i}", tOrder, Clients, PneumaticTube, PickUpPoint));
            }

            for (int i = 0; i < MechanicsCount; i++)
            {
                Mechanics.Add(new Mechanic($"Mechanic #{i}", tBuild, PickUpPoint, PneumaticTube));
            }

            for (int i = 0; i < ClientsCount; i++)
            {
                Clients.Add(new Client(tClient));
            }
        }
    }
}