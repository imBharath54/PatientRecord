namespace PatientRecordMicroService.Models
{
    public class Record
    {

        public int Id { get; set; } 
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public int AppointmentId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Reason { get; set; }
        public string Notes { get; set; }

    }
}
