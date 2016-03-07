using UnityEngine;
using UnityEngine.UI;

public class PrintScore : MonoBehaviour {

    TextFlash m_textFlash;

    void Awake()
    {
        m_textFlash = GetComponent<TextFlash>();
    }

    void Start()
    {
        PrintNewScore(0, false);
    }

	// Use this for initialization
	void OnEnable () 
    {
        MoveRing.NewScore += PrintNewScore;
        InputListener.ResetGame += ResetScore;
        LocalizedTexts.OnLanguageChanged += OnLanguageChanged;
	}

    void OnDisable()
    {
        MoveRing.NewScore -= PrintNewScore;
        InputListener.ResetGame -= ResetScore;
        LocalizedTexts.OnLanguageChanged -= OnLanguageChanged;
    }
	
	void PrintNewScore(float score)
    {
        PrintNewScore(score, true);
    }

    void PrintNewScore(float score, bool flash)
    {
        GetComponent<Text>().text = LocalizedTexts.GetText(LocalizedTexts.Text.Score) + ": " + score.ToString("F1");

        if (flash)
        {
            m_textFlash.FlashText(Color.green);
        }
    }

    void ResetScore()
    {
        PrintNewScore(0);
        m_textFlash.FlashText(Color.red);
    }

    void OnLanguageChanged()
    {
        PrintNewScore(GameData.currentScore, false);
    }
}
