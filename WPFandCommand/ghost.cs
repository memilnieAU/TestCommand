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
       
        public Image spaceGhostPic;
        public static int TotalAmount { get; set; }
        public int Id { get; set; }
        private int pos_x;

        public int Pos_x
        {
            get { return pos_x; }
            set { pos_x = value; Notify(); update(spaceGhostPic); }
        }

        void update(Image image)
        {
            if (image != null)
            {
            image.Margin = new Thickness(Pos_x, Pos_y, 0, 0);

            }
        }


        private int pos_y;


        public int Pos_y
        {
            get { return pos_y; }
            set { pos_y = value; Notify(); update(spaceGhostPic); }
        }

        public Ghost()
        {
            Id = TotalAmount;
            Pos_x = Id*10;
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
            Pos_x--;
        }
        public void Right1()
        {
            Pos_x++;
        }
        public void Up1()
        {
            Pos_y++;
        }
        public void Down1()
        {
            Pos_y--;
        }

    }

    public class GoOneLeftCommand : ICommand
    {
        List<ICommand> CommandRowIsDone { get; set; }
        
        Ghost ghost;
        public GoOneLeftCommand(Ghost ghost, List<ICommand> CommandRowIsDone)
        {
            this.ghost = ghost;
        this.CommandRowIsDone = CommandRowIsDone;
        }
            
        public void Execute()
        {
            CommandRowIsDone.Add(this);
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
        List<ICommand> CommandRowIsDone { get; set; }

        Ghost ghost;
        public GoOneRightCommand(Ghost ghost, List<ICommand> CommandRowIsDone)
        {
            this.ghost = ghost;
            this.CommandRowIsDone = CommandRowIsDone;
        }

        public void Execute()
        {
            CommandRowIsDone.Add(this);
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
        List<ICommand> CommandRowIsDone { get; set; }

        Ghost ghost;
        public GoOneUpCommand(Ghost ghost, List<ICommand> CommandRowIsDone)
        {
            this.ghost = ghost;
            this.CommandRowIsDone = CommandRowIsDone;
        }



        public void Execute()
        {
            CommandRowIsDone.Add(this);
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
        List<ICommand> CommandRowIsDone { get; set; }

        Ghost ghost;
        public GoOneDownCommand(Ghost ghost, List<ICommand> CommandRowIsDone)
        {
            this.ghost = ghost;
            this.CommandRowIsDone = CommandRowIsDone;

        }


        public void Execute()
        {
            CommandRowIsDone.Add(this);
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
        List<ICommand> CommandRowIsDone { get; set; }

        List<GhostInvoker> ghostInvoker;
        List<Ghost> ghost;
        


        public GoTenLeftCommand(List<GhostInvoker> ghostInvoker, List<Ghost> ghost, List<ICommand> CommandRowIsDone)
        {
            this.ghostInvoker = ghostInvoker;
            this.ghost = ghost;
            this.CommandRowIsDone = CommandRowIsDone;

        }


        public void Execute()
        {
        
            for (int i = 0; i < 50; i++)
            {
                
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoLeft();

                }

                
            }


        }


        public void Unexecute()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoRight();

                }

           
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
    public class GoTenRightCommand : ICommand
    {
        List<GhostInvoker> ghostInvoker;
        List<Ghost> ghost;
        List<ICommand> CommandRow;


        public GoTenRightCommand(List<GhostInvoker> ghostInvoker, List<Ghost> ghost, List<ICommand> list)
        {
            this.ghostInvoker = ghostInvoker;
            this.ghost = ghost;
            CommandRow = list;

        }


        public void Execute()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoRight();

                }


            }


        }


        public void Unexecute()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoLeft();

                }


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
    public class GoTenUpCommand : ICommand
    {
        List<GhostInvoker> ghostInvoker;
        List<Ghost> ghost;
        List<ICommand> CommandRow;


        public GoTenUpCommand(List<GhostInvoker> ghostInvoker, List<Ghost> ghost, List<ICommand> list)
        {
            this.ghostInvoker = ghostInvoker;
            this.ghost = ghost;
            CommandRow = list;

        }


        public void Execute()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoUp();

                }


            }


        }


        public void Unexecute()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoDown();

                }


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
    public class GoTenDownCommand : ICommand
    {
        List<GhostInvoker> ghostInvoker;
        List<Ghost> ghost;
        List<ICommand> CommandRow;


        public GoTenDownCommand(List<GhostInvoker> ghostInvoker, List<Ghost> ghost, List<ICommand> list)
        {
            this.ghostInvoker = ghostInvoker;
            this.ghost = ghost;
            CommandRow = list;

        }


        public void Execute()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoDown();

                }


            }

        }


        public void Unexecute()
        {
            for (int i = 0; i < 50; i++)
            {
                for (int x = 0; x < ghostInvoker.Count; x++)
                {
                    ghostInvoker[x].GoUp();

                }


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
