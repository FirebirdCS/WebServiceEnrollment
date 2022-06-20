using System.Runtime.Serialization;

namespace Web_Final.Models
{
    [DataContract]
    public class EnrollmentResponse
    {
        [DataMember]
        public int Codigo { get; set; }
        [DataMember]
        public string Respuesta { get; set; }


    }
}