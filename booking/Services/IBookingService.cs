using booking.Data.Entities;
using booking.Models;

namespace booking.Services
{
    public interface IBookingService
    {
        Task<BookingEntity> CreatebookingAsync(BookingDto dto);
        Task<bool> Deletebooking(string id);
        Task<IEnumerable<BookingModel>> GetAllbookings(string userEmail);
        Task<BookingModel> GetbookingAsync(string id);
    }
}