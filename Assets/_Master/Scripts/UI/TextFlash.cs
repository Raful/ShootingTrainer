using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour 
{
    Text m_textComponent;
    Color m_startColor;
    bool m_flashing = false;

    [SerializeField]
    float m_flashSpeed = 2;

    void Awake()
    {
        m_textComponent = GetComponent<Text>();
        m_startColor = m_textComponent.color;
    }

    void Update()
    {
        if (m_flashing)
        {
            Vector3 currentColor = new Vector3(m_textComponent.color.r, m_textComponent.color.g, m_textComponent.color.b);
            Vector3 targetColor = new Vector3(m_startColor.r, m_startColor.g, m_startColor.b);

            Vector3 newColorVector = Vector3.Lerp(currentColor, targetColor, Time.deltaTime * m_flashSpeed);
            Color newColor = new Color(newColorVector.x, newColorVector.y, newColorVector.z);

            m_textComponent.color = newColor;

            if (newColor == m_startColor)
            {
                m_flashing = false;
            }
        }
    }

    public void FlashText(Color color)
    {
        if (enabled)
        {
            m_textComponent.color = color;
            m_flashing = true;
        }
    }
}
