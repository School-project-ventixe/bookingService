using booking.Data.Entities;
using booking.Extensions;
using booking.Models;
using booking.Repositories;

namespace booking.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<BookingEntity> CreatebookingAsync(BookingDto dto)
    {
        if (dto == null)
            return null!;

        await _bookingRepository.BeginTransactionAsync();
        var entity = dto.MapTo<BookingEntity>();

        await _bookingRepository.AddAsync(entity);
        var savedResult = await _bookingRepository.SaveAsync();
        if (!savedResult)
        {
            await _bookingRepository.RollbackTransactionAsync();
            return null!;
        }

        await _bookingRepository.CommitTransactionAsync();
        return entity;
    }

    public async Task<IEnumerable<BookingModel>> GetAllbookings(string userEmail)
    {
        var bookings = await _bookingRepository.GetAllAsync(userEmail);

        var model = bookings.Select(x => x.MapTo<BookingModel>());
        return model;
    }

    public async Task<BookingModel> GetbookingAsync(string id)
    {
        var bookingEntity = await _bookingRepository.GetAsync(x => x.Id == id);
        if (bookingEntity == null)
            return null!;

        var model = bookingEntity.MapTo<BookingModel>();
        return model;
    }

    public async Task<bool> Deletebooking(string id)
    {
        var entity = await _bookingRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        await _bookingRepository.BeginTransactionAsync();
        await _bookingRepository.DeleteAsync(x => x.Id == id);

        var res = await _bookingRepository.SaveAsync();
        if (!res)
        {
            await _bookingRepository.RollbackTransactionAsync();
            return false;
        }
        await _bookingRepository.CommitTransactionAsync();
        return true;
    }
}