
using System.Windows;

namespace Jewelery.Windows
{
    /// <summary>
    /// Interaction logic for InfoAboutProduct.xaml
    /// </summary>
    public partial class InfoAboutProduct : Window
    {
        public InfoAboutProduct()
        {
            InitializeComponent();
        }

        private void return_btn_Click(object sender, RoutedEventArgs e)
        {
            Auth AuthWindow = new Auth();
            AuthWindow.Show();
            Close();
        }
    }
}
