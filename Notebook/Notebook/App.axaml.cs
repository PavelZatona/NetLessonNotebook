using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Notebook.Services.Abstract;
using Notebook.Services.Implementations;
using Notebook.ViewModels;
using Notebook.Views;
using System;

namespace Notebook;

public partial class App : Application
{
    /// <summary>
    /// Инжектор зависимостей. Это штука, которая на место интерфейсов подсовывает конкретные реализации
    /// в соответствии с настройками
    /// </summary>
    public static ServiceProvider Di { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Вызов настройки инжектора зависимостей
        Di = ConfigureServices()
            .BuildServiceProvider();

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Запрашиваем сервисы, потребные для MainViewModel
        // Мы спросили у инжектора: дай нам конкретную реализацию интерфейса
        // INotesStorageService
        // Инжектор отдаёт NotesMemoryStorageService (т.к. мы так его настроили)
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        var notesStorageService = Di.GetService<INotesStorageService>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(notesStorageService) // Передаём хранилище заметок во вью модель
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(notesStorageService)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    // Настройка инжектора зависимостей
    private IServiceCollection ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();

        // Синглтоны. Синглтон - это класс, имеющий всего один объект на всю программу
        #region Синглтоны

        // Везде, где программа будет пытаться иметь дело с INotesStorageService она на самом деле
        // будет получать NotesMemoryStorageService
        services.AddSingleton<INotesStorageService, NotesMemoryStorageService>();

        #endregion

        return services;
    }
}
