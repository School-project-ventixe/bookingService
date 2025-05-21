namespace booking.Data.Entities;

public class BookingEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BookingEmail { get; set; } = null!;
    public string EventId { get; set; } = null!;
}
