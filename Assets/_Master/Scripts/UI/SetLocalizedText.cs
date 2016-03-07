using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetLocalizedText : MonoBehaviour 
{
    [SerializeField]
    LocalizedTexts.Text m_text;

    void Start()
    {
        UpdateLanguage();
    }

    void OnEnable()
    {
        LocalizedTexts.OnLanguageChanged += UpdateLanguage;
    }

    void OnDisable()
    {
        LocalizedTexts.OnLanguageChanged -= UpdateLanguage;
    }

    private void UpdateLanguage()
    {
        GetComponent<Text>().text = LocalizedTexts.GetText(m_text);
    }
}
