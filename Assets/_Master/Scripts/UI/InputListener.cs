using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System;

public class InputListener : MonoBehaviour
{
    #region delegates and events

    public delegate void InputDelegate();

    public static event InputDelegate Shoot;
    public static event InputDelegate ResetGame;

    #endregion //delegates and events

    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
	}

    void HandleMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider);
        }
        else if (Advertisement.isShowing)
        {
            //Do nothing
        }
        else if (Camera.main.pixelRect.Contains(Input.mousePosition))
        {
            Shoot();
        }
    }

    public void ResetGameClicked()
    {
        if (ResetGame != null)
        {
            ResetGame();
        }
    }

    public void ChangeLanguage()
    {
        LocalizedTexts.SetLanguage(GetNextLanguage());
    }

    private LocalizedTexts.Language GetNextLanguage()
    {
        Array a = Enum.GetValues(typeof(LocalizedTexts.Language));
        int i = 0;
        int length = a.GetLength(0);
        for (i = 0; i < length; i++)
        {
            if ((LocalizedTexts.Language)a.GetValue(i) == LocalizedTexts.CurrentLanguage)
            {
                //Found the current language
                if (i == length - 1)
                {
                    //Current language is the last one in the enum, choose the first one
                    return (LocalizedTexts.Language)a.GetValue(0);
                }

                return (LocalizedTexts.Language)a.GetValue(i + 1);
            }
        }

        //This should never occur
        return LocalizedTexts.Language.English;
    }
}
