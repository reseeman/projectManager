using Project_Manager.dbfiles;
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

namespace Project_Manager
{
    /// <summary>
    /// Логика взаимодействия для windowWorkers.xaml
    /// </summary>
    public partial class windowWorkers : Window
    {
        public windowWorkers()
        {
            InitializeComponent();
            OdbConnectHelper.entObj = new projdbEntities();
            dataGrid.ItemsSource = OdbConnectHelper.entObj.workers.ToList();

        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            windowInfoWorkers wndInfWrk = new windowInfoWorkers();
            wndInfWrk.ShowDialog();
        }
    }
}
