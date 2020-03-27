using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFandCommand
{
    public class Ghost : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName]string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public Button button;
        public Image spaceGhostPic;
        public static int TotalAmount { get; set; }
        public int Id { get; set; }
        private int pos_x;

        public int Pos_x
        {
            get { return pos_x; }
            set { pos_x = value-100; Notify(); }
        }

        private int pos_y;


        public int Pos_y
        {
            get { return pos_y; }
            set { pos_y = value-100; Notify(); }
        }

        public Ghost()
        {
            Id = TotalAmount;
            Pos_x = 0;
            Pos_y = 0;
            TotalAmount++;
           



            spaceGhostPic = new Image()
            {
                Tag = Id

                ,
                Margin = new Thickness(Pos_x, Pos_y, 0, 0)
                ,
                Width = 30
                ,
                Height = 20,
                Source = new BitmapImage(new Uri(@"C:\Users\memil\source\repos\TestCommand\WPFandCommand\bin\Debug\SpaceGhost.png", UriKind.RelativeOrAbsolute)),
                
            };


        }
        public void Left1()
        {
            pos_x--;
        }
        public void Right1()
        {
            pos_x++;
        }
        public void Up1()
        {
            pos_y--;
        }
        public void Down1()
        {
            pos_y++;
        }

    }

    public class GoOneLeftCommand : ICommand
    {
        Ghost ghost;
        public GoOneLeftCommand(Ghost ghost)
        {
            this.ghost = ghost;
        }


        public void Execute()
        {
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posX Før:{ghost.Pos_x} ");
            ghost.Left1();
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posX Efter:{ghost.Pos_x} ");
        }


        public void Unexecute()
        {
            ghost.Right1();
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
    public class GoOneRightCommand : ICommand
    {
        Ghost ghost;
        public GoOneRightCommand(Ghost ghost)
        {
            this.ghost = ghost;
        }

        public void Execute()
        {
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posX Før:{ghost.Pos_x} ");
            ghost.Right1();
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posX Efter:{ghost.Pos_x} ");
        }
        public void Unexecute()
        {
            ghost.Left1();
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
    public class GoOneUpCommand : ICommand
    {
        Ghost ghost;
        public GoOneUpCommand(Ghost ghost)
        {
            this.ghost = ghost;
        }



        public void Execute()
        {
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posY Før:{ghost.Pos_y} ");
            ghost.Up1();
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posY Efter:{ghost.Pos_y} ");
        }

        public void Unexecute()
        {
            ghost.Down1();
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
    public class GoOneDownCommand : ICommand
    {
        Ghost ghost;
        public GoOneDownCommand(Ghost ghost)
        {
            this.ghost = ghost;
        }


        public void Execute()
        {
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posY Før:{ghost.Pos_y} ");
            ghost.Down1();
            System.Diagnostics.Debug.WriteLine($"{ghost.Id} posY Efter:{ghost.Pos_y} ");
        }

        public void Unexecute()
        {
            ghost.Up1();
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
    public class GoTenLeftCommand : ICommand
    {
        List<GhostInvoker> ghostInvoker;
        List<Ghost> ghost;
        List<ICommand> CommandRow;


        public GoTenLeftCommand(List<GhostInvoker> ghostInvoker, List<Ghost> ghost, List<ICommand> list)
        {
            this.ghostInvoker = ghostInvoker;
            this.ghost = ghost;
            CommandRow = list;

        }


        public void Execute()
        {
            CommandRow.Add(this);
            for (int i = 0; i < ghostInvoker.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine($"{ghost[i].Id} posX Før:{ghost[i].Pos_x} ");
                for (int x = 0; x < 50; x++)
                {
                    ghostInvoker[i].GoLeft();

                }

                System.Diagnostics.Debug.WriteLine($"{ghost[i].Id} posX Efter:{ghost[i].Pos_x} ");

            }

            foreach (Ghost ghost in ghost)
            {
                ghost.spaceGhostPic.Margin = new Thickness(ghost.Pos_x, ghost.Pos_y, 0, 0);
            }
        }


        public void Unexecute()
        {
            for (int i = 0; i < ghostInvoker.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine($"{ghost[i].Id} posX Før:{ghost[i].Pos_x} ");
                for (int x = 0; x < 50; x++)
                {
                    ghostInvoker[i].GoRight();

                }
                System.Diagnostics.Debug.WriteLine($"{ghost[i].Id} posX Efter:{ghost[i].Pos_x} ");

            }
            foreach (GhostInvoker ghostInvoke in ghostInvoker)
            {
            }
            foreach (Ghost ghost in ghost)
            {
                ghost.spaceGhostPic.Margin = new Thickness(ghost.Pos_x, ghost.Pos_y, 0, 0);
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
