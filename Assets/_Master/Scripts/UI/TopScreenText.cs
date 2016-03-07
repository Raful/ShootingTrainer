using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopScreenText : MonoBehaviour 
{
    Text m_textComponent;
    TextFlash m_textFlash;
    LocalizedTexts.Text m_currentText;

#if UNITY_ANDROID || UNITY_IOS || UNITY_WINRT
    const LocalizedTexts.Text PRESS_TO_START = LocalizedTexts.Text.TouchToStart;
    const LocalizedTexts.Text PRESS_TO_SHOOT = LocalizedTexts.Text.TouchToShoot;
#else
    const LocalizedTexts.Text PRESS_TO_START = LocalizedTexts.Text.ClickToStart;
    const LocalizedTexts.Text PRESS_TO_SHOOT = LocalizedTexts.Text.ClickToShoot;
#endif

    // Use this for initialization
	void Awake () 
    {
        m_textComponent = GetComponent<Text>();
        m_textFlash = GetComponent<TextFlash>();
	}

    void Start()
    {
        SetText(PRESS_TO_START);

        LocalizedTexts.OnLanguageChanged += OnLanguageChanged;
    }

    void OnEnable()
    {
        MoveRing.Started += HandleStarted;
        MoveRing.NewScore += HandleShoot;
        InputListener.ResetGame += Reset;
    }
    void OnDisable()
    {
        MoveRing.Started -= HandleStarted;
        MoveRing.NewScore -= HandleShoot;
        InputListener.ResetGame += Reset;
    }
	
	void HandleStarted()
    {
        SetText(PRESS_TO_SHOOT);
    }

    void HandleShoot(float score)
    {
        Reset();
    }

    void Reset()
    {
        SetText(PRESS_TO_START);
    }

    void SetText(LocalizedTexts.Text text, bool flash = true)
    {
        m_textComponent.text = LocalizedTexts.GetText(text);

        if (flash)
        {
            m_textFlash.FlashText(Color.black);
        }

        m_currentText = text;
    }

    void OnLanguageChanged()
    {
        SetText(m_currentText, false);
    }
}
