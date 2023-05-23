using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Project_Manager.dbfiles;

namespace Project_Manager
{
    public partial class windowAddProject : Window
    {
        public windowAddProject()
        {
            InitializeComponent();

            cmbStatus.SelectedValuePath = "idStatus";
            cmbStatus.DisplayMemberPath = "nameStatus";
            cmbStatus.ItemsSource = OdbConnectHelper.entObj.status.ToList();

            this.tbxDescription.PreviewTextInput += new TextCompositionEventHandler(TbxString_PreviewTextInput);
            this.tbxProjName.PreviewTextInput += new TextCompositionEventHandler(TbxString_PreviewTextInput);
        }

        void TbxString_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        void TbxInt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void buttonAddProj_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                if (dpDateStart.Text == "" || dpDateFinish.Text == "")
                {
                    MessageBox.Show("Не все даты указаны.", "Дата", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                DateTime seldatestart = dpDateStart.SelectedDate.Value;
                DateTime seldatefinish = dpDateFinish.SelectedDate.Value;
                if (seldatefinish < seldatestart)
                {
                    MessageBox.Show("Некорректные даты!", "Дата", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAddProj_Click(object sender, RoutedEventArgs e)
        {
            //bool availability = false;

            //if (cmbIsReady.SelectedValue == "Готово")
            //{
            //    availability = true;
            //}
            try
            {
                projects projObj = new projects()
                {
                    nameProject = tbxProjName.Text,
                    dateStart = DateTime.Parse(dpDateStart.Text),
                    dateFinish = DateTime.Parse(dpDateFinish.Text),
                    projectPeriodDays = Convert.ToInt32(tbxSrok.Text),
                    description = tbxDescription.Text,
                    //isProjectReady = Convert.ToBoolean(cmbIsReady.SelectedValue)
                    idStatus = Convert.ToInt32(cmbStatus.SelectedValue)
                };

                if (tbxProjName.Text == "" || dpDateStart.Text == "" || dpDateFinish.Text == "" || tbxSrok.Text == "")
                {
                    MessageBox.Show("Не все данные введены!", "Данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else
                {
                    OdbConnectHelper.entObj.projects.Add(projObj);
                    OdbConnectHelper.entObj.SaveChanges();

                    MessageBox.Show("Проект №" + projObj.idProject + " успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
