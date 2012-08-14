using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudScrubsStorage
{
    public class DiabeticRetinopathyHit //:TableServiceEntity
    {
        public string SSN { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Doctor { get; set; }
    }
}
