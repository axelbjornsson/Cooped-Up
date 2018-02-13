using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnPlayer : MonoBehaviour {


	public LayerMask playerLayer;
    private float playerWidth;
    private int slices = 3;
    private float widthSlice;

    public float bounciness;
	// Use this for initialization
	void Start () {
        playerWidth = GetComponent<BoxCollider2D>().size.x;
        widthSlice = playerWidth / slices;
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = -slices - 1; i <= slices + 1; i++)
        {
            Vector2 rayOrigin = new Vector2(this.transform.position.x + i * widthSlice*2, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin,  Vector2.down, 0.7f, playerLayer);

            Debug.DrawRay(rayOrigin, Vector2.down * 5, Color.red);

            if (hit.collider != null && hit.transform.tag == "Player")
            {
                GetComponent<Player>().Bounce(bounciness);
            }
        }
	}
}
