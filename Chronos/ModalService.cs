using Chronos.Abstractions;
using Chronos.Core.TimeSheets;
using Chronos.Views.Modals;
using SukiUI.Dialogs;

namespace Chronos;

public class ModalService : IModalService
{
    private readonly ISukiDialogManager _dialogManager;

    public ModalService(ISukiDialogManager dialogManager)
    {
        _dialogManager = dialogManager;
    }
    
    public void ShowCreateSheetModal()
    {
        var modal = _dialogManager.CreateDialog();
        modal.SetTitle("Новая запись");
        modal.SetContent(new CreateSheetModalView());
        modal.TryShow();
    }

    public void ShowUpdateSheetModal(TimeSheet timeSheet)
    {
        var modal = _dialogManager.CreateDialog();
        modal.SetTitle("Редактирование записи");
        modal.SetContent(new UpdateSheetModalView(timeSheet));
        modal.TryShow();
    }
}