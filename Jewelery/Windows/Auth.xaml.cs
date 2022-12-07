using Jewelery.Classes;
using Jewelery.Contexts;
using System;
using System.Linq;
using System.Windows;


namespace Jewelery.Windows
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        JewelryDbContext Db = new JewelryDbContext();
        public Auth()
        {
            InitializeComponent();
        }

        private bool DoesHavePass()
        {
            if (password_txt.Text.Length < 0)
            {
                MessageBox.Show("Пароль не был введён");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Authoriz(JewelryDbContext context)
        {
            var Choosen = context.Users.Where(r => r.Login == login_txt.Text).FirstOrDefault();

            if (DoesHavePass())
            {

                if (!String.IsNullOrEmpty(Choosen.Password) && password_txt.Text == Choosen.Password)
                {
                    if (Choosen.RegistryStatus == true)
                    {

                        switch (Choosen.Permissions)
                        {
                            case "Administrator":

                                WhoInSession.CurrentUserInSession = Choosen.Permissions;
                                WhoInSession.WhoIn = Choosen.Login;
                                WorkspaceForAllUsers NewWindow = new WorkspaceForAllUsers();
                                NewWindow.Show();
                                Close();
                                break;

                            case "Buyer":

                                WhoInSession.CurrentUserInSession = Choosen.Permissions;
                                WhoInSession.WhoIn = Choosen.Login;
                                WorkspaceForAllUsers Buyer = new WorkspaceForAllUsers();
                                Buyer.Show();
                                Close();
                                break;

                            case "Worker":

                                WhoInSession.CurrentUserInSession = Choosen.Permissions;
                                WhoInSession.WhoIn = Choosen.Login;
                                WorkspaceForAllUsers Worker = new WorkspaceForAllUsers();
                                Worker.Show();
                                Close();
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Ваш аккаунт имеет статус:\n{Choosen.Permissions}", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Неизвестный пользователь");
                }
            }
        }

        private void registration_btn_Click(object sender, RoutedEventArgs e)
        {
            Registration RegistryWindow = new Registration();
            RegistryWindow.Show();
            Close();
        }

        private void entering_btn_Click(object sender, RoutedEventArgs e)
        {
            Authoriz(Db);
        }

        private void aboutProgram_btn_Click(object sender, RoutedEventArgs e)
        {
            InfoAboutProduct InformationWindow = new InfoAboutProduct();
            InformationWindow.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
