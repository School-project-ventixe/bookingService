namespace booking.Models;

public class EventDto
{
    public string EventName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? EventDescription { get; set; }
    public string? Location { get; set; }
    public DateTime? StartDate { get; set; }
    public decimal? Price { get; set; }
}