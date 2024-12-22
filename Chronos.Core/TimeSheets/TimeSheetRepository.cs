namespace Chronos.Core.TimeSheets;

public class TimeSheetRepository : ITimeSheetRepository
{
    private readonly ApplicationDbContext _context;

    public TimeSheetRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
   public async Task<TimeSheet> GetByIdAsync(Guid id)
    {
        var sheetEntry = await _context.TimeSheets.FindOneAsync(x => x.Id == id);

        return sheetEntry;
    }

    public async Task<IEnumerable<TimeSheet>> GetAllAsync()
    {
        var result = await  _context.TimeSheets.FindAllAsync();
        
        return result;
    }

    public async Task<TimeSheet> CreateAsync(TimeSheetCreateDto createDto)
    {
        var entry = new TimeSheet()
        {
            Id = Guid.NewGuid(),
            Customer = createDto.Customer,
            WorkName = createDto.WorkName,
            Date = createDto.Date,
            Hours = createDto.Hours,
            PricePerHour = createDto.PricePerHour,
        };

        await _context.TimeSheets.InsertAsync(entry);

        return entry;
    }

    public async Task<TimeSheet> UpdateAsync(Guid id, TimeSheetCreateDto updatedEntry)
    {
        var entry = await _context.TimeSheets.FindOneAsync(x => x.Id == id);

        if (entry is null)
        {
            return null;
        }

        entry.WorkName = updatedEntry.WorkName;
        entry.Customer = updatedEntry.Customer;
        entry.Hours = updatedEntry.Hours;
        entry.PricePerHour = updatedEntry.PricePerHour;
        entry.Date = updatedEntry.Date;

        await _context.TimeSheets.UpdateAsync(id, entry);

        return entry;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entryExist = await _context.TimeSheets.ExistsAsync(x => x.Id == id);

        if (!entryExist)
        {
            return false;
        }

        await _context.TimeSheets.DeleteAsync(id);

        return true;
    }
}