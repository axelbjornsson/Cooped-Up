using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBlockScript : MonoBehaviour {

    bool isSelected;

    public float lerpIncrement = 0.01f;
    float lerpProgress;

    bool p1Selected;
    bool p2Selected;
	// Use this for initialization
	void Start ()
    {
        isSelected = false;
    }
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.name == "StartPlayer1")
        {
            p1Selected = true;
            isSelected = true;
        }
        else if (coll.gameObject.name == "StartPlayer2")
        {
            p2Selected = true;
            isSelected = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.name == "StartPlayer1")
        {
            p1Selected = false;
        }
        else
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
		if(isSelected)
        {
             ChangeChildrenColor(lerpIncrement, Color.red);
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

            if (sprite.color.r >= targetColor.r - 0.07f && targetColor == Color.red)
            {
                Application.Quit();
                break;
            }

            sprite.color = Color.Lerp(sprite.color, targetColor, lerpValue);
            
        }
    }
}
