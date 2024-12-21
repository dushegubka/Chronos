using Chronos.Core.TimeSheets;
using LiteDB.Async;

namespace Chronos.Core;

public class ApplicationDbContext : IDisposable
{
    private const string DbName = "chronos.db";
    private const string TimeSheetCollectionName = "timeSheets";
    
    private readonly ILiteDatabaseAsync _database;
    
    public ApplicationDbContext()
    {
        _database = new LiteDatabaseAsync(DbName);
    }
    
    public ILiteCollectionAsync<TimeSheet> TimeSheets => _database.GetCollection<TimeSheet>(TimeSheetCollectionName);

    public void Dispose()
    {
        _database.Dispose();
    }
}