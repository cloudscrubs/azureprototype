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
    public partial class Test1 : PhoneApplicationPage
    {
        public Test1()
        {
            InitializeComponent();
        }

        string blah = "";
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            blah = NavigationContext.QueryString["msg"];
        }

        private void inputButton_Click(object sender, RoutedEventArgs e)
        {
            string str = inputbox.Text;
            Service1Client client1 = new Service1Client();
            client1.SeePatientDataCompleted += new EventHandler<SeePatientDataCompletedEventArgs>(client1_SeePatientDataCompleted);
            client1.SeePatientDataAsync(str);
        }

        void client1_SeePatientDataCompleted(object sender, SeePatientDataCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Result != null)
            {
                SSN.Text = e.Result.SSN;
                Name.Text = e.Result.Name;
                DOB.Text = e.Result.DOB.ToString();
                Gender.Text = e.Result.Gender;
                Address.Text = e.Result.Address;
                Nationality.Text = e.Result.Nationality;
                PhoneNumber.Text = e.Result.PhoneNumber;
                LegalStatus.Text = e.Result.LegalStatus;
                Insurance.Text = e.Result.MedicalInsurance;
                NextOfKin.Text = e.Result.NextOfKin;
            }

            else MessageBox.Show("Unable to retrieve data");
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            string str = blah;
            inputbox.Text = blah;
            Service1Client client1 = new Service1Client();
            client1.SeePatientDataCompleted += new EventHandler<SeePatientDataCompletedEventArgs>(client1_SeePatientDataCompleted);
            client1.SeePatientDataAsync(str);
        }
    }
}