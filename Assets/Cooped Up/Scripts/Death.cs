using UnityEngine;

public class Death : MonoBehaviour {
    public float initialSpeed = 2f;

    private float speed;
    
    public void Reset()
    {
        transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        speed = initialSpeed;
    }

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
        if (other.transform.tag == "Player")
		{
            GameObject.Find("GameManager").GetComponent<GameController>().GameOver();
			//other.GetComponent<GamePlayer>().Die(); 
		}
    }
}
