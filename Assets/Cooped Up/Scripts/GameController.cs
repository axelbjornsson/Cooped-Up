using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public ScoreBoard scoreBoard;

    //0: start screen
    //1: main screen
    //2: hiscore screen
    byte state;

    [System.Serializable]
    public class BackgroundColors : System.Object
    {
        public Color mainMenuColor;
        public Color hiScoreScreenColor;
        public Color originalColor;
        public Color targetColor;
    }
    public BackgroundColors colors;

    //This is to handle high-score
    private Vector2 highestPoint;
    private Vector2 originalPoint;

    [System.Serializable]
    public class ObjectReferences : System.Object
    {
        //object references for various things
        public List<Transform> players;
        public GameObject startScreen;
        public GameObject mainGameScreen;
        public GameObject highScoreScreen;
        public Spawner spawner;
        public Death death;
    }
    public ObjectReferences referenceObjects;

    private bool gameWon;
    public GameObject WinText;

    private Vector2[] playerPositions;
	// Use this for initialization
	void Start () {
        state = 0;

        Camera.main.backgroundColor = colors.mainMenuColor;
        Camera.main.GetComponent<Camera2Player>().enabled = false;

        originalPoint = Camera.main.transform.position;

        playerPositions = new Vector2[2];
        playerPositions[0] = referenceObjects.players[0].position;
        playerPositions[1] = referenceObjects.players[1].position;
	}
	
	// Update is called once per frame
	void Update () {
        if(Camera.main.transform.position.y > 40)
        {
            gameWon = true;
            Debug.Log(Camera.main.transform.position.y);
            GameObject.Find("Win text").SetActive(true);
        }

        if(highestPoint.y < Camera.main.transform.position.y && referenceObjects.mainGameScreen.activeSelf)
        {
            Camera.main.backgroundColor = Color.Lerp(colors.originalColor, colors.targetColor, (highestPoint.y - originalPoint.y)/150);
            highestPoint = Camera.main.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(state == 0 || state == 2 || gameWon)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R))
            {
                StartGame();
            }
        }
	}

    public void StartGame()
    {
        state = 1;

        Camera.main.backgroundColor = colors.originalColor;

        Camera.main.GetComponent<Camera2Player>().enabled = true;

        referenceObjects.mainGameScreen.SetActive(true);
        referenceObjects.highScoreScreen.SetActive(false);
        referenceObjects.startScreen.SetActive(false);
        referenceObjects.spawner.enabled = true;
        ResetGameState();
    }

    public void GameOver()
    {
        state = 2;

        Camera.main.backgroundColor = colors.hiScoreScreenColor;

        referenceObjects.mainGameScreen.SetActive(false);

        //highScoreScreen.SetActive(false); doesn't exist yet
        referenceObjects.highScoreScreen.SetActive(true);

        int score = (int)(highestPoint.y - originalPoint.y);
        if (scoreBoard.scores[0] < score)
        {
            scoreBoard.scores[0] = score;
        }

        GameObject.Find("TempHiScore").GetComponent<TextMesh>().text = scoreBoard.scores[0].ToString();
        GameObject.Find("TempScore").GetComponent<TextMesh>().text = score.ToString();
        GameObject.Find("HiScorePlayer1").transform.position = playerPositions[0];
        GameObject.Find("HiScorePlayer2").transform.position = playerPositions[1];

        referenceObjects.spawner.enabled = false;

        //a temporary fix, not a very good one.
        Camera.main.GetComponent<Camera2Player>().enabled = false;
        Camera.main.transform.position = new Vector3(0, 4.4f, -10);
    }

    private void ResetGameState()
    {
        highestPoint = originalPoint;
        foreach(Transform child in referenceObjects.spawner.blockParent.transform)
        {
            Destroy(child.gameObject);
        }
        referenceObjects.death.Reset();

        referenceObjects.players[0].position = playerPositions[0];
        referenceObjects.players[0].gameObject.SetActive(true);
        referenceObjects.players[1].position = playerPositions[1];
        referenceObjects.players[1].gameObject.SetActive(true);
        
        referenceObjects.spawner.GetComponent<Spawner>().Reset();

        gameWon = false;
        WinText.SetActive(false);
    }
}
