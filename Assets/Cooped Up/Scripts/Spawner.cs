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

	public float spawnDelay;
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
        screenHalfWidth = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth,0)).x;
	}
    
    // Update is called once per frame
    void FixedUpdate () {
        //transform.Translate(new Vector2(0, 0.01f));

		if (!waitingSpawn) {
            if(itemSpawn.Count == 0)
            {
                specialQueueIncrement %= 30;
                if(specialQueueIncrement == 20)
                {
                    QueueStairs();
                }
                else if(specialQueueIncrement == 15)
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
        for (int i = 0; i < 20; i++)
        {
            itemSpawn.Enqueue(new SpawnItem(tree[0], transform.position, 0.4f));
        }
        itemSpawn.Enqueue(new SpawnItem(tree[1], transform.position, 3f));
    }

    private void QueueStairs()
    {
        int slices = 5;
        float widthIncrement = screenHalfWidth / slices;
        if (Random.value > 0.5f)
        {
            for (int i = -slices; i < slices; i++)
            {
                float time = 1f;
                if (i == slices - 1) time = 2.5f;
                itemSpawn.Enqueue(new SpawnItem(debris[2], new Vector2(transform.position.x + (i * widthIncrement), transform.position.y), time));
            }
        }
        else
        {
            for (int i = slices; i > -slices; i--)
            {
                float time = 1f;
                if (i == -slices + 1) time = 2.5f;
                itemSpawn.Enqueue(new SpawnItem(debris[2], new Vector2(transform.position.x + (i * widthIncrement), transform.position.y), time));
            }
        }
    }

    private void QueueRandom()
    {
		GameObject randSpawn = debris[Random.Range(0, debris.Count)];
		Vector3 randPos = new Vector2(Random.Range(- screenHalfWidth, 
												   screenHalfWidth), 
									  transform.position.y);
        itemSpawn.Enqueue(new SpawnItem(randSpawn, randPos, nextSpawnTime));
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
        specialQueueIncrement = 0;
    }
}
