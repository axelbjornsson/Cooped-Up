using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnPlayer : MonoBehaviour {


	public LayerMask playerLayer;

    public float bounciness;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down * 10f, 0.7f, playerLayer);
	
		if (hit.collider != null && hit.transform.tag == "Player") {
            GetComponent<Player>().Bounce(bounciness);			
		}
	}
}
