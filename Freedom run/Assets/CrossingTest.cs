using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrossingTest : MonoBehaviour {

    public GameObject Point;
    public List<GameObject> pointList;
    public List<Vector2> pointList1;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "Djoints" && coll.tag != "bg_for_djoint")
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
        pointList.RemoveAt(1);
        pointList1.RemoveAt(1);
        Destroy(gameObject);

    }
}
