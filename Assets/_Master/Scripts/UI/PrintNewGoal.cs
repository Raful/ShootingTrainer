using UnityEngine;
using UnityEngine.UI;

public class PrintNewGoal : MonoBehaviour
{
    TextFlash m_textFlash;
    Goal m_currentGoal;

    void Awake()
    {
        m_textFlash = GetComponent<TextFlash>();
        m_currentGoal = GameData.currentGoal;
    }

    void OnEnable()
    {
        GameData.NewGoal += PrintGoalScore;
        LocalizedTexts.OnLanguageChanged += OnLanguageChanged;
    }

    void OnDisable()
    {
        GameData.NewGoal -= PrintGoalScore;
        LocalizedTexts.OnLanguageChanged -= OnLanguageChanged;
    }
    
    void PrintGoalScore(Goal goal)
    {
        PrintGoalScore(goal, true);
    }

    void PrintGoalScore(Goal goal, bool flash)
    {
        GetComponent<Text>().text = LocalizedTexts.GetText(LocalizedTexts.Text.Goal) + ": " + goal.score + "/" + goal.max;

        if (flash)
            FlashText();

        m_currentGoal = goal;
    }

    void FlashText()
    {
        m_textFlash.FlashText(Color.green);
    }

    void OnLanguageChanged()
    {
        PrintGoalScore(GameData.currentGoal, false);
    }
}
