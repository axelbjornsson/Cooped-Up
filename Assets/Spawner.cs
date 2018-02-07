using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public List<GameObject> basics = new List<GameObject>();
    
	public float spawnDelay;
	public Transform blockParent;
    private float screenLength;
    private float nextSpawnTime;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		screenLength = GetComponentInParent<Camera>().orthographicSize;
	}

    // Update is called once per frame
    void Update () {
		if (Time.time >= nextSpawnTime) {
			Spawn();
			nextSpawnTime = Time.time + spawnDelay;
		}
	}

    private void Spawn()
    {
		GameObject randSpawn = basics[Random.Range(0, basics.Count)];
		Vector3 randPos = new Vector2(Random.Range(transform.position.x - screenLength, 
												   transform.position.x + screenLength), 
									  transform.position.y);

		Instantiate(randSpawn, randPos, Quaternion.identity, blockParent);

    }
}
