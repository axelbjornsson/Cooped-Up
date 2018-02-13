using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour {

    
    public float radius;
    public float speed;

	// Use this for initialization
	void Start () {
        Color color = new Color(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
        GetComponent<SpriteRenderer>().color = color;

        transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        transform.Translate(Vector2.up*radius); 

        StartCoroutine(TimedDestruction());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(Vector2.up * speed);
    }

    IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
