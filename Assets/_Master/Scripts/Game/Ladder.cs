using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
    [SerializeField]
    Goal[] m_goals;

    const string CURRENT_INDEX_KEY = "LadderIndex";

    GoalHandler m_goalHandlerScript;

    int m_currentIndex
    {
        get
        {
            return PlayerPrefs.GetInt(CURRENT_INDEX_KEY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(CURRENT_INDEX_KEY, value);
        }
    }

	// Use this for initialization
	void Awake () 
    {
        m_goalHandlerScript = GetComponent<GoalHandler>();

	}

    void Start()
    {
        GameData.currentGoal = m_goals[m_currentIndex];
    }

    void OnEnable()
    {
        m_goalHandlerScript.GoalReached += GoalReached;
        m_goalHandlerScript.GoalFailed += GoalFailed;
    }

    void OnDisable()
    {
        m_goalHandlerScript.GoalReached -= GoalReached;
        m_goalHandlerScript.GoalFailed -= GoalFailed;
    }
	
	void GoalReached()
    {
        m_currentIndex++;

        if (m_currentIndex >= m_goals.Length)
        {
            //Reached end of ladder
            m_currentIndex = 0;
        }

        GameData.currentGoal = m_goals[m_currentIndex];

        InputListener.Shoot += ResetScore;
    }

    void GoalFailed()
    {
        InputListener.Shoot += ResetScore;
        //m_currentIndex--;

        if (m_currentIndex < 0)
        {
            m_currentIndex = 0;
        }

        //m_goalHandlerScript.SetGoal(m_goals[m_currentIndex]);
    }

    void ResetScore()
    {
        m_goalHandlerScript.scoreScript.ResetScore();

        InputListener.Shoot -= ResetScore;
    }
}
