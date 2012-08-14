using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudScrubsStorage
{
    public class HospitalBasicDetails : TableServiceEntity
    {
        public HospitalBasicDetails()
        {
            base.PartitionKey = "HospitalBasicDetails";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();
        }

        public string HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Facilities { get; set; }
        public string Departments { get; set; }
        public int Beds_Rooms { get; set; }
        public string Type { get; set; }
    }

    public class HospitalDoctorConnectionDetails : TableServiceEntity
    {
        public HospitalDoctorConnectionDetails()
        {
            base.PartitionKey = "HospitalDoctorConnections";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();
        }

        public string HospitalIDLinkRowKey { get; set; }
        public string DoctorIDLinkRowKey { get; set; }
        public string DoctorHospitalPhoneNumber { get; set; }
    }

    public class HospitalPatientConnectionDetails : TableServiceEntity
    {
        public HospitalPatientConnectionDetails()
        {
            base.PartitionKey = "HospitalPatientConnections";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();
        }

        public string HospitalIDLinkRowKey { get; set; }
        public string PatientIDLinkRowKey { get; set; }
    }
}
