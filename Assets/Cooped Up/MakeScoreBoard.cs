using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
    [MenuItem("Assets/Create/ScoreBoard")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ScoreBoard>();
    }
}