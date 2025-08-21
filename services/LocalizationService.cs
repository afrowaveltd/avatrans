using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using avatrans.Models;


namespace Avatrans.Services
{
    public class LocalizationService : ILocalizationService
    {
        public event EventHandler? LanguageChanged;
    
    private Dictionary<string, string> _currentDictionary = new();
    private readonly string _localesPath;
    private CultureInfo? _currentCulture;

    public CultureInfo CurrentCulture
    {
        get => _currentCulture ?? new CultureInfo("en");
        set
        {
            if (_currentCulture != value)
            {
                _currentCulture = value;
                LoadDictionary(value.TwoLetterISOLanguageName);
                LanguageChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public LocalizationService()
    {
        _localesPath = Path.Combine(AppContext.BaseDirectory, "Locales");
        
        // Načtení výchozího jazyka z nastavení nebo systémového jazyka
        var settings = LoadSettings();
        var defaultLanguage = settings.UILanguage ?? GetSystemLanguage();
        CurrentCulture = new CultureInfo(defaultLanguage);
    }

    public string this[string key] => GetString(key);

    public string GetString(string key)
    {
        if (_currentDictionary.TryGetValue(key, out var value))
            return value;
        
        return key; // Fallback na klíč, pokud překlad neexistuje
    }

    public IEnumerable<CultureInfo> GetAvailableCultures()
    {
        var cultures = new List<CultureInfo>();
        
        if (!Directory.Exists(_localesPath))
            return cultures;

        var jsonFiles = Directory.GetFiles(_localesPath, "*.json");
        foreach (var file in jsonFiles)
        {
            var languageCode = Path.GetFileNameWithoutExtension(file);
            if (languageCode.Length == 2) // pouze dvoupísmenné kódy
            {
                try
                {
                    cultures.Add(new CultureInfo(languageCode));
                }
                catch
                {
                    // Ignorovat neplatné jazykové kódy
                }
            }
        }

        return cultures;
    }

    public void SetLanguage(string languageCode)
    {
        CurrentCulture = new CultureInfo(languageCode);
        
        // Uložit do nastavení
        var settings = LoadSettings();
        settings.UILanguage = languageCode;
        SaveSettings(settings);
    }

    private void LoadDictionary(string languageCode)
    {
        var filePath = Path.Combine(_localesPath, $"{languageCode}.json");
        
        if (!File.Exists(filePath))
        {
            // Fallback na angličtinu
            filePath = Path.Combine(_localesPath, "en.json");
            if (!File.Exists(filePath))
            {
                _currentDictionary = new Dictionary<string, string>();
                return;
            }
        }

        try
        {
            var json = File.ReadAllText(filePath);
            _currentDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) 
                                ?? new Dictionary<string, string>();
        }
        catch
        {
            _currentDictionary = new Dictionary<string, string>();
        }
    }

    private string GetSystemLanguage()
    {
        try
        {
            return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        }
        catch
        {
            return "en"; // Fallback na angličtinu
        }
    }

    private AppSettings LoadSettings()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");
        
        if (!File.Exists(settingsPath))
            return new AppSettings();

        try
        {
            var json = File.ReadAllText(settingsPath);
            return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
        }
        catch
        {
            return new AppSettings();
        }
    }

    private void SaveSettings(AppSettings settings)
    {
        try
        {
            var settingsPath = Path.Combine(AppContext.BaseDirectory, "settings.json");
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(settingsPath, json);
        }
        catch
        {
            // Ignorovat chyby zápisu
        }
    }

    }
}