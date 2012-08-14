using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudScrubsStorage
{
    
    public class DoctorBasicDetails : TableServiceEntity
    {
        public DoctorBasicDetails()
        {
            base.PartitionKey = "DoctorBasicDetails";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();

        }

        public string DoctorID { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PersonalClinicID { get; set; }
    }

    public class DoctorPatientConnectionDetails : TableServiceEntity
    {
        public DoctorPatientConnectionDetails()
        {
            base.PartitionKey = "DoctorPatientConnectionDetails";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();

        }

        public string DoctorIDLinkRowKey { get; set; }
        public string PatientIDLinkRowKey { get; set; }
        public string AilmentDetailRowKey { get; set; }
    }

    public class DoctorHospitalConnectionDetails : TableServiceEntity
    {
        public DoctorHospitalConnectionDetails()
        {
            base.PartitionKey = "DoctorHospitalConnectionDetails";
            base.RowKey = DateTime.UtcNow.Ticks.ToString() + Guid.NewGuid().ToString();

        }

        public string DoctorIDLinkRowKey { get; set; }
        public string HospitalIDLinkRowKey { get; set; }
        public string DoctorHospitalPhoneNumber { get; set; }
    }
}
