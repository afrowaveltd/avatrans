using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avatrans.Services;
using System;

namespace Avatrans.MarkupExtensions;

public class LocalizeExtension : MarkupExtension
{
    public string Key { get; set; }

    public LocalizeExtension(string key)
    {
        Key = key;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        // Získání služby přes service provider
        var localizationService = serviceProvider.GetService(typeof(ILocalizationService)) as ILocalizationService;
        
        if (localizationService == null)
            return new Binding { Source = Key };

        // Vytvoření binding na indexer služby
        return new Binding
        {
            Source = localizationService,
            Path = $"[{Key}]",
            Mode = BindingMode.OneWay
        };
    }
}