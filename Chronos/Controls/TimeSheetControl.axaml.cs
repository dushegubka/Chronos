using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Chronos.Controls;

public class TimeSheetControl : TemplatedControl
{
    public TimeSheetControl()
    {
        
    }

    private string _customer;

    public static readonly DirectProperty<TimeSheetControl, string> CustomerProperty = AvaloniaProperty.RegisterDirect<TimeSheetControl, string>(
        nameof(Customer), o => o.Customer, (o, v) => o.Customer = v);

    public string Customer
    {
        get => _customer;
        set => SetAndRaise(CustomerProperty, ref _customer, value);
    }

    public static readonly StyledProperty<string> WorkNameProperty = AvaloniaProperty.Register<TimeSheetControl, string>(
        nameof(WorkName));

    public string WorkName
    {
        get => GetValue(WorkNameProperty);
        set => SetValue(WorkNameProperty, value);
    }

    public static readonly StyledProperty<int> HoursProperty = AvaloniaProperty.Register<TimeSheetControl, int>(
        nameof(Hours));

    public int Hours
    {
        get => GetValue(HoursProperty);
        set => SetValue(HoursProperty, value);
    }

    public static readonly StyledProperty<decimal> PricePerHourProperty = AvaloniaProperty.Register<TimeSheetControl, decimal>(
        nameof(PricePerHour));

    public decimal PricePerHour
    {
        get => GetValue(PricePerHourProperty);
        set => SetValue(PricePerHourProperty, value);
    }

    public static readonly StyledProperty<DateTime> DateProperty = AvaloniaProperty.Register<TimeSheetControl, DateTime>(
        nameof(Date));

    public DateTime Date
    {
        get => GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }
}