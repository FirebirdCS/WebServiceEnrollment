using System.Runtime.Serialization;

namespace Web_Final.Models
{
    [DataContract]
    public class EnrollmentRequest
    {
        [DataMember]
        public string NoExpediente { get; set; }
        [DataMember]
        public string Ciclo { get; set; }
        [DataMember]
        public int MesInicioPago { get; set; }
        [DataMember]
        public string CarreraId { get; set; }
    }
}