using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFandCommand
{
    public interface ICommand : System.Windows.Input.ICommand
    {
        void Execute();
        void Unexecute();


    }
}
