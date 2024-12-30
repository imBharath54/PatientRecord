using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatientRecordMicroService.Models;

namespace PatientRecordMicroService.Services
{
    public interface IRecordService
    {
        Task<Record> GetRecordByIdAsync(int id);
        Task<IEnumerable<Record>> GetRecordsByPatientIdAsync(int patientId);
        Task<IEnumerable<Record>> GetRecordsByDoctorIdAsync(int doctorId);
        Task<IEnumerable<Record>> GetRecordsByAppointmentIdAsync(int appointmentId);
        Task<IEnumerable<Record>> GetAllRecordsAsync();
        Task AddRecordAsync(Record record);
        Task UpdateRecordAsync(Record record);
        Task DeleteRecordAsync(int id);
    }
}
