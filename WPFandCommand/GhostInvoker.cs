using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFandCommand
{
    public class GhostInvoker
    {
        public ICommand Left1;
        ICommand Right1;
        ICommand Up1;
        ICommand Down1;
      
        public GhostInvoker(ICommand Left1, ICommand Right1, ICommand Up1, ICommand Down1)
        {
            this.Left1 = Left1;
            this.Right1 = Right1;
            this.Up1 = Up1;
            this.Down1 = Down1;
          
        }
        public GhostInvoker(ICommand Left1)
        {
            this.Left1 = Left1;
        }
        
       

        public void GoLeft()
        {
            Left1.Execute();
            
        }
        public void GoRight()
        {
            Right1.Execute();
        }
        public void GoUp()
        {
            Up1.Execute();
        }
        public void GoDown()
        {
            Down1.Execute();
        }
    }
}
