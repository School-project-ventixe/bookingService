using booking.Data.Entities;
using booking.Extensions;
using booking.Models;
using booking.Repositories;

namespace booking.Services;

public class BookingService(
    IBookingRepository bookingRepository,
    IHttpClientFactory httpClientFactory) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("EventService");

    public async Task<IEnumerable<BookingWithEvent>> GetAllBookingsOnUserAsync(string userEmail)
    {
        var bookings = await _bookingRepository.GetAllAsync(userEmail);
        if (bookings == null || !bookings.Any())
            return [];

        var result = new List<BookingWithEvent>();

        foreach (var booking in bookings)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/events/{booking.EventId}");
                if (!response.IsSuccessStatusCode)
                    continue;

                var eventDto = await response.Content.ReadFromJsonAsync<EventDto>();
                if (eventDto == null)
                    continue;

                var mapped = booking.MapTo<BookingWithEvent>();
                mapped.Event = eventDto;
                result.Add(mapped);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Fel vid anrop mot EventService: {ex.Message}");
            }
        }

        return result;
    }

    public async Task<BookingEntity> CreatebookingAsync(BookingRegDto dto)
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