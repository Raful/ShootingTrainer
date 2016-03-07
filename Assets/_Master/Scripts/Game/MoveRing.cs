using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class MoveRing : MonoBehaviour 
{
    [SerializeField]
    float m_speed = 1, m_jitter = 0.1f, m_changeDelay = 0.3f;
    [SerializeField]
    Transform m_target;

    float m_timer;
    Vector2 m_direction;
    bool m_paused = true;
    Vector3 m_startPosition;

    public delegate void ScoreDel(float score);
    public delegate void StartedDel();

    public static event ScoreDel NewScore;
    public static event StartedDel Started;

	// Use this for initialization
	void Start () 
    {
        m_startPosition = transform.position;
	}

    void OnEnable()
    {
        InputListener.Shoot += ShootHandler;
        InputListener.ResetGame += Reset;
    }

    void OnDisable()
    {
        InputListener.Shoot -= ShootHandler;
        InputListener.ResetGame -= Reset;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (!m_paused)
        {
            m_timer += Time.deltaTime;

            if (m_timer > m_changeDelay)
            {
                m_timer -= m_changeDelay;

                Vector2 diffPosition = m_target.position - transform.position;
                Vector2 randDirection = Random.insideUnitCircle.normalized;

                if (diffPosition.y > m_jitter && randDirection.y < 0 ||
                    diffPosition.y < m_jitter && randDirection.y > 0)
                {
                    randDirection.y = -randDirection.y;
                }

                if (diffPosition.x > m_jitter && randDirection.x < 0 ||
                    diffPosition.x < m_jitter && randDirection.x > 0)
                {
                    randDirection.x = -randDirection.x;
                }

                m_direction = randDirection;
            }

            transform.Translate(m_direction * m_speed * Time.deltaTime);
        }
	}

    void ShootHandler()
    {
        if (!m_paused)
        {
            Shoot();
        }
        else
        {
            transform.position = m_startPosition;
            m_timer = m_changeDelay;
            Started();
        }

        m_paused = !m_paused;
    }

    void Shoot()
    {
        float distanceFromTarget = ((Vector2)(m_target.position - transform.position)).magnitude;
        float score = 10.9f;

        score -= distanceFromTarget * 15;

        if (score < 0)
        {
            score = 0;
        }

        //Round to one decimal
        score = Mathf.Round(score * 10);
        score /= 10;

        if (NewScore != null)
        {
            NewScore(score);
        }

        Debug.Log(score);
    }

    void Reset()
    {
        transform.position = m_startPosition;
        m_paused = true;
    }
}
