using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Player : MonoBehaviour {


    public List<Transform> targets;

    public Vector3 offset;

	void LateUpdate () {

        if(targets.Count == 0)
        {
            return;
        }

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPos = centerPoint + offset;

        transform.position = newPos;
	}

    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        var Bounds = new Bounds(targets[0].position, Vector3.zero);

        for(int i = 0; i < targets.Count; i++)
        {
            Bounds.Encapsulate(targets[i].position);
        }

        return Bounds.center;
    }
}
