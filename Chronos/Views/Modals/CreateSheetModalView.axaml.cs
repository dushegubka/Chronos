using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Chronos.ViewModels.Modals;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using SukiUI.Dialogs;

namespace Chronos.Views.Modals;

public partial class CreateSheetModalView : UserControl
{
    public CreateSheetModalView()
    {
        InitializeComponent();

        DataContext = new CreateSheetModalViewModel();
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var dialog = App.Current.Services.GetService<ISukiDialogManager>();
        
        dialog?.DismissDialog();
    }
}