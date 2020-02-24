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

namespace Project_Core
{
    /// <summary>
    /// Interaction logic for HomeHub.xaml
    /// </summary>
    public partial class Core : Window
    {
        public Core()
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

        /// <summary>
        /// Launches the budgetting tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBudget_Click(object sender, RoutedEventArgs e)
        {
            BudgetBuddyView bb = new BudgetBuddyView();
            bb.Show();
        }
    }
}
