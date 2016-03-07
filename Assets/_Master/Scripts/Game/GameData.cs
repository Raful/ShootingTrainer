using System;

public class GameData
{
    #region variables
    static int m_currentScore;
    static int m_maxScore;
    static Goal m_currentGoal;
    #endregion

    #region public setters and getters
    public static int currentScore
    {
        get { return m_currentScore; }
        set
        {
            m_currentScore = value;
            if (NewCurrentScore != null)
                NewCurrentScore(value);
        }
    }
    public static int maxScore
    {
        get { return m_maxScore; }
        set
        {
            m_maxScore = value;
            if (NewMaxScore != null)
                NewMaxScore(value);
        }
    }
    public static Goal currentGoal
    {
        get { return m_currentGoal; }
        set
        {
            m_currentGoal = value;
            if (NewGoal != null)
                NewGoal(value);
        }
    }
    #endregion

    #region events
    public static Action<int> NewCurrentScore;
    public static Action<int> NewMaxScore;
    public static Action<Goal> NewGoal;
    #endregion
}
