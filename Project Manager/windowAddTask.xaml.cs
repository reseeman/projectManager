using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Project_Manager.dbfiles;

namespace Project_Manager
{
    public partial class windowAddTask : Window
    {
        public windowAddTask()
        {
            InitializeComponent();

            cmbProject.SelectedValuePath = "idProject";
            cmbWorker.SelectedValuePath = "idWorker";

            cmbProject.DisplayMemberPath = "nameProject";
            cmbWorker.DisplayMemberPath = "nameWorker";

            cmbStatus.SelectedValuePath = "idStatus";
            cmbStatus.DisplayMemberPath = "nameStatus";
            cmbStatus.ItemsSource = OdbConnectHelper.entObj.status.ToList();

            cmbProject.ItemsSource = OdbConnectHelper.entObj.projects.ToList();
            cmbWorker.ItemsSource = OdbConnectHelper.entObj.workers.ToList();

            this.tbxTaskName.PreviewTextInput += new TextCompositionEventHandler(TbxString_PreviewTextInput);
        }

        void TbxString_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        void TbxInt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void buttonAddTask_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                if (dpDateStart.Text == "" || dpDateFinish.Text == "" || cmbProject.SelectedItem == null || cmbWorker.SelectedItem == null || cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Не все данные указаны!", "Данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    DateTime dtFinish = Convert.ToDateTime(dpDateFinish.Text.ToString());
                    DateTime dtStart = Convert.ToDateTime(dpDateStart.Text.ToString());
                    TimeSpan srok = dtFinish.Subtract(dtStart);
                    tbxSrok.Text = Convert.ToString(srok.Days);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая работа с приложением: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tasks tskObj = new tasks()
                {
                    nameTask = tbxTaskName.Text,
                    taskStart = DateTime.Parse(dpDateStart.Text),
                    taskFinish = DateTime.Parse(dpDateFinish.Text),
                    taskPeriodDays = Convert.ToInt32(tbxSrok.Text),
                    idProject = Convert.ToInt32(cmbProject.SelectedValue),
                    idWorker = Convert.ToInt32(cmbWorker.SelectedValue),
                    idStatus = Convert.ToInt32(cmbStatus.SelectedValue)
                };

                DateTime seldatestart = dpDateStart.SelectedDate.Value;
                DateTime seldatefinish = dpDateFinish.SelectedDate.Value;
                if (seldatefinish < seldatestart)
                {
                    MessageBox.Show("Некорректные даты!", "Дата", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                else
                {
                    OdbConnectHelper.entObj.tasks.Add(tskObj);
                    OdbConnectHelper.entObj.SaveChanges();

                    MessageBox.Show("Задача №" + tskObj.idTask + " успешно добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая работа с приложением: " + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
