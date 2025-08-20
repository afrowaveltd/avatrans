using avatrans.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        // Register your common services here
        collection.AddTransient<MainWindowViewModel>();
    }
}  