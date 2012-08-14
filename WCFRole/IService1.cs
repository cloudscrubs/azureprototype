using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CloudScrubsStorage;

namespace WCFRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        // TODO: Add your service operations here

        [OperationContract]
        void AddPatientData(BasicDetails PatientData);

        [OperationContract]
        void AddGeneralHistoryData(GeneralHistory GenHistData);

        [OperationContract]
        BasicDetails SeePatientData(string str);

        [OperationContract]
        void UpdateBasicDetails(string str, BasicDetails PatientData);

        [OperationContract]
        GeneralHistory SeeGeneralHistoryData(string str);

        [OperationContract]
        void UpdateGeneralHistory(string str, GeneralHistory GenHistData);

        [OperationContract]
        void AddAilmentDetails(AilmentDetails AilData);

        [OperationContract]
        List<AilmentDetails> SeeAilmentDetailsData(string str);

        [OperationContract]
        AilmentDetails SeeSpecificAilmentDetailsData(string ssn, string rowkey);

        [OperationContract]
        void UpdateSpecificAilmentsData(string ssn, string rowkey, AilmentDetails AilData);

        [OperationContract]
        void AddDoctorBasicDetails(DoctorBasicDetails DocData);

        [OperationContract]
        void AddDoctorPatientConnectionDetails(DoctorPatientConnectionDetails DocPatientData);

        [OperationContract]
        void AddDoctorHospitalConnectionDetails(DoctorHospitalConnectionDetails DocHospData);

        [OperationContract]
        void AddHospitalBasicDetails(HospitalBasicDetails HospitalData);

        [OperationContract]
        void AddHospitalDoctorConnectionDetails(HospitalDoctorConnectionDetails HospDocData);

        [OperationContract]
        void AddHospitalPatientConnectionDetails(HospitalPatientConnectionDetails HospPatientData);

        [OperationContract]
        DoctorBasicDetails SeeDoctorBasicDetails(string DocID);

        [OperationContract]
        DoctorPatientConnectionDetails SeeDoctorPatientConnectionDetails(string DocID);

        [OperationContract]
        DoctorHospitalConnectionDetails SeeDoctorHospitalConnectionDetails(string DocID);

        [OperationContract]
        HospitalBasicDetails SeeHospitalBasicDetails(string HospID);

        [OperationContract]
        HospitalDoctorConnectionDetails SeeHospitalDoctorConnectionDetails(string HospID);

        [OperationContract]
        HospitalPatientConnectionDetails SeeHospitalPatientConnectionDetails(string HospID);

        [OperationContract]
        void UpdateDoctorBasicDetails(string DocID, DoctorBasicDetails DocData);

        [OperationContract]
        void UpdateDoctorPatientConnectionDetails(string DocID, DoctorPatientConnectionDetails DocPatientData);

        [OperationContract]
        void UpdateDoctorHospitalConnectionDetails(string DocID, DoctorHospitalConnectionDetails DocHospData);

        [OperationContract]
        void UpdateHospitalBasicDetails(string HospID, HospitalBasicDetails HospitalData);

        [OperationContract]
        void UpdateHospitalDoctorConnectionDetails(string HospID, HospitalDoctorConnectionDetails HospDocData);

        [OperationContract]
        void UpdateHospitalPatientConnectionDetails(string HospID, HospitalPatientConnectionDetails HospPatientData);

        [OperationContract]
        string ComputeICD9MapPlotData(string ICD9Code, DateTime startTime, DateTime endTime);

        [OperationContract]
        List<ICD9MapPlotResultEntry> GetICD9MapPlotData(string OperationID);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
