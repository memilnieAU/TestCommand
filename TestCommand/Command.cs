using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestCommand
{
    public class OnCommand : ICommand
    {

        ReciverLight light;
        public OnCommand(ReciverLight light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.On();
        }

        public void unexecute()
        {
            light.Off();
        }
    }



    public class OffCommand : ICommand
    {
        ReciverLight light;
        public OffCommand(ReciverLight light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.Off();
        }

        public void unexecute()
        {
            light.On();
        }
       
    }
    public class UpCommand : ICommand
    {
        ReciverLight light;
        public UpCommand(ReciverLight light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.Up();
        }

        public void unexecute()
        {
            light.Down();
        }

    }
    public class DownCommand : ICommand
    {
        ReciverLight light;
        public DownCommand(ReciverLight light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.Down();
        }

        public void unexecute()
        {
            light.Up();
        }

    }
    public class Down10Command : ICommand
    {
        ReciverLight light;
        List<ICommand> list;
        public Down10Command(ReciverLight light)
        {
            this.light = light;
            list = new List<ICommand>();
            CreatList();
        }
        private void CreatList()
        {
            for (int i = 0; i < 10; i++)
            {
                list.Add(new DownCommand(light));

            }

        }
        public void execute()
        {
            foreach (ICommand item in list)
            {
                item.execute();
            }
           
        }

        public void unexecute()
        {
            foreach (ICommand item in list)
            {
                item.unexecute();
            }

        }

    }
}
