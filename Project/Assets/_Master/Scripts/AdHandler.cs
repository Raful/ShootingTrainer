using UnityEngine;
using UnityEngine.Advertisements;

public class AdHandler : MonoBehaviour {

    [SerializeField][Tooltip("The delay (in seconds) before the ad can be shown again")]
    float m_adDelay;

    float m_nextAdTime = 0;

	// Use this for initialization
	void Start () {
        //Advertisement.Initialize("24819");
        MoveRing.NewScore += ShowAd;

        m_nextAdTime = Time.time + m_adDelay;

        if (m_adDelay < 60)
        {
            Debug.LogWarning("Ad delay very short: " + m_adDelay + " s");
        }
	}
	
	void ShowAd(float score)
    {
        if (m_nextAdTime <= Time.time)
        {
            m_nextAdTime = Time.time + m_adDelay;

            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    }

    ~AdHandler()
    {
        MoveRing.NewScore -= ShowAd;
    }
}
