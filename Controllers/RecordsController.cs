using Microsoft.AspNetCore.Mvc;
using PatientRecordMicroService.Models;
using PatientRecordMicroService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;

        // Constructor to inject the service layer
        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
        }

        // GET: api/Records/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            try
            {
                var record = await _recordService.GetRecordByIdAsync(id);
                return Ok(record); // Return the record if found
            }
            catch (KeyNotFoundException ex)
            {
                // Return NotFound with the exception message if record does not exist
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/Records/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetRecordsByPatientId(int patientId)
        {
            var records = await _recordService.GetRecordsByPatientIdAsync(patientId);
            if (records == null || records.ToList().Count == 0)
            {
                return NotFound(new { message = "No records found for the specified patient." });
            }
            return Ok(records);
        }

        // GET: api/Records/doctor/{doctorId}
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetRecordsByDoctorId(int doctorId)
        {
            var records = await _recordService.GetRecordsByDoctorIdAsync(doctorId);
            if (records == null || records.ToList().Count == 0)
            {
                return NotFound(new { message = "No records found for the specified doctor." });
            }
            return Ok(records);
        }

        // GET: api/Records/appointment/{appointmentId}
        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetRecordsByAppointmentId(int appointmentId)
        {
            var records = await _recordService.GetRecordsByAppointmentIdAsync(appointmentId);
            if (records == null || records.ToList().Count == 0)
            {
                return NotFound(new { message = "No records found for the specified appointment." });
            }
            return Ok(records);
        }

        // GET: api/Records
        [HttpGet]
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _recordService.GetAllRecordsAsync();
            return Ok(records);
        }

        // POST: api/Records
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody] Record record)
        {
            if (record == null)
            {
                return BadRequest("Record cannot be null.");
            }

            try
            {
                await _recordService.AddRecordAsync(record);
                return CreatedAtAction(nameof(GetRecordById), new { id = record.Id }, record);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // Return a bad request if there's a validation issue
            }
        }

        // PUT: api/Records/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, [FromBody] Record record)
        {
            if (id != record.Id)
            {
                return BadRequest("The ID in the URL and body must match.");
            }

            try
            {
                await _recordService.UpdateRecordAsync(record);
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Record not found
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // Return validation error
            }
        }

        // DELETE: api/Records/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            try
            {
                await _recordService.DeleteRecordAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Return NotFound if record doesn't exist
            }
        }
    }
}
