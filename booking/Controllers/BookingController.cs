using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllBookingsOnUser()
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userEmail))
            return Unauthorized();

        var bookings = await _bookingService.GetAllBookingsOnUserAsync(userEmail);

        return (bookings != null)
            ? Ok(bookings)
            : NotFound();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById(string id)
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userEmail))
            return Unauthorized();

        var bookingModel = await _bookingService.GetbookingAsync(id);

        return (bookingModel != null)
            ? Ok(bookingModel)
            : NotFound();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Createbooking(BookingRegDto bookingDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdbooking = await _bookingService.CreatebookingAsync(bookingDto);

        return (createdbooking != null)
            ? Ok(createdbooking)
            : Unauthorized("Failed to create booking");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletebooking(string id)
    {
        bool isDeleted = await _bookingService.Deletebooking(id);
        return (isDeleted)
            ? Ok()
            : BadRequest();
    }
}
