using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigParticle : MonoBehaviour
{
    public float radius;
    public float speed;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        transform.Translate(Vector2.up * radius);

        StartCoroutine(TimedDestruction());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed);
    }

    IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
