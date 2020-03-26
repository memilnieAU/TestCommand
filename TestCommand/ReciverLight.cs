using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommand
{
    public class ReciverLight
    {
        public int lightlevel;
        public ReciverLight()
        {
            lightlevel = 0;
        }

        public void On()
        {
            lightlevel = 100;
        }
        public void Off()
        {
            lightlevel = 0;
        }
        public void Up()
        {
            if (lightlevel <= 100)
            {
            lightlevel++; ;

            }
        }
        public void Down()
        {
            if (lightlevel >= 0)
            {
            lightlevel--;

            }
        }
    }
}
