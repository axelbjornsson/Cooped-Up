using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public List<GameObject> basics = new List<GameObject>();
    
	public float spawnDelay;
	public Transform blockParent;
    public int screenLength;
    private float nextSpawnTime;

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
		Vector3 randPos = new Vector2(Random.Range(transform.position.x - screenLength/2, 
												   transform.position.x + screenLength/2), 
									  transform.position.y);

		Instantiate(randSpawn, randPos, Quaternion.identity, blockParent);

    }
}
