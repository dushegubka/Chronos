using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Avalonia.Threading;
using Chronos.Abstractions;
using Chronos.Core.TimeSheets;
using Chronos.Events;
using Chronos.ViewModels.Modals;
using Chronos.Views.Modals;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SukiUI.Controls;
using SukiUI.Dialogs;
using Tmds.DBus.Protocol;
using CreateSheetModalViewModel = Chronos.ViewModels.Modals.CreateSheetModalViewModel;

namespace Chronos.ViewModels;

public class DashboardViewModel : ViewModelBase
{
    private TimeSheet _selectedSheet;
    private bool _isButtonPanelAvailable;

    public DashboardViewModel()
    {
        RegisterMessages();
        
        RegisterCommands();
        
        RxApp.MainThreadScheduler.Schedule(LoadTimeSheets);
    }

    public ObservableCollection<TimeSheet> TimeSheets { get; set; } = new();
    
    public ReactiveCommand<Unit, Unit> ShowCreateDialogCommand { get; set; }
    
    public ReactiveCommand<Unit, Unit> ShowUpdateDialogCommand { get; set; }
    
    public ReactiveCommand<Unit, Unit> ShowDeleteDialogCommand { get; set; }

    private async void LoadTimeSheets()
    {
        var repository = App.Current.Services.GetService<ITimeSheetRepository>();

        var timeSheets = await repository?.GetAllAsync()!;

        Dispatcher.UIThread.Post(() => TimeSheets.AddRange(timeSheets));     
    }

    public TimeSheet SelectedSheet
    {
        get => _selectedSheet;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedSheet, value);
            IsButtonPanelAvailable = true;
        }
    }
    
    public bool IsButtonPanelAvailable
    {
        get => SelectedSheet is not null;
        set => this.RaiseAndSetIfChanged(ref _isButtonPanelAvailable, value);
    }

    private void RegisterMessages()
    {
        MessageBus.Current.Listen<TimeSheetCreatedEventArgs>()
            .Where(x => x.TimeSheet is not null)
            .Subscribe(x => TimeSheets.Add(x.TimeSheet));
        
        MessageBus.Current.Listen<TimeSheetUpdatedEventArgs>()
            .Where(x => x.TimeSheet is not null)
            .Subscribe(x =>
            {
                var entry = TimeSheets.FirstOrDefault(y => y.Id == x.TimeSheet.Id);
                if (entry is null) return;
                
                TimeSheets.Remove(entry);
                TimeSheets.Add(x.TimeSheet);
            });

        MessageBus.Current.Listen<TimeSheetDeletingEventArgs>()
            .Where(x => x.SheetId != Guid.Empty)
            .Subscribe(async void (x) =>
            {
                var repository = App.Current.Services.GetService<ITimeSheetRepository>();
                var isDeleted = await repository?.DeleteAsync(x.SheetId);

                if (isDeleted)
                {
                    TimeSheets.Remove(TimeSheets.FirstOrDefault(y => y.Id == x.SheetId)!);
                }
            });
    }

    private void RegisterCommands()
    {
        ShowCreateDialogCommand = ReactiveCommand.Create(() =>
        {
            var modalService = App.Current.Services.GetService<IModalService>();
            modalService?.ShowCreateSheetModal();
        });
        
        ShowUpdateDialogCommand = ReactiveCommand.Create(() =>
        {
            var modalService = App.Current.Services.GetService<IModalService>();
            modalService?.ShowUpdateSheetModal(SelectedSheet);
        });

        ShowDeleteDialogCommand = ReactiveCommand.Create(() =>
        {
            var modelService = App.Current.Services.GetService<IModalService>();
            modelService?.ShowConfirmDeleting(SelectedSheet.Id);
        });
    }
}