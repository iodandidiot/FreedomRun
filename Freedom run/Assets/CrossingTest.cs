using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrossingTest : MonoBehaviour {

    public GameObject Point;   
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "Djoints")
        {
            SpriteRenderer colorPoint = Point.GetComponent<SpriteRenderer>();
            colorPoint.color = Color.red;
            StartCoroutine("DeletePoint");
        }
        
    }

    IEnumerator DeletePoint()
    {
        yield return new WaitForSeconds(1f);
        Destroy(Point.gameObject);
        Destroy(gameObject);

    }
}
