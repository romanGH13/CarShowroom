using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom
{
    abstract public class Employee
    {
        public string Name { get; private set; }

        public bool IsWorkTime { get; set; } = true;

        private Thread? employeeThread;

        public Employee(string name)
        {
            Name = name;
        }

        public void StartWork()
        {
            employeeThread = new Thread(WorkLoop);
            employeeThread.Start();
        }

        public void WorkLoop()
        {
            while (IsWorkTime)
            {
                try
                {
                    DoWork();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } 
        }

        abstract protected void DoWork();

    }
}
