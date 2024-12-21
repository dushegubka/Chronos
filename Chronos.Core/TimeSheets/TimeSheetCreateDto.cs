namespace Chronos.Core.TimeSheets;

public class TimeSheetCreateDto
{
    public required string Customer { get; set; }

    public DateTimeOffset Date { get; set; }

    public required string WorkName { get; set; }

    public required decimal PricePerHour { get; set; }
    
    public int Hours { get; set; }
    
    public decimal Total => PricePerHour * Hours;
}