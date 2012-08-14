using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using CloudScrubsStorage;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WCFRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        CloudStorageAccount account;
        CloudTableClient client;
        TableServiceContext tableContext;
        CloudQueueClient qclient;
        CloudQueue q;


        StorageCredentialsAccountAndKey accountAndKey = new StorageCredentialsAccountAndKey("cloudscrubs1", "jiOOIYMw6m2vZUsDVlTGqgWhy4VPuqq/JM6PqNpz0/ONkGIsIAORTNoHDJm38UuqIxskIOjF7OglQ/7prYJTxg==");

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        //ADD BASIC DETAILS

        public void AddPatientData(BasicDetails PatientData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            BasicDetails x = new BasicDetails();
            x.Name = PatientData.Name;
            x.SSN = PatientData.SSN;
            x.DOB = PatientData.DOB;
            x.LegalStatus = PatientData.LegalStatus;
            x.MedicalInsurance = PatientData.MedicalInsurance;
            x.Gender = PatientData.Gender;
            x.Address = PatientData.Address;
            x.Nationality = PatientData.Nationality;
            x.NextOfKin = PatientData.NextOfKin;
            x.PhoneNumber = PatientData.PhoneNumber;
            x.Longitude = PatientData.Longitude;
            x.Latitude = PatientData.Latitude;



            tableContext.AddObject("PatientDetails", x);
            tableContext.SaveChanges();
        }

        //ADD GENERAL HISTORY

        public void AddGeneralHistoryData(GeneralHistory GenHistData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            GeneralHistory x = new GeneralHistory();
            x.Allergies = GenHistData.Allergies;
            x.BloodPressure = GenHistData.BloodPressure;
            x.BloodType = GenHistData.BloodType;
            x.BMI = GenHistData.BMI;
            x.Conditions = GenHistData.Conditions;
            x.Height = GenHistData.Height;
            x.Weight = GenHistData.Weight;
            x.Others = GenHistData.Others;
            x.PatientIDLinkRowKey = GenHistData.PatientIDLinkRowKey;



            tableContext.AddObject("PatientDetails", x);
            tableContext.SaveChanges();
        }

        //ADD AILMENT DETAILS

        public void AddAilmentDetails(AilmentDetails AilData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            AilmentDetails x = new AilmentDetails();
            x.AttendingPhysician = AilData.AttendingPhysician;
            x.Diagnosis = AilData.Diagnosis;
            x.DiagnosisID = AilData.DiagnosisID;
            x.GeneralPhysician = AilData.GeneralPhysician;
            x.Hospital = AilData.Hospital;
            x.Lab_Pathology = AilData.Lab_Pathology;
            x.Lab_Physical = AilData.Lab_Physical;
            x.Lab_Radiology = AilData.Lab_Physical;
            x.Medication = AilData.Medication;
            x.PatientIDLinkRowKey = AilData.PatientIDLinkRowKey;
            x.ProgressNotes = AilData.ProgressNotes;
            x.Symptoms = AilData.Symptoms;
            x.TimeIn = AilData.TimeIn;
            x.TimeOut = AilData.TimeOut;
            x.Treatment = AilData.Treatment;
            x.AilmentDetailRowKey = AilData.AilmentDetailRowKey;


            tableContext.AddObject("PatientDetails", x);
            tableContext.SaveChanges();
        }

        //VIEW GENERAL HISTORY

        public GeneralHistory SeeGeneralHistoryData(string SSN)
        {
            if (SSN == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<GeneralHistory> data = (from i in tableContext.CreateQuery<GeneralHistory>("PatientDetails") where i.PartitionKey == "GeneralHistory" select i).AsQueryable<GeneralHistory>();
            //Label1.Text = "";
            if (data.AsEnumerable<GeneralHistory>().Any<GeneralHistory>())
            {

                GeneralHistory z = new GeneralHistory();

                var y = (from GeneralHistory i in data where i.PatientIDLinkRowKey == SSN select i).FirstOrDefault<GeneralHistory>() as GeneralHistory;

                if (y != null)
                {

                    z = y;


                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;
        }

        //VIEW ALL AILMENTS

        public List<AilmentDetails> SeeAilmentDetailsData(string SSN)
        {
            if (SSN == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<AilmentDetails> data = (from i in tableContext.CreateQuery<AilmentDetails>("PatientDetails") where i.PartitionKey == "AilmentDetails" && i.PatientIDLinkRowKey == SSN select i).AsQueryable<AilmentDetails>();

            if (data != null)
            {
                return data.ToList();
            }

            else return null;
        }

        //VIEW SPECIFIC AILMENT DETAILS

        public AilmentDetails SeeSpecificAilmentDetailsData(string SSN, string rowkey)
        {
            if (SSN == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<AilmentDetails> data = (from i in tableContext.CreateQuery<AilmentDetails>("PatientDetails") where i.PartitionKey == "AilmentDetails" && i.PatientIDLinkRowKey == SSN
 && i.AilmentDetailRowKey == rowkey select i).AsQueryable<AilmentDetails>();

            if (data.AsEnumerable<AilmentDetails>().Any<AilmentDetails>())
            {

                AilmentDetails z = new AilmentDetails();

                var y = (from AilmentDetails i in data select i).FirstOrDefault<AilmentDetails>() as AilmentDetails;

                if (y != null)
                {
                    z = y;

                }

                else
                {
                    z = null;

                }
                return z;

            }

            else return null;
        }

        //VIEW BASIC DETAILS

        public BasicDetails SeePatientData(string SSN)
        {
            if (SSN == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<BasicDetails> data = (from i in tableContext.CreateQuery<BasicDetails>("PatientDetails") where i.PartitionKey == "BasicDetails" select i).AsQueryable<BasicDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<BasicDetails>().Any<BasicDetails>())
            {

                BasicDetails z = new BasicDetails();

                var y = (from BasicDetails i in data where i.SSN == SSN select i).FirstOrDefault<BasicDetails>() as BasicDetails;

                if (y != null)
                {
                    z = y;

                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;
        }

        //UPDATE BASIC DETAILS

        public void UpdateBasicDetails(string SSN, BasicDetails PatientData)
        {
            if (SSN == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<BasicDetails> data = (from i in tableContext.CreateQuery<BasicDetails>("PatientDetails") where i.PartitionKey == "BasicDetails" select i).AsQueryable<BasicDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<BasicDetails>().Any<BasicDetails>())
            {
                var y = (from BasicDetails i in data where i.SSN == SSN select i).FirstOrDefault<BasicDetails>() as BasicDetails;

                if (y != null)
                {

                    y.Name = PatientData.Name;
                    //y.SSN = PatientData.SSN;
                    y.DOB = PatientData.DOB;
                    y.LegalStatus = PatientData.LegalStatus;
                    y.MedicalInsurance = PatientData.MedicalInsurance;
                    y.Gender = PatientData.Gender;
                    y.Address = PatientData.Address;
                    y.Nationality = PatientData.Nationality;
                    y.NextOfKin = PatientData.NextOfKin;
                    y.PhoneNumber = PatientData.PhoneNumber;

                    //tableContext.AddObject("PatientDetails", x);
                    tableContext.UpdateObject(y);
                    tableContext.SaveChanges();
                }
            }
        }

        //UPDATE SPECIFIC AILMENT DETAILS

        public void UpdateSpecificAilmentsData(string SSN, string rowkey, AilmentDetails AilData)
        {
            if (SSN == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<AilmentDetails> data = (from i in tableContext.CreateQuery<AilmentDetails>("PatientDetails") where i.PartitionKey == "AilmentDetails" select i).AsQueryable<AilmentDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<AilmentDetails>().Any<AilmentDetails>())
            {



                var x = (from AilmentDetails i in data where i.PatientIDLinkRowKey == SSN && i.AilmentDetailRowKey == rowkey select i).FirstOrDefault<AilmentDetails>() as AilmentDetails;

                if (x != null)
                {
                    //x.AilmentDetailRowKey = AilData.AilmentDetailRowKey;
                    x.AttendingPhysician = AilData.AttendingPhysician;
                    x.Diagnosis = AilData.Diagnosis;
                    x.GeneralPhysician = AilData.GeneralPhysician;
                    x.Hospital = AilData.Hospital;
                    x.Lab_Pathology = AilData.Lab_Pathology;
                    x.Lab_Physical = AilData.Lab_Physical;
                    x.Lab_Radiology = AilData.Lab_Physical;
                    x.Medication = AilData.Medication;
                    //x.PatientIDLinkRowKey = AilData.PatientIDLinkRowKey;
                    x.ProgressNotes = AilData.ProgressNotes;
                    x.Symptoms = AilData.Symptoms;
                    x.TimeIn = AilData.TimeIn;
                    x.TimeOut = AilData.TimeOut;
                    x.Treatment = AilData.Treatment;

                    //tableContext.AddObject("PatientDetails", x);
                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();
                }
            }
        }

        //UPDATE GENERAL HISTORY

        public void UpdateGeneralHistory(string SSN, GeneralHistory GenHistData)
        {
            if (SSN == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<GeneralHistory> data = (from i in tableContext.CreateQuery<GeneralHistory>("PatientDetails") where i.PartitionKey == "GeneralHistory" select i).AsQueryable<GeneralHistory>();
            //Label1.Text = "";
            if (data.AsEnumerable<GeneralHistory>().Any<GeneralHistory>())
            {

                var x = (from GeneralHistory i in data where i.PatientIDLinkRowKey == SSN select i).FirstOrDefault<GeneralHistory>() as GeneralHistory;

                if (x != null)
                {
                    x.Allergies = GenHistData.Allergies;
                    x.BloodPressure = GenHistData.BloodPressure;
                    x.BloodType = GenHistData.BloodType;
                    x.BMI = GenHistData.BMI;
                    x.Conditions = GenHistData.Conditions;
                    x.Height = GenHistData.Height;
                    x.Weight = GenHistData.Weight;
                    x.Others = GenHistData.Others;
                    //x.PatientIDLinkRowKey = GenHistData.PatientIDLinkRowKey;

                    //tableContext.AddObject("PatientDetails", x);
                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();
                }
            }
        }

        //ADD DOCTOR DETAILS

        public void AddDoctorBasicDetails(DoctorBasicDetails DocData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            DoctorBasicDetails x = new DoctorBasicDetails();

            x.DoctorID = DocData.DoctorID;
            x.Name = DocData.Name;
            x.Specialization = DocData.Specialization;
            x.PhoneNumber = DocData.PhoneNumber;
            x.Email = DocData.Email;
            x.PersonalClinicID = DocData.PersonalClinicID;


            tableContext.AddObject("DoctorDetails", x);
            tableContext.SaveChanges();
        }

        public void AddDoctorPatientConnectionDetails(DoctorPatientConnectionDetails DocPatientData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            DoctorPatientConnectionDetails x = new DoctorPatientConnectionDetails();

            x.DoctorIDLinkRowKey = DocPatientData.DoctorIDLinkRowKey;
            x.PatientIDLinkRowKey = DocPatientData.PatientIDLinkRowKey;
            x.AilmentDetailRowKey = DocPatientData.AilmentDetailRowKey;

            tableContext.AddObject("DoctorDetails", x);
            tableContext.SaveChanges();
        }

        public void AddDoctorHospitalConnectionDetails(DoctorHospitalConnectionDetails DocHospData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            DoctorHospitalConnectionDetails x = new DoctorHospitalConnectionDetails();

            x.DoctorIDLinkRowKey = DocHospData.DoctorIDLinkRowKey;
            x.HospitalIDLinkRowKey = DocHospData.HospitalIDLinkRowKey;
            x.DoctorHospitalPhoneNumber = DocHospData.DoctorHospitalPhoneNumber;

            tableContext.AddObject("DoctorDetails", x);
            tableContext.SaveChanges();
        }

        public void AddHospitalBasicDetails(HospitalBasicDetails HospitalData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            HospitalBasicDetails x = new HospitalBasicDetails();

            x.HospitalID = HospitalData.HospitalID;
            x.HospitalName = HospitalData.HospitalName;
            x.Address = HospitalData.Address;
            x.Latitude = HospitalData.Latitude;
            x.Longitude = HospitalData.Longitude;
            x.Facilities = HospitalData.Facilities;
            x.Departments = HospitalData.Departments;
            x.Beds_Rooms = HospitalData.Beds_Rooms;
            x.Type = HospitalData.Type;

            tableContext.AddObject("DoctorDetails", x);
            tableContext.SaveChanges();
        }

        public void AddHospitalDoctorConnectionDetails(HospitalDoctorConnectionDetails HospDocData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            HospitalDoctorConnectionDetails x = new HospitalDoctorConnectionDetails();

            x.HospitalIDLinkRowKey = HospDocData.HospitalIDLinkRowKey;
            x.DoctorIDLinkRowKey = HospDocData.DoctorIDLinkRowKey;
            x.DoctorHospitalPhoneNumber = HospDocData.DoctorHospitalPhoneNumber;

            tableContext.AddObject("DoctorDetails", x);
            tableContext.SaveChanges();
        }

        public void AddHospitalPatientConnectionDetails(HospitalPatientConnectionDetails HospPatientData)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            HospitalPatientConnectionDetails x = new HospitalPatientConnectionDetails();

            x.HospitalIDLinkRowKey = HospPatientData.HospitalIDLinkRowKey;
            x.PatientIDLinkRowKey = HospPatientData.PatientIDLinkRowKey;

            tableContext.AddObject("DoctorDetails", x);
            tableContext.SaveChanges();
        }

        public DoctorBasicDetails SeeDoctorBasicDetails(string DocID)
        {
            if (DocID == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<DoctorBasicDetails> data = (from i in tableContext.CreateQuery<DoctorBasicDetails>("DoctorDetails") where i.PartitionKey == "DoctorBasicDetails" select i).AsQueryable<DoctorBasicDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<DoctorBasicDetails>().Any<DoctorBasicDetails>())
            {

                DoctorBasicDetails z = new DoctorBasicDetails();

                var y = (from DoctorBasicDetails i in data where i.DoctorID == DocID select i).FirstOrDefault<DoctorBasicDetails>() as DoctorBasicDetails;

                if (y != null)
                {

                    z = y;


                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;
        }

        public DoctorPatientConnectionDetails SeeDoctorPatientConnectionDetails(string DocID)
        {
            if (DocID == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<DoctorPatientConnectionDetails> data = (from i in tableContext.CreateQuery<DoctorPatientConnectionDetails>("DoctorDetails") where i.PartitionKey == "DoctorPatientConnectionDetails" select i).AsQueryable<DoctorPatientConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<DoctorPatientConnectionDetails>().Any<DoctorPatientConnectionDetails>())
            {

                DoctorPatientConnectionDetails z = new DoctorPatientConnectionDetails();

                var y = (from DoctorPatientConnectionDetails i in data where i.DoctorIDLinkRowKey == DocID select i).FirstOrDefault<DoctorPatientConnectionDetails>() as DoctorPatientConnectionDetails;

                if (y != null)
                {

                    z = y;


                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;
        
        }

        public DoctorHospitalConnectionDetails SeeDoctorHospitalConnectionDetails(string DocID)
        {
            if (DocID == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<DoctorHospitalConnectionDetails> data = (from i in tableContext.CreateQuery<DoctorHospitalConnectionDetails>("DoctorDetails") where i.PartitionKey == "DoctorHospitalConnectionDetails" select i).AsQueryable<DoctorHospitalConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<DoctorHospitalConnectionDetails>().Any<DoctorHospitalConnectionDetails>())
            {

                DoctorHospitalConnectionDetails z = new DoctorHospitalConnectionDetails();

                var y = (from DoctorHospitalConnectionDetails i in data where i.DoctorIDLinkRowKey == DocID select i).FirstOrDefault<DoctorHospitalConnectionDetails>() as DoctorHospitalConnectionDetails;

                if (y != null)
                {

                    z = y;


                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;

        }

        public HospitalBasicDetails SeeHospitalBasicDetails(string HospID)
        {
            if (HospID == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<HospitalBasicDetails> data = (from i in tableContext.CreateQuery<HospitalBasicDetails>("DoctorDetails") where i.PartitionKey == "HospitalBasicDetails" select i).AsQueryable<HospitalBasicDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<HospitalBasicDetails>().Any<HospitalBasicDetails>())
            {

                HospitalBasicDetails z = new HospitalBasicDetails();

                var y = (from HospitalBasicDetails i in data where i.HospitalID == HospID select i).FirstOrDefault<HospitalBasicDetails>() as HospitalBasicDetails;

                if (y != null)
                {

                    z = y;


                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;

        }

        public HospitalDoctorConnectionDetails SeeHospitalDoctorConnectionDetails(string HospID)
        {
            if (HospID == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<HospitalDoctorConnectionDetails> data = (from i in tableContext.CreateQuery<HospitalDoctorConnectionDetails>("DoctorDetails") where i.PartitionKey == "HospitalDoctorConnectionDetails" select i).AsQueryable<HospitalDoctorConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<HospitalDoctorConnectionDetails>().Any<HospitalDoctorConnectionDetails>())
            {

                HospitalDoctorConnectionDetails z = new HospitalDoctorConnectionDetails();

                var y = (from HospitalDoctorConnectionDetails i in data where i.HospitalIDLinkRowKey == HospID select i).FirstOrDefault<HospitalDoctorConnectionDetails>() as HospitalDoctorConnectionDetails;

                if (y != null)
                {

                    z = y;


                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;

        }

        public HospitalPatientConnectionDetails SeeHospitalPatientConnectionDetails(string HospID)
        {
            if (HospID == null)
                return null;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            if (HospID == null)
                return null;
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<HospitalPatientConnectionDetails> data = (from i in tableContext.CreateQuery<HospitalPatientConnectionDetails>("DoctorDetails") where i.PartitionKey == "HospitalPatientConnectionDetails" select i).AsQueryable<HospitalPatientConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<HospitalPatientConnectionDetails>().Any<HospitalPatientConnectionDetails>())
            {

                HospitalPatientConnectionDetails z = new HospitalPatientConnectionDetails();

                var y = (from HospitalPatientConnectionDetails i in data where i.HospitalIDLinkRowKey == HospID select i).FirstOrDefault<HospitalPatientConnectionDetails>() as HospitalPatientConnectionDetails;

                if (y != null)
                {

                    z = y;


                }

                else
                {
                    z = null;

                }
                return z;

            }
            else return null;

        }

        public void UpdateDoctorBasicDetails(string DocID, DoctorBasicDetails DocData)
        {
            if (DocID == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<DoctorBasicDetails> data = (from i in tableContext.CreateQuery<DoctorBasicDetails>("DoctorDetails") where i.PartitionKey == "DoctorBasicDetails" select i).AsQueryable<DoctorBasicDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<DoctorBasicDetails>().Any<DoctorBasicDetails>())
            {

                //DoctorBasicDetails z = new DoctorBasicDetails();

                var x = (from DoctorBasicDetails i in data where i.DoctorID == DocID select i).FirstOrDefault<DoctorBasicDetails>() as DoctorBasicDetails;

                if (x != null)
                {

                    //x.DoctorID = DocData.DoctorID;
                    x.Name = DocData.Name;
                    x.Specialization = DocData.Specialization;
                    x.PhoneNumber = DocData.PhoneNumber;
                    x.Email = DocData.Email;
                    x.PersonalClinicID = DocData.PersonalClinicID;

                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();

                }              

            }
            
        }

        public void UpdateDoctorPatientConnectionDetails(string DocID, DoctorPatientConnectionDetails DocPatientData)
        {
            if (DocID == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<DoctorPatientConnectionDetails> data = (from i in tableContext.CreateQuery<DoctorPatientConnectionDetails>("DoctorDetails") where i.PartitionKey == "DoctorPatientConnectionDetails" select i).AsQueryable<DoctorPatientConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<DoctorPatientConnectionDetails>().Any<DoctorPatientConnectionDetails>())
            {

                DoctorPatientConnectionDetails z = new DoctorPatientConnectionDetails();

                var x = (from DoctorPatientConnectionDetails i in data where i.DoctorIDLinkRowKey == DocID select i).FirstOrDefault<DoctorPatientConnectionDetails>() as DoctorPatientConnectionDetails;

                if (x != null)
                {

                    //x.DoctorIDLinkRowKey = DocPatientData.DoctorIDLinkRowKey;
                    x.PatientIDLinkRowKey = DocPatientData.PatientIDLinkRowKey;
                    x.AilmentDetailRowKey = DocPatientData.AilmentDetailRowKey;

                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();


                }                

            }

        }

        public void UpdateDoctorHospitalConnectionDetails(string DocID, DoctorHospitalConnectionDetails DocHospData)
        {
            if (DocID == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<DoctorHospitalConnectionDetails> data = (from i in tableContext.CreateQuery<DoctorHospitalConnectionDetails>("DoctorDetails") where i.PartitionKey == "DoctorHospitalConnectionDetails" select i).AsQueryable<DoctorHospitalConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<DoctorHospitalConnectionDetails>().Any<DoctorHospitalConnectionDetails>())
            {

                DoctorHospitalConnectionDetails z = new DoctorHospitalConnectionDetails();

                var x = (from DoctorHospitalConnectionDetails i in data where i.DoctorIDLinkRowKey == DocID select i).FirstOrDefault<DoctorHospitalConnectionDetails>() as DoctorHospitalConnectionDetails;

                if (x != null)
                {
                    //x.DoctorIDLinkRowKey = DocHospData.DoctorIDLinkRowKey;
                    x.HospitalIDLinkRowKey = DocHospData.HospitalIDLinkRowKey;
                    x.DoctorHospitalPhoneNumber = DocHospData.DoctorHospitalPhoneNumber;

                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();


                }                

            }

        }

        public void UpdateHospitalBasicDetails(string HospID, HospitalBasicDetails HospitalData)
        {
            if (HospID == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<HospitalBasicDetails> data = (from i in tableContext.CreateQuery<HospitalBasicDetails>("DoctorDetails") where i.PartitionKey == "HospitalBasicDetails" select i).AsQueryable<HospitalBasicDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<HospitalBasicDetails>().Any<HospitalBasicDetails>())
            {

                HospitalBasicDetails z = new HospitalBasicDetails();

                var x = (from HospitalBasicDetails i in data where i.HospitalID == HospID select i).FirstOrDefault<HospitalBasicDetails>() as HospitalBasicDetails;

                if (x != null)
                {

                    //x.HospitalID = HospitalData.HospitalID;
                    x.HospitalName = HospitalData.HospitalName;
                    x.Address = HospitalData.Address;
                    x.Latitude = HospitalData.Latitude;
                    x.Longitude = HospitalData.Longitude;
                    x.Facilities = HospitalData.Facilities;
                    x.Departments = HospitalData.Departments;
                    x.Beds_Rooms = HospitalData.Beds_Rooms;
                    x.Type = HospitalData.Type;

                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();


                }               

            }
            
        }

        public void UpdateHospitalDoctorConnectionDetails(string HospID, HospitalDoctorConnectionDetails HospDocData)
        {
            if (HospID == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<HospitalDoctorConnectionDetails> data = (from i in tableContext.CreateQuery<HospitalDoctorConnectionDetails>("DoctorDetails") where i.PartitionKey == "HospitalDoctorConnectionDetails" select i).AsQueryable<HospitalDoctorConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<HospitalDoctorConnectionDetails>().Any<HospitalDoctorConnectionDetails>())
            {

                HospitalDoctorConnectionDetails z = new HospitalDoctorConnectionDetails();

                var x = (from HospitalDoctorConnectionDetails i in data where i.HospitalIDLinkRowKey == HospID select i).FirstOrDefault<HospitalDoctorConnectionDetails>() as HospitalDoctorConnectionDetails;

                if (x != null)
                {

                    //x.HospitalIDLinkRowKey = HospDocData.HospitalIDLinkRowKey;
                    x.DoctorIDLinkRowKey = HospDocData.DoctorIDLinkRowKey;
                    x.DoctorHospitalPhoneNumber = HospDocData.DoctorHospitalPhoneNumber;

                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();


                }
            }

            }
        
        public void UpdateHospitalPatientConnectionDetails(string HospID, HospitalPatientConnectionDetails HospPatientData)
        {
            if (HospID == null)
                return;

#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else 
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("DoctorDetails");
            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            IQueryable<HospitalPatientConnectionDetails> data = (from i in tableContext.CreateQuery<HospitalPatientConnectionDetails>("DoctorDetails") where i.PartitionKey == "HospitalPatientConnectionDetails" select i).AsQueryable<HospitalPatientConnectionDetails>();
            //Label1.Text = "";
            if (data.AsEnumerable<HospitalPatientConnectionDetails>().Any<HospitalPatientConnectionDetails>())
            {

                HospitalPatientConnectionDetails z = new HospitalPatientConnectionDetails();

                var x = (from HospitalPatientConnectionDetails i in data where i.HospitalIDLinkRowKey == HospID select i).FirstOrDefault<HospitalPatientConnectionDetails>() as HospitalPatientConnectionDetails;

                if (x != null)
                {

                    //x.HospitalIDLinkRowKey = HospPatientData.HospitalIDLinkRowKey;
                    x.PatientIDLinkRowKey = HospPatientData.PatientIDLinkRowKey;

                    tableContext.UpdateObject(x);
                    tableContext.SaveChanges();


                }

            }

        }

        public string ComputeICD9MapPlotData(string ICD9Code, DateTime startTime, DateTime endTime)
        {
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            //client = new CloudQueueClient(account.BlobEndpoint.ToString(), account.Credentials);
            qclient = account.CreateCloudQueueClient();
            q = qclient.GetQueueReference("icd9mapplotrequests");
            q.CreateIfNotExist();

            string OperationID = Guid.NewGuid().ToString();

            string starttime = startTime.Ticks.ToString();

            string endtime = endTime.Ticks.ToString();

            q.AddMessage(new CloudQueueMessage(string.Format("{0}|{1}|{2}|{3}",ICD9Code,starttime,endtime,OperationID)));

            return OperationID;
        }

        public List<ICD9MapPlotResultEntry> GetICD9MapPlotData(string OperationID)
        {
            List<ICD9MapPlotResultEntry> result;
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif
            CloudBlobClient bclient = account.CreateCloudBlobClient();
            CloudBlobContainer container = bclient.GetContainerReference("results");
            container.CreateIfNotExist();
            CloudBlob blob = container.GetBlobReference(OperationID+".xml");
            
            Stream stream = new MemoryStream(); 
               
            //string myXml = encoding.GetString(contents.AsBytes()); 
            StringReader stringReader = new StringReader(blob.DownloadText()); 
            var reader = new XmlTextReader(stringReader);
            var deserializer = new XmlSerializer(typeof(List<ICD9MapPlotResultEntry>));
            result = (List<ICD9MapPlotResultEntry>)deserializer.Deserialize(reader);
           

            return result;
        }
    }
}
