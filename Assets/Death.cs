using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
    public float speed = 2f;

    // Update is called once per frame
    void Update () {
		var x = this.transform.localScale;
		x.y += speed * Time.deltaTime;
		speed *= 1.001f;
		this.transform.localScale = x;
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.transform.tag);
		if (other.transform.tag == "Player")
			Debug.Log("RED AND DEAD");
	}
}
