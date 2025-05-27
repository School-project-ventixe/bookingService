using booking.Data.Entities;
using booking.Models;

namespace booking.Services
{
    public interface IBookingService
    {
        Task<BookingEntity> CreatebookingAsync(BookingRegDto dto);
        Task<bool> Deletebooking(string id);
        Task<IEnumerable<BookingWithEvent>> GetAllBookingsOnUserAsync(string userEmail);
        Task<BookingModel> GetbookingAsync(string id);
    }
}