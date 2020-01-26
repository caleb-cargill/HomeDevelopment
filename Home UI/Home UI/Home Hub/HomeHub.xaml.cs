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
using System.Windows.Shapes;

namespace Home_UI.Home_Hub
{
    /// <summary>
    /// Interaction logic for HomeHub.xaml
    /// </summary>
    public partial class HomeHub : Window
    {
        public HomeHub()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Launches the to do list tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnToDoList_Click(object sender, RoutedEventArgs e)
        {
            // Probably need to only let one window open at a time
            ToDoList toDo = new ToDoList();
            toDo.Show();
        }

        private void BtnDevExplorer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnQuickAccess_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBudget_Click(object sender, RoutedEventArgs e)
        {
            Home_UI.Home_Hub.BudgetBuddy bb = new BudgetBuddy();
            bb.Show();
        }

        private void BtnHomeworkPlanner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNotes_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
