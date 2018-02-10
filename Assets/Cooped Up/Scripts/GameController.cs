using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    public float xMin;
    public float xMax;

    public int score;

    public List<Transform> players;
    public GameObject startScreen;
    public GameObject mainGameScreen;
    public GameObject highScoreScreen;
    public Spawner spawner;

    private Vector2[] playerPositions;
	// Use this for initialization
	void Start () {
        playerPositions = new Vector2[2];
        playerPositions[0] = players[0].position;
        playerPositions[1] = players[1].position;
	}
	
	// Update is called once per frame
	void Update () {
        //CheckWrapping();
	}

    public void StartGame()
    {
        mainGameScreen.SetActive(true);
        //highScoreScreen.SetActive(false); doesn't exist yet
        startScreen.SetActive(false);
        spawner.enabled = true;
        ResetGameState();
    }

    private void ResetGameState()
    {
        foreach(Transform child in spawner.blockParent.transform)
        {
            Destroy(child.gameObject);
        }
        players[0].position = playerPositions[0];
        players[1].position = playerPositions[1];
    }

    /*
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
    */
}
