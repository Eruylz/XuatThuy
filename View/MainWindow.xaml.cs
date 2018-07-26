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
using System.Windows.Navigation;
using System.Windows.Shapes;
using XuatThuy.ViewModel;
using XuatThuy.Utils;

namespace XuatThuy.ViewModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MainWindow main = new MainWindow();
            //main.Hide();
            //this.Hide();

            //PhieuCanWindow wPhieuCan = new PhieuCanWindow();
            //wPhieuCan.Show();
            //this.Visibility = Visibility.Hidden;

        }

        private void btnXuat_Click(object sender, RoutedEventArgs e)
        {
            //lblHienThi.TextInput = "aaaa";
            //DangNhap wDangNhap = new DangNhap();
            //wDangNhap.Show();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
