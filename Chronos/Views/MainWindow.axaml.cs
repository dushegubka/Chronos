using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using SukiUI.Dialogs;

namespace Chronos.Views;

public partial class MainWindow : SukiWindow
{
    public MainWindow()
    {
        InitializeComponent();

        SukiDialogHost.Manager = App.Current.Services.GetService<ISukiDialogManager>()!;
    }
}