using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using XuatThuy.Model;
using XuatThuy.Utils;

namespace XuatThuy.ViewModel
{
    public class LoginVM : ViewModelBase
    {
        public RelayCommand<object> SignInCmd { get; set; }

        string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        string _notifyMessage;
        public string NotifyMessage
        {
            get => _notifyMessage;
            set
            {
                _notifyMessage = value;
                RaisePropertyChanged("NotifyMessage");
            }
        }

        void SignIn(object parameter)
        {
            Login loginwindow = parameter as Login;
            string Password = loginwindow.pbxPassword.Password;
            XuatHangDataContext db = new XuatHangDataContext();
            db.CommandTimeout = 0;
            //loginwindow.pbxPassword.Password;

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                Debug.WriteLine("Sai mật khẩu hoặc tên đăng nhập");
                NotifyMessage = "Sai mật khẩu hoặc tên đăng nhập";
            }
            else
            {
                Guid? Session_ID = null;

                try
                {
                    int ret = db.p_TaiKhoan_DangNhap(ref Session_ID, UserName,
                                                Password,
                                                null,
                                                null,
                                                null, null);
                    Debug.WriteLine("UserName = " + UserName);
                    Debug.WriteLine("Password = " + Password);
                    Debug.WriteLine("ret = " + ret.ToString());
                    switch (ret)
                    {
                        case 0://mat khau chinh thanh cong
                            {
                                var tk = db.p_TaiKhoan_LayTheoTen(Session_ID, UserName).FirstOrDefault();

                                if (tk != null)
                                {
                                    Session.ID = Session_ID;
                                    Session.UserName = tk.TenDangNhap;

                                    var cn = db.P_CaNhan_LayTheoID(Session_ID, tk.CaNhanID).FirstOrDefault();

                                    if (cn != null)
                                    {
                                        Session.FullName = cn.HoVaTen;
                                    }

                                    LoginSuccess(loginwindow);
                                }
                            }
                            break;
                        case 1://mat khau tam thanh cong
                            {
                                var tk = db.p_TaiKhoan_LayTheoTen(Session_ID, UserName).FirstOrDefault();

                                if (tk != null)
                                {
                                    Session.ID = Session_ID;
                                    Session.UserName = tk.TenDangNhap;

                                    var cn = db.P_CaNhan_LayTheoID(Session_ID, tk.CaNhanID).FirstOrDefault();

                                    if (cn != null)
                                    {
                                        Session.FullName = cn.HoVaTen;
                                    }

                                    //tblThongBao.Text = "Đăng nhập thành công với mật khẩu tạm";
                                    LoginSuccess(loginwindow);

                                }
                            }
                            break;
                        default:
                            //tblThongBao.Text = "Sai tài khoản hoặc mật khẩu";
                            NotifyMessage = "Sai mật khẩu hoặc tên đăng nhập";
                            loginwindow.pbxPassword.Password = string.Empty;
                            break;
                    }
                }
                catch (SqlException sqlExn)
                {
                    NotifyMessage = "Không kết nối được với cơ sở dữ liệu";
                    LogUtils.PrintLog("ERROR: Không kết nối được với cơ sở dữ liệu");
                    LogUtils.PrintLog("ERROR MESSAGE: " + sqlExn.Message);
                }
                catch (Exception ex)
                {
                    NotifyMessage = "Lỗi không xác định";
                    LogUtils.PrintLog("ERROR: Lỗi không xác định");
                    LogUtils.PrintLog("ERROR MESSAGE: " + ex.Message);
                }
            }
        }

        void LoginSuccess(Login loginwindow)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            loginwindow.Close();
        }

        public LoginVM()
        {
            SignInCmd = new RelayCommand<object>(SignIn);

            //BangDT bangDT = new BangDT("10.68.51.250", 1024);
            //Debug.WriteLine("mmmmmmmmmmmmmmmmmmmmmmmmmm");
            //bangDT.Line1_Text = "mmmmmmm";
            //bangDT.SetTextLine(1);


            //LogUtils.PrintLog("ccccccccccccccccccccccccccccccccccc");
            //object aaa = "aaaaa";
            //int b = 0;
            //float c = 5 / b;
            //PasswordBox bb = aaa as PasswordBox;
        }
    }
}
