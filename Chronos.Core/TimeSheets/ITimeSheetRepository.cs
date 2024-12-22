namespace Chronos.Core.TimeSheets;

public interface ITimeSheetRepository
{
    Task<TimeSheet> GetByIdAsync(Guid id);

    Task<IEnumerable<TimeSheet>> GetAllAsync();

    Task<TimeSheet> CreateAsync(TimeSheetCreateDto timeSheet);

    Task<TimeSheet> UpdateAsync(Guid id, TimeSheetCreateDto timeSheet);

    Task<bool> DeleteAsync(Guid id);
}