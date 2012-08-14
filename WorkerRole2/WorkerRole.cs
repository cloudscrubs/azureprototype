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

namespace WorkerRole2
{
    public class WorkerRole : RoleEntryPoint
    {
        CloudStorageAccount account;
        CloudTableClient client;
        TableServiceContext tableContext;
        StorageCredentialsAccountAndKey accountAndKey = new StorageCredentialsAccountAndKey("cloudscrubs1", "jiOOIYMw6m2vZUsDVlTGqgWhy4VPuqq/JM6PqNpz0/ONkGIsIAORTNoHDJm38UuqIxskIOjF7OglQ/7prYJTxg==");
        CloudBlobClient bclient;
        CloudBlobContainer container;

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("$projectname$ entry point called", "Information");

            while (true)
            {
                Thread.Sleep(10000);
                IQueryable<AilmentDetails> data = (from i in tableContext.CreateQuery<AilmentDetails>("PatientDetails") where i.PartitionKey == "AilmentDetails" && i.DiagnosisID == "249" select i).AsQueryable<AilmentDetails>();
                if (data.AsEnumerable<AilmentDetails>().Any<AilmentDetails>())
                {
                    List<DiabeticRetinopathyHit> hits = new List<DiabeticRetinopathyHit>();
                    foreach (AilmentDetails x in data)
                    {
                        if (x.TimeIn.AddYears(10) < DateTime.Now)
                        {
                            var foo = (from bar in tableContext.CreateQuery<BasicDetails>("PatientDetails") where bar.SSN == x.PatientIDLinkRowKey select bar).FirstOrDefault<BasicDetails>();
                            if (foo != null)
                            {
                                hits.Add(new DiabeticRetinopathyHit { Doctor = x.GeneralPhysician, Latitude = foo.Latitude, Longitude = foo.Longitude, Name = foo.Name, SSN = foo.SSN });
                            }
                        }
                    }

                    var serializer = new XmlSerializer(typeof(List<DiabeticRetinopathyHit>));
                    var stringBuilder = new StringBuilder();
                    XmlWriter writer = XmlWriter.Create(stringBuilder);
                    serializer.Serialize(writer, hits);
                    Encoding encoding = Encoding.Default;
                    CloudBlob blob = container.GetBlobReference("DiabeticRetinopatyHits.xml");
                    blob.UploadByteArray(encoding.GetBytes(stringBuilder.ToString()));

                }
                Trace.WriteLine("Working", "Information");
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
            client = account.CreateCloudTableClient();
            client.CreateTableIfNotExist("PatientDetails");
            bclient = account.CreateCloudBlobClient();
            container = bclient.GetContainerReference("results");

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
