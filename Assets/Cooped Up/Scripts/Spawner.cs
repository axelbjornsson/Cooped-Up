using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public struct SpawnItem
    {
        public GameObject item;
        public Vector2 position;
        public float time;

        public SpawnItem(GameObject item, Vector2 position, float time)
        {
            this.item = item;
            this.position = position;
            this.time = time;
        }
    }

    Queue<SpawnItem> itemSpawn = new Queue<SpawnItem>();
    bool waitingSpawn;

    public List<GameObject> debris = new List<GameObject>();

    public GameObject[] tree = new GameObject[2];

    public GameObject[] rocks = new GameObject[2];

	public float initSpawnDelay;
    private float spawnDelay;
	public Transform blockParent;
    private float screenHalfWidth;
    private float nextSpawnTime;
    private int specialQueueIncrement;
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
        spawnDelay = initSpawnDelay;
        screenHalfWidth = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth,0)).x;
	}
    
    // Update is called once per frame
    void FixedUpdate () {
        //transform.Translate(new Vector2(0, 0.01f));

		if (!waitingSpawn) {
            if(itemSpawn.Count == 0)
            {
                specialQueueIncrement %= 20;
                if(specialQueueIncrement == 19)
                {
                    QueueStairs();
                }
                else if(specialQueueIncrement == 10)
                {
                    QueueDivider();
                }
                else
                {
                    QueueRandom();
                    spawnDelay -= 0.008f;
                    nextSpawnTime = spawnDelay;
                }
                specialQueueIncrement++;
            }
            else
            {
                SpawnItem nextItem = itemSpawn.Dequeue();
                Instantiate(nextItem.item, nextItem.position, Quaternion.identity, blockParent);
                StartCoroutine(SpawnWait(nextItem.time));
            }
        }

        if(Input.GetKeyDown("space"))
        {
            QueueStairs();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QueueDivider();
        }
    }

    private void QueueDivider()
    {
        Vector2 randPosition = new Vector2(Random.Range(-screenHalfWidth, screenHalfWidth), transform.position.y);
        for (int i = 0; i < 2; i++)
        {
            itemSpawn.Enqueue(new SpawnItem(tree[0], randPosition, 0.4f));
            itemSpawn.Enqueue(new SpawnItem(tree[0], randPosition, 0.4f));
            itemSpawn.Enqueue(new SpawnItem(tree[1], randPosition, 0.4f));
            itemSpawn.Enqueue(new SpawnItem(tree[0], randPosition, 0.4f));
            itemSpawn.Enqueue(new SpawnItem(tree[0], randPosition, 0.4f));
            itemSpawn.Enqueue(new SpawnItem(tree[2], randPosition, 0.4f));
        }
        itemSpawn.Enqueue(new SpawnItem(tree[3], randPosition, spawnDelay));
    }

    private void QueueStairs()
    {
        int slices = 5;
        byte rockToggle = 0;
        float widthIncrement = screenHalfWidth / slices;
        if (Random.value > 0.5f)
        {
            for (int i = -slices; i < 0; i++)
            {
                float time = 1f;
                if (i == slices - 1) time = 2.5f;
                itemSpawn.Enqueue(new SpawnItem(rocks[rockToggle++], new Vector2(transform.position.x + (i * widthIncrement), transform.position.y), time));

                rockToggle %= 2;
            }
        }
        else
        {
            for (int i = slices; i > 0; i--)
            {
                float time = 1f;
                if (i == -slices + 1) time = 2.5f;
                itemSpawn.Enqueue(new SpawnItem(rocks[rockToggle++], new Vector2(transform.position.x + (i * widthIncrement), transform.position.y), time));

                rockToggle %= 2;
            }
        }
    }

    private void QueueRandom()
    {
		GameObject randSpawn = debris[Random.Range(0, debris.Count)];
        itemSpawn.Enqueue(new SpawnItem(randSpawn, GetRandomPosition(), nextSpawnTime));
    }

    IEnumerator SpawnWait(float time)
    {
        waitingSpawn = true;
        yield return new WaitForSeconds(time);
        waitingSpawn = false;
    }

    public void Reset()
    {
        if(itemSpawn != null)itemSpawn.Clear();
        spawnDelay = initSpawnDelay;
        specialQueueIncrement = 0;
    }

    private Vector2 GetRandomPosition()
    {
        float p1X = GameObject.Find("Player1").transform.position.x;
        float p2X = GameObject.Find("Player2").transform.position.x;
        float midPoint = Mathf.Abs(p1X - p2X);
        float position = Random.Range(midPoint - screenHalfWidth / 1.5f, midPoint + screenHalfWidth / 1.5f);
        Debug.Log("p1: " + p1X + " p2X: " + p2X);
        Debug.Log("randpos: " + position);
        return new Vector2(position, transform.position.y);
    }
}
