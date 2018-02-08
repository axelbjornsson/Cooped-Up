using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    public float xMin;
    public float xMax;

    public List<Transform> players;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckWrapping();
	}

    void CheckWrapping()
    {
        foreach(Transform t in players)
        {
            if (t.position.x < xMin)
            {
                t.position = new Vector2(xMax, t.position.y);
            }
            else if (t.position.x > xMax)
            {
                t.position = new Vector2(xMin, t.position.y);
            }
        }
    }
}
