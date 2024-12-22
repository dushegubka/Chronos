using System;
using Chronos.Core.TimeSheets;

namespace Chronos.Events;

public class TimeSheetUpdatedEventArgs : EventArgs
{
    public TimeSheetUpdatedEventArgs(TimeSheet timeSheet)
    {
        TimeSheet = timeSheet;
    }
    
    public TimeSheet TimeSheet { get; }
}