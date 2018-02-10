using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlockScript : MonoBehaviour {

    bool isSelected;

    public float lerpIncrement = 0.001f;
    float lerpProgress;

    bool p1Selected;
    bool p2Selected;
	// Use this for initialization
	void Start ()
    {
        p1Selected = false;
        p2Selected = false;
        isSelected = false;
    }
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "StartPlayer1" || coll.gameObject.name == "HiScorePlayer1")
        {
            p1Selected = true;
            isSelected = true;
        }
        else if (coll.gameObject.name == "StartPlayer2" || coll.gameObject.name == "HiScorePlayer2")
        {
            p2Selected = true;
            isSelected = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.name == "StartPlayer1" || coll.gameObject.name == "HiScorePlayer1")
        {
            p1Selected = false;
        }
        else if (coll.gameObject.name == "StartPlayer2" || coll.gameObject.name == "HiScorePlayer2")
        {
            p2Selected = false;
        }

        if(!p1Selected && !p2Selected)
        {
            isSelected = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (isSelected)
        {
             ChangeChildrenColor(lerpIncrement, Color.green);
        }
        else
        {
             ChangeChildrenColor(lerpIncrement/2, Color.black);
        }
	}

    //A super inefficent way to do things, hopefully nothing lags.
    void ChangeChildrenColor(float lerpValue, Color targetColor)
    {
        foreach(Transform child in transform)
        {

            var sprite = child.gameObject.GetComponent<SpriteRenderer>();

            if (sprite.color.g >= targetColor.g - 0.07f && targetColor == Color.green)
            {
                GameObject.Find("GameManager").GetComponent<GameController>().StartGame();
                break;
            }

            sprite.color = Color.Lerp(sprite.color, targetColor, lerpValue);
            
        }
    }
}
