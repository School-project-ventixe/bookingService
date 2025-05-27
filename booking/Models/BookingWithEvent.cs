namespace booking.Models;

public class BookingWithEvent
{
    public string Id { get; set; } = null!;
    public string BookingEmail { get; set; } = null!;
    public string EventId { get; set; } = null!;
    public EventDto Event { get; set; } = null!;
}