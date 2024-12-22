using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using Chronos.Controls;
using Chronos.Core.TimeSheets;
using Chronos.Events;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using SukiUI.Dialogs;
using ValidationContext = ReactiveUI.Validation.Contexts.ValidationContext;

namespace Chronos.ViewModels.Modals;

public class CreateSheetModalViewModel : ViewModelBase, IValidatableViewModel
{
    private string _customer;
    private string _workName;
    private DateTimeOffset _date = DateTimeOffset.UtcNow;
    private int _hours = 1;
    private decimal _pricePerHour;


    public CreateSheetModalViewModel()
    {
        var dialog = App.Current.Services.GetService<ISukiDialogManager>();
        CreateSheetCommand = ReactiveCommand.Create(() =>
        {
            CreateSheet();
            dialog?.DismissDialog();
        });
    }
    
    [Required]
    [MaxLength(100)]
    public string Customer
    {
        get => _customer;
        set => this.RaiseAndSetIfChanged(ref _customer, value); 
    }
    
    [Required]
    [MaxLength(200)]
    public string WorkName
    {
        get => _workName;
        set => this.RaiseAndSetIfChanged(ref _workName, value); 
    }
    
    public DateTimeOffset Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value); 
    }
    
    public int Hours
    {
        get => _hours;
        set => this.RaiseAndSetIfChanged(ref _hours, value); 
    }
    
    [Required]
    public decimal PricePerHour
    {
        get => _pricePerHour;
        set => this.RaiseAndSetIfChanged(ref _pricePerHour, value); 
    }
    
    public ReactiveCommand<Unit, Unit> CreateSheetCommand { get; set; }

    
    private async void CreateSheet()
    {
        var dialog = App.Current.Services.GetService<ISukiDialogManager>();
        var timeSheet = new TimeSheetCreateDto()
        {
            Customer = Customer,
            WorkName = WorkName,
            Date = Date,
            Hours = Hours,
            PricePerHour = PricePerHour,
        };

        var repository = App.Current.Services.GetService<ITimeSheetRepository>();

        var createResult = await repository?.CreateAsync(timeSheet)!;
        
        MessageBus.Current.SendMessage(new TimeSheetCreatedEventArgs(createResult));
        
        dialog?.DismissDialog();
    }

    public IValidationContext ValidationContext { get; } = new ValidationContext();
}