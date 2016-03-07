using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrintTotalScore : MonoBehaviour 
{
    TextFlash m_textFlash;
    
    public TextFlash textFlash
    {
        get { return m_textFlash; }
    }

    void Awake()
    {
        m_textFlash = GetComponent<TextFlash>();
    }

    void Start()
    {
        ResetScore(false);
    }

	// Use this for initialization
	void OnEnable () 
    {
        MoveRing.NewScore += AddScore;
        InputListener.ResetGame += ResetScore;

        GameData.NewCurrentScore += ScoreUpdateListener;
        GameData.NewMaxScore += ScoreUpdateListener;
        
        LocalizedTexts.OnLanguageChanged += PrintScore;
    }
    void OnDisable()
    {
        MoveRing.NewScore -= AddScore;
        InputListener.ResetGame -= ResetScore;
        
        GameData.NewCurrentScore -= ScoreUpdateListener;
        GameData.NewMaxScore -= ScoreUpdateListener;

        LocalizedTexts.OnLanguageChanged += PrintScore;
    }
	
	void AddScore(float score)
    {
        m_textFlash.FlashText(Color.green);

        SetScore(GameData.currentScore + score, GameData.maxScore + 10);
    }

    public void ResetScore()
    {
        ResetScore(true);
    }

    public void ResetScore(bool flash)
    {
        if (flash)
        {
            m_textFlash.FlashText(Color.red);
        }

        SetScore(0, 0);
    }

    void SetScore(float currentScore, float maxScore)
    {
        //Don't listen for changes from self
        GameData.NewCurrentScore -= ScoreUpdateListener;
        GameData.NewMaxScore -= ScoreUpdateListener;
        
        //Update score
        GameData.currentScore = (int)currentScore;
        GameData.maxScore = (int)maxScore;
        PrintScore();

        //Start listening for changes again
        GameData.NewCurrentScore += ScoreUpdateListener;
        GameData.NewMaxScore += ScoreUpdateListener;
    }

    void PrintScore()
    {
        string text = GetComponent<Text>().text;
        text = LocalizedTexts.GetText(LocalizedTexts.Text.Total) + ": " + GameData.currentScore + "/" + GameData.maxScore;
        GetComponent<Text>().text = text;
    }

    void ScoreUpdateListener(int i)
    {
        PrintScore();
    }
}
