using System.ServiceModel;
using Web_Final.Models;

namespace Web_Final.Services
{
    [ServiceContract]
    public interface IEnrollmentService
    {
        [OperationContract]
        string Test(string s);
        [OperationContract]
        EnrollmentResponse EnrollmentProcess(EnrollmentRequest request);
        [OperationContract]
        CandidateRecordResponse CandidateRegisterProcess(CandidateRecordRequest request);
    }
}