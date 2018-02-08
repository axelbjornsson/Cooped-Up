using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Obstacle" || other.transform.tag == "BlockContainer")
		{
			var x = other.transform.GetComponent<Rigidbody2D>();
			if (x == null || x.isKinematic)
				this.GetComponent<Rigidbody2D>().isKinematic = true;

            if (gameObject.tag == "BlockContainer")
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
		if (other.transform.tag == "Player")
		{
			other.transform.GetComponent<GamePlayer>().Die();
		}
	}

}
