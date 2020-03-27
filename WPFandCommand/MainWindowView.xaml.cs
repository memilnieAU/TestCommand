using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFandCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel(this);
            DataContext = mainWindowViewModel;
            
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(string.Format("You clicked on the {0}. button.", (sender as Button).Tag));
            // int nr = (sender as Button).Content
            String nr = (sender as Button).Content.ToString();
            Char nr1 = nr[0];
            int nR = Convert.ToInt32(nr[0].ToString());
            (sender as Button).Margin = new Thickness(mainWindowViewModel.allGhosts[nR].Pos_x, mainWindowViewModel.allGhosts[nR].Pos_y, 0,0);
            
        }

      
          
    }
}
