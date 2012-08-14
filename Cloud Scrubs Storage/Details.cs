using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudScrubsStorage
{
    public class BasicDetails : TableServiceEntity
    {
        public BasicDetails()
        {
            base.PartitionKey = "BasicDetails";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }
        public string PhoneNumber { get; set; }
        public string NextOfKin { get; set; }
        public string LegalStatus { get; set; }
        public string MedicalInsurance { get; set; }
        public string Nationality { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    

    public class AilmentDetails : TableServiceEntity
    {
        public AilmentDetails()
        {
            base.PartitionKey = "AilmentDetails";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();

        }

        public string PatientIDLinkRowKey { get; set; }
        public string AilmentDetailRowKey { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string DiagnosisID { get; set; }
        public string Treatment { get; set; }
        public string Medication { get; set; }
        public string Lab_Radiology { get; set; }
        public string Lab_Physical { get; set; }
        public string Lab_Pathology { get; set; }
        public string Hospital { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string GeneralPhysician { get; set; }
        public string AttendingPhysician { get; set; }
        public string ProgressNotes { get; set; }
    }

    public class GeneralHistory : TableServiceEntity
    {
        public GeneralHistory()
        {
            base.PartitionKey = "GeneralHistory";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();

        }

        public string PatientIDLinkRowKey { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public string Conditions { get; set; }
        public int BloodPressure { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
        public string Others { get; set; }
    }
}