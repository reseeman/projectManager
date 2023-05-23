using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project_Manager.dbfiles;

namespace Project_Manager
{
    public partial class windowProjects : Window
    {
        public windowProjects()
        {
            InitializeComponent();
            OdbConnectHelper.entObj = new projdbEntities();
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.ToList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.ToList();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void searchProjByName_TextChanged(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.nameProject.Contains(tbxSearchProjByName.Text)).ToList();
        }

        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.SelectAllCells();
            dataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;

            ApplicationCommands.Copy.Execute(null, dataGrid);

            string resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

            string result = (string)Clipboard.GetData(DataFormats.Text);

            dataGrid.UnselectAllCells();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "ExportedProjects";
            dlg.DefaultExt = ".text";
            dlg.Filter = "(.xls)|*.xls";

            bool? result1 = dlg.ShowDialog();
            if (result1 == true)
            {
                string filename = dlg.FileName;
                StreamWriter file = new StreamWriter(filename, false, Encoding.Default);
                file.WriteLine(result);
                file.Close();

                MessageBox.Show("Вывод данных успешно завершён.");
            }
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            windowInfo wndInf = new windowInfo((sender as Button).DataContext as projects);
            wndInf.ShowDialog();
        }
    }
}
