using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommand
{
    public class Invoker
    {
        ICommand Up;
        ICommand Down;
        ICommand On;
        ICommand Off;
        ICommand Down10;
        public Invoker(ICommand On, ICommand Off, ICommand Up, ICommand Down, ICommand Down10)
        {
            this.On = On;
            this.Off = Off;
            this.Down = Down;
            this.Up = Up;
            this.Down10 = Down10;
        }
        
        public void ClickDown10()
        {
            this.Down10.execute();
        }

        public void ClickOn()
        {
            this.On.execute();
        }
        public void ClickOff()
        {
            Off.execute();
        }
        public void ClickDown()
        {
            Down.execute();
        }
        public void ClickUp()
        {
            Up.execute();
        }
    }
}
