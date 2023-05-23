using System.Windows;
using Project_Manager.dbfiles;

namespace Project_Manager
{
    public partial class windowMain : Window
    {

        public windowMain()
        {
            InitializeComponent();

            OdbConnectHelper.entObj = new projdbEntities();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbxLogin.Text == "user" && pbxPass.Password == "1234")
            {
                windowProjects wndProj = new windowProjects();
                wndProj.ShowDialog();
            }
            else if (tbxLogin.Text == "admin" && pbxPass.Password == "0000")
            {
                windowProjectsAdmin wndProjAdm = new windowProjectsAdmin();
                wndProjAdm.ShowDialog();
            }

        }
    }
}
