using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class GoalHandler : MonoBehaviour 
{
    public delegate void GoalUpdateDel();
    public event GoalUpdateDel GoalReached;
    public event GoalUpdateDel GoalFailed;

    [SerializeField]
    PrintTotalScore m_scoreScript;
    
    TextFlash m_textFlash;

    public PrintTotalScore scoreScript
    {
        get { return m_scoreScript; }
    }

    void Awake()
    {
        m_textFlash = GetComponent<TextFlash>();
    }

	// Use this for initialization
	void OnEnable () 
    {
        MoveRing.NewScore += HandleNewScore;
    }

    void OnDisable()
    {
        MoveRing.NewScore -= HandleNewScore;
    }

    void OnApplicationPause(bool pauseState)
    {
        if (pauseState)
        {
            Analytics.CustomEvent("StoppedPlaying", new Dictionary<string, object>
            {
                {"Goal", GameData.currentGoal},
                {"CurrentScore", GameData.currentScore },
                {"CurrentMaxScore", GameData.maxScore}
            });
        }
    }
	
    void HandleNewScore(float score)
    {
        if (GameData.maxScore == GameData.currentGoal.max)
        {
            //Check if goal is achieved or not
            if (GameData.currentScore >= GameData.currentGoal.score)
            {
                if (GoalReached != null)
                {
                    GoalReached();
                }
            }
            else
            {
                if (GoalFailed != null)
                {
                    GoalFailed();
                }
                m_scoreScript.textFlash.FlashText(Color.red);
            }

        }
    }
}
