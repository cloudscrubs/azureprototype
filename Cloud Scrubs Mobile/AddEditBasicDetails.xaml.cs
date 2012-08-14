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
    public partial class AddEditBasicDetails : PhoneApplicationPage
    {
        public AddEditBasicDetails()
        {
            InitializeComponent();
        }

        private void add_data(object sender, EventArgs e)
        {            
            Service1Client client1 = new Service1Client();
            client1.AddPatientDataCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client1_AddPatientDataCompleted);

            BasicDetails o = new BasicDetails();

            o.Name = Name.Text;
            o.SSN = SSN.Text;
            o.DOB = (DateTime)DOB.Value;
            o.Gender = Gender.Text;
            o.Address = Address.Text;
            o.Nationality = Nationality.Text;
            o.PhoneNumber = PhoneNumber.Text;
            o.LegalStatus = LegalStatus.Text;
            o.MedicalInsurance = Insurance.Text;
            o.NextOfKin = NextOfKin.Text;

            client1.AddPatientDataAsync(o);
        }

        void client1_AddPatientDataCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();

            MessageBox.Show("Data added");
        }
    }
}