using System;
using Chronos.Abstractions;
using Chronos.Core.TimeSheets;
using Chronos.Events;
using Chronos.Views.Modals;
using ReactiveUI;
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

    public void ShowConfirmDeleting(Guid sheetId)
    {
        var modal = _dialogManager.CreateDialog();
        modal.SetTitle("Подтвердите удаление");
        modal.SetContent("Вы действительно хотите удалить запись?");
        modal.SetCanDismissWithBackgroundClick(true);
        
        modal.WithActionButton("Удалить", _ =>
        {
            MessageBus.Current.SendMessage(new TimeSheetDeletingEventArgs(sheetId));
            _dialogManager.DismissDialog();
        } );
        
        modal.WithActionButton("Отмена", _ => { _dialogManager.DismissDialog(); });
        
        modal.TryShow();
    }
}