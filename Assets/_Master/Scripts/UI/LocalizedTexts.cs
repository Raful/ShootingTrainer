using System.Collections.Generic;
using System;
using UnityEngine;

public static class LocalizedTexts
{
    private static Dictionary<Text, string> m_localizedTexts;

    public static Language CurrentLanguage { get; private set; }

    public static event Action OnLanguageChanged;

    public enum Text
    {
        ClickToStart,
        ClickToShoot,
        Goal,
        TouchToStart,
        TouchToShoot,
        ResetScore,
        Score,
        Total,
        Settings,
        Language,
        LanguageName //E.g. "English" or "Svenska"
    }

    public enum Language
    {
        English,
        Swedish
    }

    static LocalizedTexts()
    {
        InitDictionary();
        SetLanguage(GetSystemLanguage());
    }

    private static void InitDictionary()
    {
        m_localizedTexts = new Dictionary<Text, string>();

        Array texts = Enum.GetValues(typeof(Text));
        foreach (Text text in texts)
        {
            m_localizedTexts.Add(text, "");
        }
    }

    public static void SetLanguage(Language language)
    {
        if (language == Language.English)
        {
            m_localizedTexts[Text.ClickToStart] = "Click to start";
            m_localizedTexts[Text.ClickToShoot] = "Click to shoot";
            m_localizedTexts[Text.Goal] = "Goal";
            m_localizedTexts[Text.ResetScore] = "Reset score";
            m_localizedTexts[Text.Score] = "Score";
            m_localizedTexts[Text.Total] = "Total";
            m_localizedTexts[Text.TouchToStart] = "Touch the screen to start";
            m_localizedTexts[Text.TouchToShoot] = "Touch the screen to shoot";
            m_localizedTexts[Text.Settings] = "Settings";
            m_localizedTexts[Text.Language] = "Language";
            m_localizedTexts[Text.LanguageName] = "English";
        }
        else if (language == Language.Swedish)
        {
            m_localizedTexts[Text.ClickToStart] = "Klicka för att starta";
            m_localizedTexts[Text.ClickToShoot] = "Klicka för att skjuta";
            m_localizedTexts[Text.Goal] = "Mål";
            m_localizedTexts[Text.ResetScore] = "Nollställ resultat";
            m_localizedTexts[Text.Score] = "Poäng";
            m_localizedTexts[Text.Total] = "Totalt";
            m_localizedTexts[Text.TouchToStart] = "Tryck på skärmen för att starta";
            m_localizedTexts[Text.TouchToShoot] = "Tryck på skärmen för att skjuta";
            m_localizedTexts[Text.Settings] = "Inställningar";
            m_localizedTexts[Text.Language] = "Språk";
            m_localizedTexts[Text.LanguageName] = "Svenska";
        }

        CurrentLanguage = language;

        if (OnLanguageChanged != null)
            OnLanguageChanged();
    }

	public static string GetText(Text text)
    {
        return m_localizedTexts[text];
    }

    private static Language GetSystemLanguage()
    {
        switch (Application.systemLanguage)
        {
            default:
                //Default to english, if the language is not supported
                return Language.English;
            case SystemLanguage.English: //English
                return Language.English;
            case SystemLanguage.Swedish: //Swedish
                return Language.Swedish;
        }
    }
}
