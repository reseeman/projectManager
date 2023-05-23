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
    /// Логика взаимодействия для windowInfoWorkers.xaml
    /// </summary>
    public partial class windowInfoWorkers : Window
    {
        int mode;
        public windowInfoWorkers()
        {
            InitializeComponent();
            mode = 0;
        }

        public windowInfoWorkers(tasks tsks, workers wrkrs)
        {
            InitializeComponent();
            tblNameWorker.Text = wrkrs.nameWorker;

            List<tasks> tsksTable = OdbConnectHelper.entObj.tasks.Where(x => x.idWorker == wrkrs.idWorker).ToList();
            dataGrid.ItemsSource = tsksTable;

            mode = 1;
        }
    }
}
