using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using TanJingTing_Project.Data;
using TanJingTing_Project.Models;

namespace TanJingTing_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly MemberDbContext _context;
        public BookingController(MemberDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Booking);
        }

        [HttpGet("{Bookingid}")]
        public IActionResult GetById(int? id)
        {
            var booking = _context.Booking.FirstOrDefault(b => b.BookingID == id);
            if (booking == null)
                return Problem(detail: "Booking with id " + id + " is not found.", statusCode: 404);

            return Ok(booking);
        }

        [HttpGet("{Bookingstatus}")]
        public IActionResult GetByStatus(string? Bookingstatus = "All")
        {
            switch (Bookingstatus.ToLower())
            {
                case "all":
                    return Ok(_context.Booking);
                case "Pending":
                    return Ok(_context.Booking.Where(b => b.BookingStatus.ToLower() == "Pending"));
                case "Approved":
                    return Ok(_context.Booking.Where(b => b.BookingStatus.ToLower() == "Approved"));
                case "Rejected":
                    return Ok(_context.Booking.Where(b => b.BookingStatus.ToLower() == "Rejected"));
                default:
                    return Problem(detail: "Booking with ststus " + Bookingstatus + " is not found.", statusCode: 404);
            }
        }
        [HttpPost]
        public IActionResult Post(Booking booking)
        {
            _context.Booking.Add(booking);
            _context.SaveChanges();

            return CreatedAtAction("GetAll", new { id = booking.BookingID }, booking);
        }
        [HttpPut]
        public IActionResult Put(int? id, Booking booking)
        {
            var entity = _context.Booking.FirstOrDefault(b => b.BookingID == id);
            if (entity == null)
                return Problem(detail: "Booking with id " + id + " is not found.", statusCode: 404);

            entity.FacilityDescription = booking.FacilityDescription;
            entity.BookingDateFrom = booking.BookingDateFrom;
            entity.BookingDateTo = booking.BookingDateTo;
            entity.BookedBy = booking.BookedBy;
            entity.BookingStatus = booking.BookingStatus;

            _context.SaveChanges();

            return Ok(entity);
        }
        [HttpDelete]
        public IActionResult Delete(int? id, Booking booking)
        {
            var entity = _context.Booking.FirstOrDefault(m => m.BookingID == id);
            if (entity == null)
                return Problem(detail: "Booking with id " + id + " is not found.", statusCode: 404);

            _context.Booking.Remove(entity);
            _context.SaveChanges();

            return Ok(entity);
        }
    }


}
