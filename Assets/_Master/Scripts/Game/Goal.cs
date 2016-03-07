[System.Serializable]
public struct Goal
{
    public int score, max;
    private int m_id;
    private static int m_nextId = 0;

    public Goal(int score, int max)
    {
        this.score = score;
        this.max = max;

        m_id = m_nextId;
        m_nextId++;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Goal))
            return false; //Not a goal

        Goal otherGoal = (Goal)obj;
        return this == otherGoal;
    }

    public override int GetHashCode()
    {
        return m_id;
    }

    public override string ToString()
    {
        return "Score: " + score + ", max score: " + max;
    }

    public static bool operator ==(Goal thisGoal, Goal otherGoal)
    {
        return otherGoal.m_id == thisGoal.m_id;
    }

    public static bool operator !=(Goal thisGoal, Goal otherGoal)
    {
        return otherGoal.m_id != thisGoal.m_id;
    }
}
