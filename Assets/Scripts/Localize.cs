using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public class Localize : MonoBehaviour
{
    
    public static void SetLanguage(Language language)
    {
        switch (language)
        {
            case Language.English:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en-US");
                break;
            case Language.Japanese:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ja-JP");
                break;
            case Language.Korean:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ko-KR");
                break;
        }
        //ES3.Save("Language", language);
    }

    public static string GetLocalizedString(string tableName, string keyName)
    {
        LocalizedString localizeString = new LocalizedString() { TableReference = tableName, TableEntryReference = keyName };
        var stringOperation = localizeString.GetLocalizedStringAsync();

        if (stringOperation.IsDone && stringOperation.Status == AsyncOperationStatus.Succeeded)
        {
            return stringOperation.Result;
        }
        else
        {
            return null;
        }
    }
}
