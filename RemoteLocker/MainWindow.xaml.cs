using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RemoteLocker.Common.Library.Action;
using RemoteLocker.Common.Library.Encryption;
using RemoteLocker.Common.Library.DataAccess;
using RemoteLocker.Common.Global;
using RemoteLocker.Communication;
using RemoteLocker.Module;
using System.IO;

namespace RemoteLocker
{    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICommunicationProvider tcpProvider;
        private ICommandSender stringCommandSender;

        public MainWindow()
        {
            InitializeComponent();
            AppAction.InitData();

            #region SETUP TCPCOMMUNICATION FOR REMOTE CONTROLLING

            tcpProvider = new TcpCommunication(this);
            stringCommandSender = new StringCommandSender();
            stringCommandSender.Provider = tcpProvider;

            tcpProvider.Start();

            #endregion
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            
        }
        
        //public void OnActionClicked(object sender, RoutedEventArgs e)
        //{
        //    if (!(e.Source is Button))
        //        return;

        //    Button actionButton = (Button)e.Source;
        //    String actionName = (String)actionButton.Tag;

        //    switch (actionName)
        //    {
        //        case CommonConstant.LOGIN_ACTION:
        //            {
        //                LoginModule loginModule = modulePanel.LoadModule<LoginModule>(null);

        //                loginModule.OnSuccess += (object senderObj, LoginModule.LoginEventArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<LoginModule>();
        //                    modulePanel.LoadModule<NotificationModule>(new object[] { "Đăng Nhập", "Đăng nhập thành công!" });

        //                    bLogin.Visibility = System.Windows.Visibility.Collapsed;
        //                    bChange.Visibility = System.Windows.Visibility.Visible;
        //                    bLogout.Visibility = System.Windows.Visibility.Visible;
        //                    bxCode.Visibility = System.Windows.Visibility.Visible;
        //                    tbIdentifyCode.Text = String.Format("Mã Xác Nhận Hiện Tại: {0}", args.IdentifyCode);
        //                };

        //                loginModule.OnError += (object senderObj, LoginModule.LoginEventArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<LoginModule>();
        //                    modulePanel.LoadModule<NotificationModule>(new object[] { "Đăng Nhập", "Đăng nhập thất bại! Vui lòng kiểm tra lại thông tin." });
        //                };

        //                loginModule.OnCancel += (object senderObj, RoutedEventArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<LoginModule>();
        //                };
        //            }
        //            break;

        //        case CommonConstant.UNLOCK_ACTION:
        //            {
        //                UnlockModule unlockModule = modulePanel.LoadModule<UnlockModule>(null);

        //                unlockModule.OnSuccess += (object senderObj, UnlockModule.UnlockEventArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<UnlockModule>();
        //                    modulePanel.LoadModule<NotificationModule>(new object[] { "Mở Khóa", "Mở khóa thành công!" });
        //                    this.Close();
        //                };

        //                unlockModule.OnError += (object senderObj, UnlockModule.UnlockEventArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<UnlockModule>();
        //                    modulePanel.LoadModule<NotificationModule>(new object[] { "Mở Khóa", "Mở khóa thất bại. Vui lòng kiểm tra lại thông tin." });
        //                };

        //                unlockModule.OnCancel += (object senderObj, RoutedEventArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<UnlockModule>();
        //                };
        //            }
        //            break;

        //        case CommonConstant.CHANGE_ACTION:
        //            {
        //                ChangeAccountModule changeAccModule = modulePanel.LoadModule<ChangeAccountModule>(null);

        //                changeAccModule.OnSuccess += (object senderObj, ChangeAccountModule.ChangeArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<ChangeAccountModule>();
        //                    modulePanel.LoadModule<NotificationModule>(new object[] { "Đổi Thông Tin Tài Khoản", "Đổi thông tin tài khoản thành công!" });
        //                };

        //                changeAccModule.OnError += (object senderObj, ChangeAccountModule.ChangeArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<ChangeAccountModule>();
        //                    modulePanel.LoadModule<NotificationModule>(new object[] { "Đổi Thông Tin Tài Khoản", "Đổi thông tin tài khoản thất bại! Vui lòng kiểm tra lại thông tin." });
        //                };

        //                changeAccModule.OnCancel += (object senderObj, RoutedEventArgs args) =>
        //                {
        //                    modulePanel.UnloadModule<ChangeAccountModule>();
        //                };
        //            }
        //            break;

        //        case CommonConstant.REQUEST_ACTION:
        //            stringCommandSender.Send(Command.UNLOCK_REQUEST);
        //            break;

        //        case CommonConstant.LOGOUT_ACTION:
        //            {
        //                modulePanel.LoadModule<NotificationModule>(new object[] { "Đăng Xuất", "Đăng xuất thành công!" });

        //                bLogin.Visibility = System.Windows.Visibility.Visible;
        //                bChange.Visibility = System.Windows.Visibility.Collapsed;
        //                bLogout.Visibility = System.Windows.Visibility.Collapsed;
        //                bxCode.Visibility = System.Windows.Visibility.Collapsed;
        //                tbIdentifyCode.Text = String.Empty;
        //            }
        //            break;

        //        case CommonConstant.SHUTDOWN_ACTION:
        //            {
        //                MessageBoxResult result = MessageBox.Show("Bạn Có Chắc Muốn Tắt Máy?", "Mất Dữ Liệu Làm Việc!!!", MessageBoxButton.YesNo);

        //                if (result == MessageBoxResult.Yes)
        //                    SystemAction.Shutdown();
        //            }
        //            break;
        //    }
        //}        
    }
    
}
