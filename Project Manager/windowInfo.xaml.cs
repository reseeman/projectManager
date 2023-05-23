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
    /// Логика взаимодействия для windowInfo.xaml
    /// </summary>
    public partial class windowInfo : Window
    {
        int mode;
        public windowInfo()
        {
            InitializeComponent();
            mode = 0;
        }

        public windowInfo(projects projs)
        {
            InitializeComponent();
            tbxProjName.Text = projs.nameProject;
            dpDateStart.Text = projs.dateStart.ToString();
            dpDateFinish.Text = projs.dateFinish.ToString();
            //tbxSrok.Text = projs.projectPeriodDays.ToString();
            cmbStatus.SelectedValue = projs.idStatus;
            tbxDescription.Text = projs.description;
            mode = 1;

            cmbStatus.SelectedValuePath = "idStatus";
            cmbStatus.DisplayMemberPath = "nameStatus";
            cmbStatus.ItemsSource = OdbConnectHelper.entObj.status.ToList();

            List<tasks> tsksTable = OdbConnectHelper.entObj.tasks.Where(x => x.idProject == projs.idProject).ToList();
            datagridtasks.ItemsSource = tsksTable;
        }

        private void buttonSaveProj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OdbConnectHelper.projectsList = OdbConnectHelper.entObj.projects.ToList();
                for (int i = 0; i < OdbConnectHelper.projectsList.Count; i++)
                {
                    if (OdbConnectHelper.projectsList[i].nameProject == tbxProjName.Text)
                    {
                        OdbConnectHelper.projectsList[i].dateStart = DateTime.Parse(dpDateStart.ToString());
                        OdbConnectHelper.projectsList[i].dateFinish = DateTime.Parse(dpDateFinish.ToString());
                        OdbConnectHelper.projectsList[i].projectPeriodDays = int.Parse(tbxSrok.Text);
                        OdbConnectHelper.projectsList[i].idStatus = (int)cmbStatus.SelectedValue;
                        OdbConnectHelper.projectsList[i].description = tbxDescription.Text;
                    }
                }
                
                OdbConnectHelper.entObj.SaveChanges();
                MessageBox.Show("Обновите таблицу!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Проверьте входные данные: {ex}", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }

        

        private void addTask_Click(object sender, RoutedEventArgs e)
        {
            windowAddTask wndAddTsks = new windowAddTask();
            wndAddTsks.ShowDialog();
        }

        private void deleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить задачу?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    tasks tsk = datagridtasks.SelectedItem as tasks;
                    OdbConnectHelper.entObj.tasks.Remove(tsk);
                    OdbConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Задача удалена! Откройте окно заново!");
                    this.Close();
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

        private void buttonSaveProj_MouseEnter(object sender, MouseEventArgs e)
        {
            DateTime dtFinish = Convert.ToDateTime(dpDateFinish.Text.ToString());
            DateTime dtStart = Convert.ToDateTime(dpDateStart.Text.ToString());
            TimeSpan srok = dtFinish.Subtract(dtStart);
            tbxSrok.Text = Convert.ToString(srok.Days);
        }
    }
}
