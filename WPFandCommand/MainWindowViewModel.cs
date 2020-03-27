using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPFandCommand
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public List<Ghost> allGhosts;
        List<GhostInvoker> allInvoker;

        public List<ICommand> CommandRowIsDone { get; set; }
        public List<ICommand> CommandRowIsUndoed { get; set; }

        private int commandRowCount;

        public int CommandRowCount
        {
            get { return commandRowCount; }
            set { commandRowCount = value;
                Notify();
            }
        }


        MainWindow gui;
        public MainWindowViewModel(MainWindow window)
        {
            gui = window;
            CommandRowIsDone = new List<ICommand>();
            CommandRowIsUndoed = new List<ICommand>();
            allInvoker = new List<GhostInvoker>();
            allGhosts = new List<Ghost>();
            allGhosts.Add(new Ghost());
            allGhosts.Add(new Ghost());

            foreach (Ghost ghost in allGhosts)
            {
                allInvoker.Add(new GhostInvoker(new GoOneLeftCommand(ghost), new GoOneRightCommand(ghost), new GoOneDownCommand(ghost), new GoOneUpCommand(ghost)));
            }
            for (int i = 0; i < allGhosts.Count; ++i)
            {
                
              
                gui.grid.Children.Add(allGhosts[i].spaceGhostPic);
            }

            gui.But_Master.Command = new GoTenLeftCommand(allInvoker,allGhosts, CommandRowIsDone);
            gui.But_Undo.Command = new UndoCommand(CommandRowIsDone, CommandRowIsUndoed);
            gui.But_Redo.Command = new RedoCommand(CommandRowIsDone, CommandRowIsUndoed);
        }



    }
    public class UndoCommand : ICommand
    {
       
        
        List<ICommand> CommandRowIsDone { get; set; }
        List<ICommand> CommandRowIsUndoed { get; set; }

        public UndoCommand(List<ICommand> CommandRowIsDone, List<ICommand> CommandRowIsUndoed)
        {
         
            this.CommandRowIsDone = CommandRowIsDone;
            this.CommandRowIsUndoed = CommandRowIsUndoed;

        }


        public void Execute()
        {
            if (CommandRowIsDone.Count > 0)
            {

                CommandRowIsDone[CommandRowIsDone.Count - 1].Unexecute();
                CommandRowIsUndoed.Add(CommandRowIsDone[CommandRowIsDone.Count - 1]);
                CommandRowIsDone.RemoveAt(CommandRowIsDone.Count - 1);
            }


        }


        public void Unexecute()
        {
            if (CommandRowIsUndoed.Count > 0)
            {
                CommandRowIsUndoed[CommandRowIsUndoed.Count - 1].Execute();
                CommandRowIsDone.Add(CommandRowIsUndoed[CommandRowIsUndoed.Count - 1]);
                CommandRowIsUndoed.RemoveAt(CommandRowIsUndoed.Count - 1);

            }
        }
        #region System.ICommand

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        #endregion
    }
    public class RedoCommand : ICommand
    {
        

        List<ICommand> CommandRowIsDone { get; set; }
        List<ICommand> CommandRowIsUndoed { get; set; }

        public RedoCommand(List<ICommand> CommandRowIsDone, List<ICommand> CommandRowIsUndoed)
        {
           
            this.CommandRowIsDone = CommandRowIsDone;
            this.CommandRowIsUndoed = CommandRowIsUndoed;

        }


        public void Execute()
        {
            if (CommandRowIsUndoed.Count > 0)
            {
                CommandRowIsUndoed[CommandRowIsUndoed.Count - 1].Execute();
                CommandRowIsDone.Add(CommandRowIsUndoed[CommandRowIsUndoed.Count - 1]);
                CommandRowIsUndoed.RemoveAt(CommandRowIsUndoed.Count - 1);

            }
        }


        public void Unexecute()
        {
            if (CommandRowIsDone.Count > 0)
            {

                CommandRowIsDone[CommandRowIsDone.Count - 1].Unexecute();
                CommandRowIsUndoed.Add(CommandRowIsDone[CommandRowIsDone.Count - 1]);
                CommandRowIsDone.RemoveAt(CommandRowIsDone.Count - 1);
            }
        }
        #region System.ICommand

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Execute();
        }

        #endregion
    }
}
