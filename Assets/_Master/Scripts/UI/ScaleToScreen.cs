using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleToScreen : MonoBehaviour 
{
    [SerializeField]
    int m_standardHeight = 500;
    [SerializeField]
    Camera m_camera = Camera.main;

	// Use this for initialization
	void Start () 
    {
        Text text = GetComponent<Text>();
        text.fontSize *= m_camera.pixelHeight / m_standardHeight;
	}
}
