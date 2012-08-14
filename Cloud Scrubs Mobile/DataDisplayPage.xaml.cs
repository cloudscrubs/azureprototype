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
    public partial class DataDisplayPage : PhoneApplicationPage
    {
        public DataDisplayPage()
        {
            InitializeComponent();
        }

        string blah = "";
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            blah = NavigationContext.QueryString["msg"];
        }

        //private void inputButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string str = blah;
        //    Service1Client client1 = new Service1Client();
        //    client1.SeePatientDataCompleted += new EventHandler<SeePatientDataCompletedEventArgs>(client1_SeePatientDataCompleted);
        //    client1.SeePatientDataAsync(str);
        //}

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
            
            Service1Client client1 = new Service1Client();
            client1.SeePatientDataCompleted += new EventHandler<SeePatientDataCompletedEventArgs>(client1_SeePatientDataCompleted);
            client1.SeePatientDataAsync(str);

            client1.SeeGeneralHistoryDataCompleted += new EventHandler<SeeGeneralHistoryDataCompletedEventArgs>(client1_SeeGeneralHistoryDataCompleted);
            client1.SeeGeneralHistoryDataAsync(str);
        }

        void client1_SeeGeneralHistoryDataCompleted(object sender, SeeGeneralHistoryDataCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Result != null)
            {
                Height.Text = e.Result.Height.ToString();
                Weight.Text = e.Result.Weight.ToString();
                BloodType.Text = e.Result.BloodType;
                BloodPressure.Text = e.Result.BloodPressure.ToString();
                Allergies.Text = e.Result.Allergies;
                BMI.Text = e.Result.BMI.ToString();
                Conditions.Text = e.Result.Conditions;
                Others.Text = e.Result.Others;
            }

            else
            {
                MessageBox.Show("General History does not exist for patient");
            }
        }

        //private void LayoutRoot_Loaded_1(object sender, RoutedEventArgs e)
        //{

        //}
    }
}