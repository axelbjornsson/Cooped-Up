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
		if (other.transform.tag == "Obstacle" || other.transform.tag == "BlockContainer" || other.transform.tag == "CompoundBlock")
		{
			var x = other.transform.GetComponent<Rigidbody2D>();
            if(x == null || x.isKinematic)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.GetComponent<Rigidbody2D>().isKinematic = true;
            }

            if (gameObject.tag == "BlockContainer")
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
		if (other.transform.tag == "Player")
		{
            Player player = other.transform.GetComponent<Player>();
            
            if (player.controller.collisions.below)
            {
                if(transform.position.y > other.transform.position.y)
                GameObject.Find("GameManager").GetComponent<GameController>().GameOver();
            }
        }
	}

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacle" || other.transform.tag == "BlockContainer" || other.transform.tag == "CompoundBlock")
        {
            var x = other.transform.GetComponent<Rigidbody2D>();
            if (x == null || x.isKinematic)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                this.GetComponent<Rigidbody2D>().isKinematic = true;
            }

            if (gameObject.tag == "BlockContainer")
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

}
