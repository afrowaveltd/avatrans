using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Avatrans.Services
{
    public interface ILocalizationService
    {
        event EventHandler LanguageChanged;
        CultureInfo CurrentCulture { get; set; }
        string this[string key] { get; }
        string GetString(string key);
        IEnumerable<CultureInfo> GetAvailableCultures();
        void SetLanguage(string languageCode);
    }
}