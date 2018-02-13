using UnityEngine;

public class Death : MonoBehaviour {
    public float initialSpeed = 2f;
    public Vector3 originalPos;

    private float speed;
    
    public void Reset()
    {
        speed = initialSpeed;
        transform.position = originalPos;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));
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
		}
    }
    
}
