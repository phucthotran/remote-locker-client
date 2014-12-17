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
using System.Windows.Threading;

namespace RemoteLocker.Module
{
    /// <summary>
    /// Interaction logic for NotificationModule.xaml
    /// </summary>
    public partial class NotificationModule : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Animation.FadeAnimate fadeAnimate;
        private String title;
        private String summary;
        
        public String Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        public String Summary
        {
            get { return summary; }
            set
            {
                if (value != summary)
                {
                    summary = value;
                    this.OnPropertyChanged("Summary");
                }
            }
        }

        public NotificationModule()
        {
            InitializeComponent();
            DataContext = this;
        }

        public NotificationModule(String Title, String Summary) : this()
        {
            this.title = Title;
            this.summary = Summary;

            fadeAnimate = new Animation.FadeAnimate(TimeSpan.FromSeconds(3));               
            this.BeginAnimation(UserControl.OpacityProperty, fadeAnimate.FadeOut());
        }

        protected virtual void OnPropertyChanged(String PropertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
