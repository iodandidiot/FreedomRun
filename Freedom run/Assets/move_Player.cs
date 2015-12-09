using UnityEngine;
using System.Collections;

public class move_Player : MonoBehaviour {

    public float statVelosity;


	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        Rigidbody2D rigidPlayer = gameObject.GetComponent<Rigidbody2D>();
        rigidPlayer.velocity = new Vector2(statVelosity, gameObject.transform.position.y);
	}
}
