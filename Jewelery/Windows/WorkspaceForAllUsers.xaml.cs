using Jewelery.Classes;
using Jewelery.Contexts;
using Jewelery.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Jewelery.Windows
{
    /// <summary>
    /// Interaction logic for WorkspaceForAllUsers.xaml
    /// </summary>
    public partial class WorkspaceForAllUsers : Window
    {
        JewelryDbContext Db = new JewelryDbContext();
        public WorkspaceForAllUsers()
        {
            InitializeComponent();
            DataGrid_Orders_Load(Db);
            DataGrid_Users_Load(Db);
            DataGrid_Masters_Load(Db);

            if (WhoInSession.CurrentUserInSession == "Buyer")
            {
                AprooveAction.Visibility = Visibility.Hidden;
                adminTab.Visibility = Visibility.Hidden;

            }
            else if (WhoInSession.CurrentUserInSession == "Master")
            {
                adminTab.Visibility = Visibility.Hidden;

            }
        }

        private void creatingOrder_btn_Click(object sender, RoutedEventArgs e)
        {

            Orders NewOrder = new Orders()
            {
                OrderDescription = order_txt.Text,
                OrderedBy = WhoInSession.WhoIn,
                OrderStatus = "Отправлен на рассмотр сотрудникам предприятия",
            };

            Db.Orders.Add(NewOrder);
            Db.SaveChanges();
            DataGrid_Orders_Load(Db);
        }
        private void DataGrid_Orders_Load(JewelryDbContext context)
        {
            Orders_dataGrid.ItemsSource = context.Orders.Where(r => r.OrderedBy == WhoInSession.WhoIn).ToList();
            
        }
        private void DataGrid_Users_Load(JewelryDbContext context)
        {

            usersDataGrid.ItemsSource = context.UsersGetAll().ToList();
        }
        private void DataGrid_Masters_Load(JewelryDbContext context)
        {

            CreatingMaster_dataGrid.ItemsSource = context.MastersGetAll().ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Отправиться к регистрационному окну // Goes to the registration window
        {
            Registration RegistWindow = new Registration();
            RegistWindow.Show();
            Close();
        }

        private void sort_btn_Click(object sender, RoutedEventArgs e)
        {
            Orders_dataGrid.ItemsSource = Db.Orders.Where(x => x.OrderDescription == sort_txt.Text).ToList();
        }

        private void AprooveAction_Click(object sender, RoutedEventArgs e)
        {
            var SelectedOrderID = (Orders_dataGrid.SelectedItem as Orders).OrderID;
            var FoundElement = Db.Orders.Where(r => r.OrderID == SelectedOrderID).FirstOrDefault();

            FoundElement.OrderStatus = "Ожидает Вашего прихода и выкупа";

            Db.Orders.Update(FoundElement);
            Db.SaveChanges();
            DataGrid_Orders_Load(Db);
        }

        private void cancelSort_btn_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_Orders_Load(Db);
        }

        private void EmployeeAdd_action_Click(object sender, RoutedEventArgs e)
        {
            var SelectedValue = (ComboBoxItem)statusOfMaster_combo.SelectedItem;
            var ConvertedValue = (string)SelectedValue.Content;
            JewelryMaster NewMemberOfCommunity = new JewelryMaster()
            {
                Master_DateOfRegistry = DateTime.Now,
                Master_IerarchPost = ierarchPos_txt.Text,
                Master_Name = login_master_txt.Text,
                Master_Status = ConvertedValue
            };
            Db.JeweleryMasters.Add(NewMemberOfCommunity);
            Db.SaveChanges();

            DataGrid_Masters_Load(Db);
        }

        private void EmployeeDeleting_action_Click(object sender, RoutedEventArgs e)
        {
            var SelectedOrderID = (CreatingMaster_dataGrid.SelectedItem as JewelryMaster).Master_ID;
            var FoundElement = Db.JeweleryMasters.Where(r => r.Master_ID == SelectedOrderID).FirstOrDefault();

            Db.JeweleryMasters.Remove(FoundElement);
            Db.SaveChanges();
            DataGrid_Masters_Load(Db);
        }
    }
}
