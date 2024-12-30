using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientRecordMicroService.Models;
using PatientRecordMicroService.Context;

namespace PatientRecordMicroService.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationContext _context;

        // Constructor to inject the DbContext
        public RecordRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Get a record by its ID
        public async Task<Record> GetRecordById(int id)
        {
            return await _context.Records
                .AsNoTracking()  // to avoid unintentional updates
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // Get all records for a specific patient
        public async Task<IEnumerable<Record>> GetRecordsByPatientId(int patientId)
        {
            return await _context.Records
                .AsNoTracking()
                .Where(r => r.PatientId == patientId)
                .ToListAsync();
        }

        // Get all records for a specific doctor
        public async Task<IEnumerable<Record>> GetRecordsByDoctorId(int doctorId)
        {
            return await _context.Records
                .AsNoTracking()
                .Where(r => r.DoctorId == doctorId)
                .ToListAsync();
        }

        // Get all records for a specific appointment
        public async Task<IEnumerable<Record>> GetRecordsByAppointmentId(int appointmentId)
        {
            return await _context.Records
                .AsNoTracking()
                .Where(r => r.AppointmentId == appointmentId)
                .ToListAsync();
        }

        // Get all records from the database
        public async Task<IEnumerable<Record>> GetAllRecords()
        {
            return await _context.Records
                .AsNoTracking()
                .ToListAsync();
        }

        // Add a new record to the database
        public async Task Add(Record record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        // Update an existing record
        public async Task Update(Record record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            _context.Records.Update(record);
            await _context.SaveChangesAsync();
        }

        // Delete a record from the database
        public async Task Delete(int id)
        {
            var record = await GetRecordById(id);
            if (record == null)
                throw new KeyNotFoundException($"Record with ID {id} not found.");

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}
