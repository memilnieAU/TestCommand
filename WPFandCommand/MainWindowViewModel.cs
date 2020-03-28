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
using System.Windows.Input;

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
        public List<ICommand> PathList { get; set; }
        private int commandRowCount;

        public int CommandRowCount
        {
            get { return commandRowCount; }
            set
            {
                commandRowCount = value;
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
            PathList = new List<ICommand>();
            foreach (Ghost ghost in allGhosts)
            {
                allInvoker.Add(new GhostInvoker(new GoOneLeftCommand(ghost, CommandRowIsDone), new GoOneRightCommand(ghost, CommandRowIsDone), new GoOneDownCommand(ghost, CommandRowIsDone), new GoOneUpCommand(ghost, CommandRowIsDone)));
                //KeyBinding OpenCmdKeyBinding = new KeyBinding(new GoOneLeftCommand(ghost), Key.A, ModifierKeys.Control);
                gui.grid.Children.Add(ghost.spaceGhostPic);
            }
            

            gui.InputBindings.Add(new KeyBinding(new GoTenLeftCommand(allInvoker, allGhosts, CommandRowIsDone), Key.Left, ModifierKeys.None));
            gui.InputBindings.Add(new KeyBinding(new GoTenRightCommand(allInvoker, allGhosts, CommandRowIsDone), Key.Right, ModifierKeys.None));
            gui.InputBindings.Add(new KeyBinding(new GoTenUpCommand(allInvoker, allGhosts, CommandRowIsDone), Key.Up, ModifierKeys.None));
            gui.InputBindings.Add(new KeyBinding(new GoTenDownCommand(allInvoker, allGhosts, CommandRowIsDone),Key.Down,ModifierKeys.None));
            gui.InputBindings.Add(new KeyBinding(new UndoCommand(CommandRowIsDone, CommandRowIsUndoed), Key.X, ModifierKeys.Control));
            gui.InputBindings.Add(new KeyBinding(new DoPathCommand(PathList), Key.C, ModifierKeys.Control));
            gui.But_Master.Command = new GoTenLeftCommand(allInvoker, allGhosts, CommandRowIsDone);
            gui.But_Undo.Command = new UndoCommand(CommandRowIsDone, CommandRowIsUndoed);
            gui.But_Redo.Command = new RedoCommand(CommandRowIsDone, CommandRowIsUndoed);
            PathList.Add(GoTenLeft());
            PathList.Add(GoTenLeft());
            PathList.Add(GoTenRight());
            PathList.Add(GoTenRight());
            PathList.Add(GoTenDown());
            PathList.Add(GoTenLeft());
            PathList.Add(GoTenLeft());
            PathList.Add(GoTenRight());
            PathList.Add(GoTenRight());
            PathList.Add(GoTenDown());

        }
        ICommand GoTenLeft()
        {
             
            return new GoTenLeftCommand(allInvoker, allGhosts, CommandRowIsDone);
        }
        ICommand GoTenRight()
        {

            return new GoTenRightCommand(allInvoker, allGhosts, CommandRowIsDone);
        }
        ICommand GoTenDown()
        {

            return new GoTenDownCommand(allInvoker, allGhosts, CommandRowIsDone);
        }
    }
    public class DoPathCommand : ICommand
    {
        List<ICommand> PathList;
        public DoPathCommand(List<ICommand> PathList)
        {
            this.PathList = PathList;
        }
        public void Execute()
        {
            foreach (ICommand command in PathList)
            {
                System.Threading.Thread.Sleep(200);
                command.Execute();
            }
        }


        public void Unexecute()
        {
            throw new NotImplementedException();
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
            for (int i = 0; i < 25; i++)
            {

            if (CommandRowIsDone.Count > 0)
            {

                CommandRowIsDone[CommandRowIsDone.Count - 1].Unexecute();
                CommandRowIsUndoed.Add(CommandRowIsDone[CommandRowIsDone.Count - 1]);
                CommandRowIsDone.RemoveAt(CommandRowIsDone.Count - 1);
            }
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
