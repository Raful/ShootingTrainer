using UnityEditor;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    [MenuItem("Utils/Clear saved data (no confirmation)")]
    static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
