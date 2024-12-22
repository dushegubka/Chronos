using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Chronos.Core.TimeSheets;
using Chronos.ViewModels.Modals;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Dialogs;

namespace Chronos.Views.Modals;

public partial class UpdateSheetModalView : UserControl
{
    public UpdateSheetModalView(TimeSheet timeSheet)
    {
        InitializeComponent();

        DataContext = new UpdateSheetModalViewModel(timeSheet);
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var dialog = App.Current.Services.GetService<ISukiDialogManager>();
        
        dialog?.DismissDialog();
    }
}