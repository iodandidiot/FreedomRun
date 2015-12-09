using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(10, 0));
        //rigid.angularVelocity
	}
}
