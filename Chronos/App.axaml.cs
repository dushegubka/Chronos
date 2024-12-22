using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Chronos.Abstractions;
using Chronos.Core;
using Chronos.Core.TimeSheets;
using Chronos.ViewModels;
using Chronos.ViewModels.Modals;
using Chronos.Views;
using Chronos.Views.Modals;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace Chronos;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += DesktopOnExit;
            
            Services = BuildServices();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public new static App Current => (App)Application.Current!;

    public IServiceProvider Services { get; set; }

    public IServiceProvider BuildServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ISukiDialogManager, SukiDialogManager>();
        services.AddSingleton<ISukiToastManager, SukiToastManager>();
        services.AddSingleton<IModalService, ModalService>();
        
        services.AddSingleton<ApplicationDbContext>();
        services.AddSingleton<ITimeSheetRepository, TimeSheetRepository>();
        
        return services.BuildServiceProvider();
    }
    
    private void DesktopOnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        var dbContext = Services.GetService<ApplicationDbContext>()!;
        dbContext.Dispose();
    }
}