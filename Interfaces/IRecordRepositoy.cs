using PatientRecordMicroService.Models;

namespace PatientRecordMicroService.Repositories
{
    public interface IRecordRepository
    {
        public Task<Record> GetRecordById(int id);
        public Task<IEnumerable<Record>> GetRecordsByPatientId(int patientId);
        public Task<IEnumerable<Record>> GetRecordsByDoctorId(int doctorId);
        public Task<IEnumerable<Record>> GetRecordsByAppointmentId(int appointmentId);
        public Task<IEnumerable<Record>> GetAllRecords();
        public Task Add(Record record);
        public Task Update(Record record);
        public Task Delete(int id);
    }
}
