using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buid_bridge : MonoBehaviour {

    public GameObject player;
    public List <Vector2> pointsList;
    private Vector2 mousePos;
    public GameObject creatBridge;
    public bool crB;
    public GameObject pointSprite;
    BoxCollider2D coll;
    GameObject point1;
    GameObject point2;
	// Use this for initialization
	void Start () 
    {
        player.gameObject.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (pointsList.ToArray().Length == 2 && !crB )
        {
            crB = true;

            BuildWithSpringJoint(Mathf.Sqrt(Mathf.Pow((pointsList[0].x - pointsList[1].x), 2) + Mathf.Pow((pointsList[0].y - pointsList[1].y), 2)));
            //EdgeCollider2D edge = creatBridge.AddComponent<EdgeCollider2D>();
            //edge.points = pointsList.ToArray();
            //Rigidbody2D rigid = creatBridge.AddComponent<Rigidbody2D>();
            //rigid.isKinematic = true;            
            //player.gameObject.SetActive(true);                                   
            
        }
        if (Input.GetMouseButtonUp(0) && pointsList.ToArray().Length < 2)
        {            
                
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointsList.Add(mousePos);
            if(point1==null)point1=(GameObject)Instantiate(pointSprite, mousePos, Quaternion.identity);
            else point2 = (GameObject)Instantiate(pointSprite, mousePos, Quaternion.identity);
        }
	    
    
    }
    public void BuildWithHingeJoint(float leng)
    {
        GameObject creatBridge1=(GameObject)Instantiate(creatBridge,new Vector2 (pointsList[0].x, pointsList[0].y), Quaternion.identity);
        HingeJoint2D djoint = creatBridge1.AddComponent<HingeJoint2D>();
        int num = (int)(leng /0.4f);
        Rigidbody2D rgPoint1=point1.GetComponent<Rigidbody2D>();
        djoint.connectedBody = rgPoint1;
        djoint.anchor = new Vector2(0.06f,0);
        djoint.connectedAnchor=new Vector2(-0.06f,0);
        GameObject[] creatBridgeAll = new GameObject[num - 1];
        HingeJoint2D[] djointAll = new HingeJoint2D[num - 1];
        Rigidbody2D[] rigidbody2D = new Rigidbody2D[num - 1];
        for (int i = 0; i < num-1; i++)
        {
            if (i == 0)
            {
                creatBridgeAll[0] = (GameObject)Instantiate(creatBridge, new Vector2(0, 0), Quaternion.identity);
                djointAll[0] = creatBridgeAll[0].AddComponent<HingeJoint2D>();
                rigidbody2D[0] = creatBridge1.GetComponent<Rigidbody2D>();
                djointAll[0].connectedBody = rigidbody2D[0];
                djointAll[0].anchor= new Vector2(0.06f,0);
                djointAll[0].connectedAnchor = new Vector2(-0.06f, 0);
            }
            else
            {
                creatBridgeAll[i] = (GameObject)Instantiate(creatBridge, new Vector2(0, 0), Quaternion.identity);
                djointAll[i] = creatBridgeAll[i].AddComponent<HingeJoint2D>();
                rigidbody2D[i] = creatBridgeAll[i - 1].GetComponent<Rigidbody2D>();
                djointAll[i].connectedBody = rigidbody2D[i];
                djointAll[i].anchor = new Vector2(0.06f, 0);
                djointAll[i].connectedAnchor = new Vector2(-0.06f, 0);
            }
        }
        HingeJoint2D djointLast = creatBridgeAll[num - 2].AddComponent<HingeJoint2D>();
        Rigidbody2D rigidbodyLast = point2.GetComponent<Rigidbody2D>();
        djointLast.connectedBody = rigidbodyLast;
        djointLast.anchor = new Vector2(-0.06f, 0);
        djointLast.connectedAnchor = new Vector2(0.06f, 0);
        player.gameObject.SetActive(true);
    }

    public void BuildWithSpringJoint(float leng)
    {
        GameObject creatBridge1 = (GameObject)Instantiate(creatBridge, new Vector2(pointsList[0].x, pointsList[0].y), Quaternion.identity);
        SpringJoint2D djoint = creatBridge1.AddComponent<SpringJoint2D>();
        int num = (int)(leng / 0.26f);
        Rigidbody2D rgPoint1 = point1.GetComponent<Rigidbody2D>();
        djoint.connectedBody = rgPoint1;
        djoint.anchor = new Vector2(0, 0);
        djoint.connectedAnchor = new Vector2(0, 0);
        djoint.distance = 0;
        djoint.dampingRatio = 1;
        djoint.frequency = 7;
        //djoint.gameObject.SetActive(false);
        GameObject[] creatBridgeAll = new GameObject[num - 1];
        SpringJoint2D[] djointAll = new SpringJoint2D[num - 1];
        Rigidbody2D[] rigidbody2D = new Rigidbody2D[num - 1];
        for (int i = 0; i < num - 1; i++)
        {
            if (i == 0)
            {
                creatBridgeAll[0] = (GameObject)Instantiate(creatBridge, new Vector2(0, 0), Quaternion.identity);
                djointAll[0] = creatBridgeAll[0].AddComponent<SpringJoint2D>();
                rigidbody2D[0] = creatBridge1.GetComponent<Rigidbody2D>();
                djointAll[0].connectedBody = rigidbody2D[0];
                djointAll[0].anchor = new Vector2(0, 0);
                djointAll[0].connectedAnchor = new Vector2(0, 0);
                djointAll[0].distance = 0;
                djointAll[0].dampingRatio = 1;
                djointAll[0].frequency = 7;
                //djointAll[0].gameObject.SetActive(false);
            }
            else
            {
                creatBridgeAll[i] = (GameObject)Instantiate(creatBridge, new Vector2(0, 0), Quaternion.identity);
                djointAll[i] = creatBridgeAll[i].AddComponent<SpringJoint2D>();
                rigidbody2D[i] = creatBridgeAll[i - 1].GetComponent<Rigidbody2D>();
                djointAll[i].connectedBody = rigidbody2D[i];
                djointAll[i].anchor = new Vector2(0, 0);
                djointAll[i].connectedAnchor = new Vector2(0, 0);
                djointAll[i].distance = 0;
                djointAll[i].dampingRatio = 1;
                djointAll[i].frequency = 7;
                //djointAll[i].gameObject.SetActive(false);
            }
        }
        SpringJoint2D djointLast = creatBridgeAll[num - 2].AddComponent<SpringJoint2D>();
        Rigidbody2D rigidbodyLast = point2.GetComponent<Rigidbody2D>();
        djointLast.connectedBody = rigidbodyLast;
        djointLast.anchor = new Vector2(0, 0);
        djointLast.connectedAnchor = new Vector2(0, 0);
        djointLast.distance = 0;
        djointLast.dampingRatio = 1;
        djointLast.frequency = 7;
        //djointLast.gameObject.SetActive(false);
        player.gameObject.SetActive(true);

    }

}
