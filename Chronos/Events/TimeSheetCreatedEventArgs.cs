using System;
using Chronos.Core.TimeSheets;

namespace Chronos.Events;

public class TimeSheetCreatedEventArgs : EventArgs
{
    public TimeSheetCreatedEventArgs(TimeSheet timeSheet)
    {
        TimeSheet = timeSheet;
    }
    public TimeSheet TimeSheet { get; init; }
}