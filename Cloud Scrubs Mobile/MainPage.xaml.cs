using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using CloudScrubsMobile.CS_Reference;

namespace CloudScrubsMobile
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void SSN_field_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SSN_field.Text == String.Empty)
            {
                SSN_field.Text = "SSN/ID";
                SolidColorBrush Brush2 = new SolidColorBrush();
                Brush2.Color = Colors.Gray;
                SSN_field.Foreground = Brush2;
            }
        }

        private void SSN_field_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SSN_field.Text == "SSN/ID")
            {
                SSN_field.Text = String.Empty;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            pass_back.Text = String.Empty;
        }

        private void pass_back_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Pass_field.Password == String.Empty)
            {
                pass_back.Text = "Password";
            }
        }

        private void inputButton_Click(object sender, EventArgs e)
        {
            string str = SSN_field.Text;
            Service1Client client1 = new Service1Client();
            client1.SeePatientDataCompleted += new EventHandler<SeePatientDataCompletedEventArgs>(client1_SeePatientDataCompleted);
            client1.SeePatientDataAsync(str);
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddEditBasicDetails.xaml", UriKind.RelativeOrAbsolute));
        }

        void client1_SeePatientDataCompleted(object sender, SeePatientDataCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Result != null)
            {
                NavigationService.Navigate(new Uri("/DataDisplayPage.xaml?msg=" + SSN_field.Text, UriKind.RelativeOrAbsolute));
            }

            else MessageBox.Show("SSN does not exist");
        }

    }
}