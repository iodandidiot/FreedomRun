using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    private Vector2 mousePos;
    public GameObject player;
	// Use this for initialization
	void Start () 
    {
        transform.position = new Vector3(-11, transform.position.y,transform.position.z);
	}
	
	// Update is called once per frame
    public void Right()
    {
        if (transform.position.x < 11f)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }

    }
    public void Left()
    {
        if (transform.position.x > -11f)
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.position.x - mousePos.x > 5 && player.activeSelf==false)
        {
            Left();
        }
        if (mousePos.x - transform.position.x > 5 && player.activeSelf == false)
        {
            Right();
        }
        if (player.activeSelf == true)
        {
            transform.position = new Vector3(player.transform.position.x + 6f, transform.position.y, transform.position.z);
        }
    }
}
