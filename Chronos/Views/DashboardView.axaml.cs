using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Chronos.ViewModels;

namespace Chronos.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();

        DataContext = new DashboardViewModel();
    }
}