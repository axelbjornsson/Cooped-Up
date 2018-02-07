using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnPlayer : MonoBehaviour {


	public LayerMask playerLayer;
	public GameObject otherPlayer;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down * 10f, 1f, playerLayer);
	
		if (hit.collider != null && hit.transform.tag == "Player") {
			otherPlayer = hit.transform.gameObject;
			otherPlayer.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
			this.transform.SetParent(otherPlayer.transform);			
		} else if (otherPlayer != null) {
			otherPlayer.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
			this.transform.SetParent(null);
			otherPlayer = null;
		}
	}
}
