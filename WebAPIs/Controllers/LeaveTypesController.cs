using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;
using Microsoft.EntityFrameworkCore;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly LeaveTypeContext _leaveTypeContext;
        public LeaveTypesController(LeaveTypeContext leaveTypeContext)
        {
            _leaveTypeContext = leaveTypeContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveType>>> GetLeaveTypes()
        {
            try
            {
                var leaveTypes = await _leaveTypeContext.LeaveTypess.ToListAsync();
                if (leaveTypes == null || leaveTypes.Count == 0)
                    return NotFound("No leave types found.");
                return Ok(leaveTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching leave types: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(int id)
        {
            try
            {
                var leaveType = await _leaveTypeContext.LeaveTypess.FindAsync(id);
                if (leaveType == null)
                    return NotFound($"Leave type with ID {id} not found.");
                return Ok(leaveType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching leave type: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostLeaveType(LeaveType leaveType)
        {
            try
            {
                _leaveTypeContext.LeaveTypess.Add(leaveType);
                await _leaveTypeContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLeaveType), new { id = leaveType.Id }, leaveType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating leave type: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutLeaveType(int id, LeaveType leaveType)
        {
            try
            {
                if (id != leaveType.Id)
                    return BadRequest("ID in the request body does not match the ID in the URL.");
                _leaveTypeContext.Entry(leaveType).State = EntityState.Modified;
                await _leaveTypeContext.SaveChangesAsync();
                return Ok("Leave type updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(id))
                    return NotFound($"Leave type with ID {id} not found.");
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating leave type: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLeaveType(int id)
        {
            try
            {
                var leaveType = await _leaveTypeContext.LeaveTypess.FindAsync(id);
                if (leaveType == null)
                    return NotFound($"Leave type with ID {id} not found.");
                _leaveTypeContext.LeaveTypess.Remove(leaveType);
                await _leaveTypeContext.SaveChangesAsync();
                return Ok("Leave type deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting leave type: {ex.Message}");
            }
        }
        private bool LeaveTypeExists(int id)
        {
            return _leaveTypeContext.LeaveTypess.Any(e => e.Id == id);
        }
    }
}
