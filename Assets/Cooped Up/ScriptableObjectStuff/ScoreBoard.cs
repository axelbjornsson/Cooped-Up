using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : ScriptableObject {
    public List<int> scores;
    public int maxScores;

    public void addItem(int item)
    {
        scores.Add(item);
        scores.Sort();
        if (scores.Count > maxScores)
        {
            scores.RemoveAt(scores.Count - 1);
        }
    }
}
