using System;
using Chronos.Core.TimeSheets;

namespace Chronos.Abstractions;

public interface IModalService
{
    void ShowCreateSheetModal();
    
    void ShowUpdateSheetModal(TimeSheet timeSheet);

    void ShowConfirmDeleting(Guid sheetId);
}