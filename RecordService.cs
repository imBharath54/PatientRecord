using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientRecordMicroService.Models;
using PatientRecordMicroService.Repositories;

namespace PatientRecordMicroService.Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        // Get a record by its ID
        public async Task<Record> GetRecordByIdAsync(int id)
        {
            var record = await _recordRepository.GetRecordById(id);
            if (record == null)
            {
                // Optionally, you can throw an exception or handle this scenario differently.
                throw new KeyNotFoundException($"Record with ID {id} not found.");
            }

            return record;
        }

        // Get all records for a specific patient
        public async Task<IEnumerable<Record>> GetRecordsByPatientIdAsync(int patientId)
        {
            return await _recordRepository.GetRecordsByPatientId(patientId);
        }

        // Get all records for a specific doctor
        public async Task<IEnumerable<Record>> GetRecordsByDoctorIdAsync(int doctorId)
        {
            return await _recordRepository.GetRecordsByDoctorId(doctorId);
        }

        // Get all records for a specific appointment
        public async Task<IEnumerable<Record>> GetRecordsByAppointmentIdAsync(int appointmentId)
        {
            return await _recordRepository.GetRecordsByAppointmentId(appointmentId);
        }

        // Get all records from the database
        public async Task<IEnumerable<Record>> GetAllRecordsAsync()
        {
            return await _recordRepository.GetAllRecords();
        }

        // Add a new record to the database
        public async Task AddRecordAsync(Record record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("Record cannot be null");
            }

            // Here you can include additional validation logic before adding the record
            if (string.IsNullOrEmpty(record.Reason))
            {
                throw new ArgumentException("Reason for the record must be provided.");
            }

            await _recordRepository.Add(record);
        }

        // Update an existing record
        public async Task UpdateRecordAsync(Record record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("Record cannot be null");
            }

            // Optionally, perform more validation or checks here before updating
            if (string.IsNullOrEmpty(record.Reason))
            {
                throw new ArgumentException("Reason for the record must be provided.");
            }

            await _recordRepository.Update(record);
        }

        // Delete a record from the database
        public async Task DeleteRecordAsync(int id)
        {
            var record = await _recordRepository.GetRecordById(id);
            if (record == null)
            {
                throw new KeyNotFoundException($"Record with ID {id} not found.");
            }

            await _recordRepository.Delete(id);
        }
    }
}
