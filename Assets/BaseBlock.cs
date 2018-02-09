using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour {

	public void DisableBlock()
    {
        Color colorIncrement = new Color(0.3f, 0.3f, 0.3f, 0f);
        GetComponent<SpriteRenderer>().color -=  colorIncrement;

        GetComponent<Collider2D>().enabled = false;
        transform.Translate(new Vector3(0,0,1));
    }

}
