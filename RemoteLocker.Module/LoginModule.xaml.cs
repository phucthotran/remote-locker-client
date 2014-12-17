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
using System.ComponentModel;

namespace RemoteLocker.Module
{
    /// <summary>
    /// Interaction logic for LoginModule.xaml
    /// </summary>
    public partial class LoginModule : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void LoginEventHandler(object sender, LoginEventArgs args);

        public event LoginEventHandler OnSuccess;
        public event LoginEventHandler OnError;
        public event RoutedEventHandler OnCancel;

        private Animation.FadeAnimate fadeAnimate;
        private Controller.AccountController accController;
        private String username;

        public String Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    username = value;
                    this.OnPropertyChanged("Username");
                }
            }
        }

        public String Password
        {
            get { return tbPassword.Password; }
            set { tbPassword.Password = value; }
        }

        public LoginModule()
        {
            InitializeComponent();
            DataContext = this;

            accController = new Controller.AccountController();

            fadeAnimate = new Animation.FadeAnimate(TimeSpan.FromSeconds(3));
            this.BeginAnimation(UserControl.OpacityProperty, fadeAnimate.FadeIn());
        }

        protected virtual void OnPropertyChanged(String PropertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public class LoginEventArgs
        {
            public String Username { get; private set; }
            public String IdentifyCode { get; private set; }

            public LoginEventArgs(String Username, String IdentifyCode)
            {
                this.Username = Username;
                this.IdentifyCode = IdentifyCode;
            }
        }

        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
            bool done = accController.Available(Username, Password);

            if (done && OnSuccess != null)
                this.OnSuccess(this, new LoginEventArgs(Username, accController.Fetch().IdentifyCode));
            else if (!done && OnError != null)
                this.OnError(this, new LoginEventArgs(Username, null));
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            fadeAnimate.Completed += (object senderObj, EventArgs args) => {
                if (this.OnCancel != null)
                    this.OnCancel(this, new RoutedEventArgs());
            };
            this.IsEnabled = false;
            this.BeginAnimation(UserControl.OpacityProperty, fadeAnimate.FadeOut());            
        }        

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                bLogin_Click(this, new RoutedEventArgs());
        }
    }
}
