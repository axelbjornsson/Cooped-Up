using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Player : MonoBehaviour {


    public List<Transform> targets;

    public Vector3 offset;
    private float defaultX;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        defaultX = this.transform.position.x;
    }

    private void Update()
    {
        for(int i = 0; i<targets.Count; i++)
        {
            if(targets[i] == null)
            {
                targets.RemoveAt(i);
            }
        }
    }


	void LateUpdate () {

        if(targets.Count == 0)
        {
            return;
        }

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPos = centerPoint + offset;

        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
	}

    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return new Vector2(defaultX, targets[0].position.y);
        }

        var Bounds = new Bounds(targets[0].position, Vector3.zero);

        for(int i = 0; i < targets.Count; i++)
        {
            Bounds.Encapsulate(targets[i].position);
        }

        return new Vector3(defaultX, Bounds.center.y);
    }
}
