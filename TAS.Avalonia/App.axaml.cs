using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using TAS.Avalonia.Services;
using TAS.Avalonia.ViewModels;
using TAS.Avalonia.Views;

namespace TAS.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        // TinyIoCContainer.Current.Register<ICelesteService, CelesteService>().AsSingleton();
        AvaloniaLocator.CurrentMutable.Bind<IDialogService>().ToSingleton<DialogService>();
        AvaloniaLocator.CurrentMutable.Bind<ICelesteService>().ToSingleton<CelesteService>();
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
