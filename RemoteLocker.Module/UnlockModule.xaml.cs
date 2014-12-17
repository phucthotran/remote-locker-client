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
    /// Interaction logic for UnlockModule.xaml
    /// </summary>
    public partial class UnlockModule : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void UnlockEventHandler(object sender, UnlockEventArgs args);

        public event UnlockEventHandler OnSuccess;
        public event UnlockEventHandler OnError;
        public event RoutedEventHandler OnCancel;

        private Animation.FadeAnimate fadeAnimate;
        private Controller.AccountController accController;
        private String identifyCode;
        
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

        public UnlockModule()
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

        public class UnlockEventArgs
        {
            public String IdentifyCode { get; private set; }

            public UnlockEventArgs(String IdentifyCode)
            {
                this.IdentifyCode = IdentifyCode;
            }
        }

        private void bUnlock_Click(object sender, RoutedEventArgs e)
        {
            bool done = accController.AvailableCode(IdentifyCode);

            if (done && this.OnSuccess != null)
                this.OnSuccess(this, new UnlockEventArgs(identifyCode));
            else if (!done && this.OnError != null)
                this.OnError(this, new UnlockEventArgs(identifyCode));
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

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                bUnlock_Click(this, new RoutedEventArgs());
        }
    }
}
