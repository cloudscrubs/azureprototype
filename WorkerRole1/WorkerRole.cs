using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using CloudScrubsStorage;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        CloudStorageAccount account;
        CloudTableClient client;
        TableServiceContext tableContext;
        StorageCredentialsAccountAndKey accountAndKey = new StorageCredentialsAccountAndKey("cloudscrubs1", "jiOOIYMw6m2vZUsDVlTGqgWhy4VPuqq/JM6PqNpz0/ONkGIsIAORTNoHDJm38UuqIxskIOjF7OglQ/7prYJTxg==");
        CloudQueueClient qclient;
        CloudQueue q;
        List<ICD9MapPlotResultEntry> rows;
        CloudBlobClient bclient;
        CloudBlobContainer container;
        CloudBlob blob;

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("$projectname$ entry point called", "Information");

            while (true)
            {
                if (q.Exists())
                {
                    q.RetrieveApproximateMessageCount();
                    if (q.ApproximateMessageCount > 0)
                    {
                        Trace.WriteLine("Working", "Information");

                        var message = q.GetMessage();
                        q.DeleteMessage(message);
                        var request = message.AsString;
                        var parts = request.Split('|');

                        string ICD9Code = parts[0];
                        DateTime starttime = new DateTime(long.Parse(parts[1]));
                        DateTime endtime = new DateTime(long.Parse(parts[2]));
                        string OperationID = parts[3];
                        try
                        {
                            IQueryable<AilmentDetails> data = (from i in tableContext.CreateQuery<AilmentDetails>("PatientDetails") where i.PartitionKey == "AilmentDetails" && i.DiagnosisID == ICD9Code select i).AsQueryable<AilmentDetails>();

                            if (data.AsEnumerable<AilmentDetails>().Any<AilmentDetails>())
                            {
                                foreach (AilmentDetails x in data)
                                {
                                    if (DateTime.Compare(x.TimeIn, starttime) >= 0 && DateTime.Compare(x.TimeIn, endtime) <= 0)
                                    {
                                        string searchHospital = x.Hospital;

                                        IQueryable<HospitalBasicDetails> data2 = (from i in tableContext.CreateQuery<HospitalBasicDetails>("DoctorDetails") where i.PartitionKey == "HospitalBasicDetails" select i).AsQueryable<HospitalBasicDetails>();
                                        if (data2.AsEnumerable<HospitalBasicDetails>().Any<HospitalBasicDetails>())
                                        {
                                            HospitalBasicDetails z = new HospitalBasicDetails();
                                            var y = (from HospitalBasicDetails i in data2 where i.HospitalID == searchHospital select i).FirstOrDefault<HospitalBasicDetails>() as HospitalBasicDetails;
                                            if (y != null)
                                            {
                                                ICD9MapPlotResultEntry temp = new ICD9MapPlotResultEntry();
                                                temp.Latitude = y.Latitude;
                                                temp.Longitude = y.Longitude;
                                                temp.Time = x.TimeIn;
                                                rows.Add(temp);

                                            }

                                        }
                                    }
                                }
                            }
                            var serializer = new XmlSerializer(typeof(List<ICD9MapPlotResultEntry>));
                            var stringBuilder = new StringBuilder();
                            XmlWriter writer = XmlWriter.Create(stringBuilder);
                            serializer.Serialize(writer, rows);
                            Encoding encoding = Encoding.Default;
                            blob = container.GetBlobReference(OperationID + ".xml");
                            blob.UploadByteArray(encoding.GetBytes(stringBuilder.ToString()));

                            var resultrecord = new ICD9MapPlotResult(ICD9Code, OperationID);
                            resultrecord.SearchTimeStart = starttime;
                            resultrecord.SearchTimeEnd = endtime;
                            resultrecord.ResultURL = blob.Uri.ToString();
                            tableContext.AddObject("ICD9MapPlotResult", resultrecord);
                            tableContext.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine(e.Message, "Error");
                        }
                    }
                }
                Thread.Sleep(10000);


            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
#if DEBUG
            account = CloudStorageAccount.DevelopmentStorageAccount;
#else
            account = new CloudStorageAccount(accountAndKey, true);
#endif

            tableContext = new TableServiceContext(account.TableEndpoint.ToString(), account.Credentials);

            //client = new CloudQueueClient(account.BlobEndpoint.ToString(), account.Credentials);
            qclient = account.CreateCloudQueueClient();
            q = qclient.GetQueueReference("icd9mapplotrequests");
            rows = new List<ICD9MapPlotResultEntry>();
            bclient = account.CreateCloudBlobClient();
            container = bclient.GetContainerReference("results");
            container.CreateIfNotExist();
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("ICD9MapPlotResult");
            client.CreateTableIfNotExist("DoctorDetails");
            client.CreateTableIfNotExist("PatientDetails");
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
