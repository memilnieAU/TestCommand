using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommand
{
    class Program
    {

        static ReciverLight light;
        static Invoker invoker;
        static void Main(string[] args)
        {
            light = new ReciverLight();
            Console.WriteLine(light.lightlevel);
            invoker = new Invoker(new OnCommand(light), new OffCommand(light),new UpCommand(light),new DownCommand(light), new Down10Command(light));
            string knap = Console.ReadLine();
            while (knap != "N")
            {
                if (knap == "q")
                {
                    invoker.ClickOn();
                }
                else if (knap == "w")
                {
                    invoker.ClickOff();
                }
                else if (knap == "e")
                {
                    invoker.ClickUp();
                }
                else if (knap == "r")
                {
                    invoker.ClickDown();
                }
                else if (knap == "t")
                {
                    invoker.ClickDown10();
                }
                Console.WriteLine(light.lightlevel);
                knap = Console.ReadLine();
            }
        }
    }
}
