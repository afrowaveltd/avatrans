using System;
using System.ComponentModel;
using Avatrans.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avatrans.ViewModels;

public class ViewModelBase : ObservableObject, INotifyPropertyChanged
{
    protected readonly ILocalizationService LocalizationService;

    public ViewModelBase(ILocalizationService localizationService)
    {
        LocalizationService = localizationService;
        LocalizationService.LanguageChanged += OnLanguageChanged;
    }

    ~ViewModelBase()
    {
        LocalizationService.LanguageChanged -= OnLanguageChanged;
    }

    protected virtual void OnLanguageChanged(object? sender, EventArgs e)
    {
        // Přeforčovat všechny lokalizované property
        OnPropertyChanged(string.Empty);
    }

    protected string Localize(string key) => LocalizationService.GetString(key);
}
