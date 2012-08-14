using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace CloudScrubsStorage
{

    /// <summary>
    /// Describes an entity in the ICD9MapPlotResults Table which stores all the results for the ICD9MapPlots
    /// </summary>
    public class ICD9MapPlotResult : TableServiceEntity
    {
        /// <summary>
        /// Constructor for the ICD9MapPlotResult
        /// 
        /// </summary>
        /// <param name="partitionKey">Set the ICD9 Parameter you are searching for</param>
        /// <param name="rowkey">Set the OperationID for this search</param>
        public ICD9MapPlotResult(string partitionKey,string rowkey)
        {
            base.PartitionKey = partitionKey;
            base.RowKey = rowkey;
        }

        /// <summary>
        /// Constructor for the ICD9MapPlotResult
        /// </summary>
        /// <param name="partitionKey">Set the OperationID for this search</param>
        public ICD9MapPlotResult(string partitionKey)
        {
            base.PartitionKey = partitionKey;
        }

        /// <summary>
        /// Gets or Sets a URL which contains the computed result data
        /// </summary>
        public string ResultURL { get; set; }

        /// <summary>
        /// Gets or Sets a randomly generated code that the user uses to track the result of his search
        /// </summary>
        public string SearchID { get; set; }

        /// <summary>
        /// Gets or Sets the starting time of the search timespan parameter
        /// </summary>
        public DateTime SearchTimeStart { get; set; }

        /// <summary>
        /// Gets or Sets the starting time of the end timespan parameter 
        /// </summary>
        public DateTime SearchTimeEnd { get; set; }
    }

    public class ICD9MapPlotResultEntry
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }

    }
}
