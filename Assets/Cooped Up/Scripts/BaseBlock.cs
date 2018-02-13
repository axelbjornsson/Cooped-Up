using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour {
    private GameObject digParticle;

	public void DisableBlock()
    {
        Camera.main.GetComponent<Animator>().Play("Shake");

        Color colorIncrement = new Color(0.3f, 0.3f, 0.3f, 0f);
        GetComponent<SpriteRenderer>().color -=  colorIncrement;

        GetComponent<Collider2D>().enabled = false;
        transform.Translate(new Vector3(0,0,1));

        digParticle = GameObject.Find("GameManager").GetComponent<GameController>().digParticle;

        for (int i = 0; i < 4; i++)
        {
            digParticle.transform.position = transform.position;
            digParticle.GetComponent<DigParticle>().speed = 0.4f;
            GameObject particle = Instantiate(digParticle);

        }
    }

}
