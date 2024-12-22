using System;

namespace Chronos.Events;

public class TimeSheetDeletingEventArgs : EventArgs
{
    public TimeSheetDeletingEventArgs(Guid sheetId)
    {
        SheetId = sheetId;
    }
    
    public Guid SheetId { get; }
}