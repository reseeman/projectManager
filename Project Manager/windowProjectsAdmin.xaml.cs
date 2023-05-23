using Project_Manager.dbfiles;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Manager
{
    /// <summary>
    /// Логика взаимодействия для windowProjectsAdmin.xaml
    /// </summary>
    public partial class windowProjectsAdmin : Window
    {
        public windowProjectsAdmin()
        {
            InitializeComponent();
            OdbConnectHelper.entObj = new projdbEntities();
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.ToList();

            cmbStatusFilter.SelectedValuePath = "idStatus";
            cmbStatusFilter.DisplayMemberPath = "nameStatus";
            cmbStatusFilter.ItemsSource = OdbConnectHelper.entObj.status.ToList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.ToList();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить проект?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    projects prj = dataGrid.SelectedItem as projects;
                    OdbConnectHelper.entObj.projects.Remove(prj);
                    OdbConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Проект удален.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                return;
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            OdbConnectHelper.entObj.SaveChanges();
            dataGrid.ItemsSource = null;
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

        private void buttonAddProject_Click(object sender, RoutedEventArgs e)
        {
            windowAddProject wndAddProj = new windowAddProject();
            wndAddProj.ShowDialog();
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

        private void btnFilterDate_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.dateStart >= dpDateStart.SelectedDate && x.dateFinish <= dpDateFinish.SelectedDate).ToList();
        }

        private void cmbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int cmbStatusFilterInt = (int)cmbStatusFilter.SelectedValue;
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.status.idStatus.Equals(cmbStatusFilterInt)).ToList();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (dpDateStart.SelectedDate == null || dpDateFinish.SelectedDate == null && cmbStatusFilter == null && tbxSearchProjByName.Text != "") //только TextBox
            {
                dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.nameProject.Contains(tbxSearchProjByName.Text)).ToList();
            }
            else if (dpDateStart.SelectedDate == null && dpDateFinish.SelectedDate == null && tbxSearchProjByName.Text == "" && cmbStatusFilter.SelectedValue != null) //только ComboBox
            {
                int cmbStatusFilterInt = (int)cmbStatusFilter.SelectedValue;
                dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.status.idStatus.Equals(cmbStatusFilterInt)).ToList();
            }
            else if (dpDateStart.SelectedDate != null && dpDateFinish.SelectedDate != null && cmbStatusFilter.SelectedValue == null && tbxSearchProjByName.Text == "") //только DatePicker
            {
                dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.dateStart >= dpDateStart.SelectedDate && x.dateFinish <= dpDateFinish.SelectedDate).ToList();
            }
            else if (dpDateStart.SelectedDate != null && dpDateFinish.SelectedDate != null && cmbStatusFilter.SelectedValue == null && tbxSearchProjByName.Text != "") //DatePicker и TextBox
            {
                dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.nameProject.Contains(tbxSearchProjByName.Text) && x.dateStart >= dpDateStart.SelectedDate && x.dateFinish <= dpDateFinish.SelectedDate).ToList();
            }
            else if (dpDateStart.SelectedDate != null && dpDateFinish.SelectedDate != null && cmbStatusFilter.SelectedValue != null && tbxSearchProjByName.Text == "") //DatePicker и ComboBox
            {
                int cmbStatusFilterInt = (int)cmbStatusFilter.SelectedValue;
                dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.status.idStatus.Equals(cmbStatusFilterInt) && x.dateStart >= dpDateStart.SelectedDate && x.dateFinish <= dpDateFinish.SelectedDate).ToList();
            }
            else if (dpDateStart.SelectedDate == null || dpDateFinish.SelectedDate == null && cmbStatusFilter.SelectedValue != null && tbxSearchProjByName.Text != "") //ComboBox и TextBox
            {
                int cmbStatusFilterInt = (int)cmbStatusFilter.SelectedValue;
                dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.status.idStatus.Equals(cmbStatusFilterInt) && x.nameProject.Contains(tbxSearchProjByName.Text)).ToList();
            }
            else if (dpDateStart.SelectedDate != null && dpDateFinish.SelectedDate != null && cmbStatusFilter.SelectedValue != null && tbxSearchProjByName.Text != "") //DatePicker, TextBox, ComboBox
            {
                int cmbStatusFilterInt = (int)cmbStatusFilter.SelectedValue;
                dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.Where(x => x.status.idStatus.Equals(cmbStatusFilterInt) && x.nameProject.Contains(tbxSearchProjByName.Text) && x.dateStart >= dpDateStart.SelectedDate && x.dateFinish <= dpDateFinish.SelectedDate).ToList();
            }
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cmbStatusFilter.SelectedValue = null;
            dpDateStart.SelectedDate = null;
            dpDateFinish.SelectedDate = null;
            tbxSearchProjByName.Text = "";
            dataGrid.ItemsSource = OdbConnectHelper.entObj.projects.ToList();
        }

        private void buttonWorkers_Click(object sender, RoutedEventArgs e)
        {
            windowWorkers wndWrkrs = new windowWorkers();
            wndWrkrs.Show();
        }
    }
}
