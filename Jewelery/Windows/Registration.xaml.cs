using Jewelery.Classes;
using Jewelery.Contexts;
using Jewelery.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace Jewelery.Windows
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        JewelryDbContext Db = new JewelryDbContext();
        public Registration()
        {
            InitializeComponent();
            DataGrid_Users_Load(Db);

            update_btn.Visibility = Visibility.Hidden;

            if (WhoInSession.CurrentUserInSession != "Administrator")
            {
                admin_tab.Visibility = Visibility.Hidden;
                permissions_combo.Items.RemoveAt(0);
            }

        }

        private void DataGrid_Users_Load(JewelryDbContext context)
        {
            users_dataGrid.ItemsSource = context.UsersGetAll().ToList();
        }
        private void Button_Click(object sender, RoutedEventArgs e) // RegistrationBtn
        {
            Registrate(Db);
        }

        private bool DoesHaveInfo()
        {
            Regex NumberForm_Check = new Regex(@"^\d{1}-\d{3}-\d{3}-\d{2}-\d{2}");
            Regex PasswordForm_Check = new Regex(@"^\w*\d{3}");

            if (login_txt.Text.Length < 0 && number_txt.Text.Length < 0 &&
                password_txt.Text.Length < 0)
            {
                MessageBox.Show("Введите информацию во все имеющиеся пустые пространства", "Оповещение");
                return false;
            }
            else
            {
                if (NumberForm_Check.IsMatch(number_txt.Text) && PasswordForm_Check.IsMatch(password_txt.Text))
                {
                    MessageBox.Show("Аккаунт был успешно отправлен на проверку!");
                    return true;
                }
                else
                {
                    MessageBox.Show("Номер телефона должен быть введен по формату 1-111-111-11-11\n" +
                           "В пароле, на конце, должны присутствовать три любые цифры");
                    return false;
                }
            }

        }
        private void Registrate(JewelryDbContext context)
        {
            try
            {
                if (DoesHaveInfo()) // Проверка на то, содержит ли метод внутри себя "Истину" - truth - true
                {
                    var SelectedValue = (ComboBoxItem)permissions_combo.SelectedItem;
                    var ConvertedInfo = (string)SelectedValue.Content;

                    Users NewUserForCheck = new Users()
                    {
                        DateOfRegistry = DateTime.Now,
                        Login = login_txt.Text,
                        Password = password_txt.Text,
                        Permissions = ConvertedInfo,
                        PhoneNumber = number_txt.Text,
                        RegistryStatus = false
                    };

                    context.Users.Add(NewUserForCheck);
                    context.SaveChanges();

                    DataGrid_Users_Load(context);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // returnBtn
        {
            Auth NewWindow = new Auth();
            NewWindow.Show();
            Close();
        }

        private void aproove_action_Click(object sender, RoutedEventArgs e)
        {
            Aprooving(Db);
        }
        private void Aprooving(JewelryDbContext context)
        {
            var SelectedID = (users_dataGrid.SelectedItem as Users).ID;

            var FoundUser = context.Users.Where(r => r.ID == SelectedID).FirstOrDefault();

            FoundUser.RegistryStatus = true;

            context.Users.Update(FoundUser);
            context.SaveChanges();
            DataGrid_Users_Load(context);
        }

        private void deni_action_Click(object sender, RoutedEventArgs e)
        {
            Deni(Db);
        }
        private void Deni(JewelryDbContext context)
        {
            var SelectedID = (users_dataGrid.SelectedItem as Users).ID;

            var FoundUser = context.Users.Where(r => r.ID == SelectedID).FirstOrDefault();

            FoundUser.RegistryStatus = false;

            context.Users.Update(FoundUser);
            context.SaveChanges();
            DataGrid_Users_Load(context);
        }

        private void updateInfo_action_Click(object sender, RoutedEventArgs e)
        {
            var SelectedID = (users_dataGrid.SelectedItem as Users).ID;
            var FoundUser = Db.Users.Where(r => r.ID == SelectedID).FirstOrDefault();

            login_txt.Text = FoundUser.Login;
            number_txt.Text = FoundUser.PhoneNumber;
            password_txt.Text = FoundUser.Password;

            update_btn.Visibility = Visibility.Visible;
            registrate_btn.Visibility = Visibility.Hidden;


        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            var FoundUser = Db.Users.Where(r => r.Login == login_txt.Text).FirstOrDefault();
            FoundUser.Login = login_txt.Text;
            FoundUser.PhoneNumber = number_txt.Text;
            FoundUser.Password = password_txt.Text;
            Db.Update(FoundUser);
            Db.SaveChanges();
            DataGrid_Users_Load(Db);

            login_txt.Clear();
            number_txt.Clear();
            password_txt.Clear();

            update_btn.Visibility = Visibility.Hidden;
            registrate_btn.Visibility = Visibility.Visible;
        }

        private void deleting_action_Click(object sender, RoutedEventArgs e)
        {
            var SelectedID = (users_dataGrid.SelectedItem as Users).ID;
            var FoundUser = Db.Users.Where(r => r.ID == SelectedID).FirstOrDefault();
            Db.Users.Remove(FoundUser);
            Db.SaveChanges();
            DataGrid_Users_Load(Db);
        }
    }
}
