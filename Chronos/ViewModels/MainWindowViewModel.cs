using Chronos.Core;
using Chronos.Core.TimeSheets;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
    }
    
    public string Greeting { get; } = "Welcome to Avalonia! asd";
}