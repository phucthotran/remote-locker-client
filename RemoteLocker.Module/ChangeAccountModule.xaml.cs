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
using RemoteLocker.Common.Library.Encryption;
using RemoteLocker.Common.Model;

namespace RemoteLocker.Module
{
    /// <summary>
    /// Interaction logic for ChangeAccountModule.xaml
    /// </summary>
    public partial class ChangeAccountModule : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ChangeEventHandler(object sender, ChangeArgs args);

        public event ChangeEventHandler OnSuccess;
        public event ChangeEventHandler OnError;
        public event RoutedEventHandler OnCancel;

        private Animation.FadeAnimate fadeAnimate;
        private Controller.AccountController accController;
        private String username;
        private String identifyCode;

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

        public String IdentifyCode
        {
            get { return identifyCode; }
            set
            {
                if (value != identifyCode)
                {
                    identifyCode = value;
                    this.OnPropertyChanged("IdentifyCode");
                }
            }
        }

        public ChangeAccountModule()
        {
            InitializeComponent();
            DataContext = this;

            accController = new Controller.AccountController();

            IdentifyCode = Md5Sha1Encrypt.MD5Hashing(DateTime.Now.ToString());

            fadeAnimate = new Animation.FadeAnimate(TimeSpan.FromSeconds(3));
            this.BeginAnimation(UserControl.OpacityProperty, fadeAnimate.FadeIn());
        }

        protected virtual void OnPropertyChanged(String PropertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public class ChangeArgs
        {
            public String Username { get; private set; }            
            public String IdentifyCode { get; private set; }

            public ChangeArgs(String Username, String IdentifyCode)
            {
                this.Username = Username;
                this.IdentifyCode = IdentifyCode;
            }
        }

        private void bChange_Click(object sender, RoutedEventArgs e)
        {
            if (Username == String.Empty || Password == String.Empty || IdentifyCode == String.Empty)
            {
                this.OnError(this, new ChangeArgs(Username, IdentifyCode));
                return;
            }

            if (Username.Length < 3 || Password.Length < 3 || IdentifyCode.Length < 32)
            {
                this.OnError(this, new ChangeArgs(Username, IdentifyCode));
                return;
            }

            bool done = accController.Save(new Account(Username, Password, IdentifyCode));

            if (done && this.OnSuccess != null)
                this.OnSuccess(this, new ChangeArgs(Username, IdentifyCode));
            else if (!done && this.OnError != null)
                this.OnError(this, new ChangeArgs(Username, IdentifyCode));
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            fadeAnimate.Completed += (object senderObj, EventArgs args) =>
            {
                if (this.OnCancel != null)
                    this.OnCancel(this, new RoutedEventArgs());
            };
            this.IsEnabled = false;
            this.BeginAnimation(UserControl.OpacityProperty, fadeAnimate.FadeOut());
        }

        private void bGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            IdentifyCode = Md5Sha1Encrypt.MD5Hashing(DateTime.Now.ToString());
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                bChange_Click(this, new RoutedEventArgs());
        }
    }
}
