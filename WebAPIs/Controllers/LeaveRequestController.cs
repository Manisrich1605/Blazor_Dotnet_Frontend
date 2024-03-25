using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using WebAPIs.Data;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly LeaveTypeContext _leavetypeContext;
        public LeaveRequestController(LeaveTypeContext leavetypeContext)
        {
            _leavetypeContext = leavetypeContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveRequest>>> GetLeaveRequests()
        {
            if (_leavetypeContext.LeaveRequests == null)
            {
                return NotFound();
            }
            return await _leavetypeContext.LeaveRequests.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequest>> GetLeaveRequest(int id)
        {
            if (_leavetypeContext.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaverequest = await _leavetypeContext.LeaveRequests.FindAsync(id);
            if (leaverequest == null)
            {
                return NotFound();
            }
            return leaverequest;


        }

        [HttpPost]
        public async Task<ActionResult<LeaveRequest>> PostLeaveRequest(LeaveRequest leaverequest)
        {
            _leavetypeContext.LeaveRequests.Add(leaverequest);
            await _leavetypeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLeaveRequest), new { id = leaverequest.Id }, leaverequest);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutLeaveRequest(int id, LeaveRequest leaverequest)
        {
            if (id != leaverequest.Id)
            {
                return BadRequest();
            }
            _leavetypeContext.Entry(leaverequest).State = EntityState.Modified;
            try
            {
                await _leavetypeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLeaveRequest(int id)
        {
            if (_leavetypeContext.LeaveRequests == null)
            {
                return NotFound();
            }
            var leaverequest = await _leavetypeContext.LeaveRequests.FindAsync(id);
            if (leaverequest == null)
            {
                return NotFound();

            }
            _leavetypeContext.LeaveRequests.Remove(leaverequest);
            await _leavetypeContext.SaveChangesAsync();

            return Ok();
        }
    }


}