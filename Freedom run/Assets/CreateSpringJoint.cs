using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CreateSpringJoint : MonoBehaviour {
    public GameObject player;
    public List <Vector2> pointsList;
    public List <GameObject> points;
    private Vector2 mousePos;
    public GameObject creatBridge;
    public bool crB;
    public GameObject pointSprite;
    BoxCollider2D coll;
    public bool start = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (start && !crB)
        {
            CreatAll();
            crB = true;
        }
        if (Input.GetMouseButtonUp(0) && !start)
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //pointsList.Add(mousePos);
            points.Add((GameObject)Instantiate(pointSprite, mousePos, Quaternion.identity));
        }
	}

    public void PointAndDjoint(float leng, GameObject point1, GameObject point2)//Функция получает длину отрезка, первую и последнюю точки
    {
        //соединяем полученую точку и первую ячейку
        GameObject creatBridge1 = (GameObject)Instantiate(creatBridge, new Vector2(point1.transform.position.x, point1.transform.position.y), Quaternion.identity);
        SpringJoint2D djoint = creatBridge1.AddComponent<SpringJoint2D>();
        //int num = (int)(leng / 0.26f);
        float rPoint = creatBridge1.GetComponent<CircleCollider2D>().radius;
        int num = (int)(leng / (2 * rPoint));
        Rigidbody2D rgPoint1 = point1.GetComponent<Rigidbody2D>();
        djoint.connectedBody = rgPoint1;
        DjointPar(djoint);
        GameObject[] creatBridgeAll = new GameObject[num - 1];
        SpringJoint2D[] djointAll = new SpringJoint2D[num - 1];
        Rigidbody2D[] rigidbody2D = new Rigidbody2D[num - 1];
        for (int i = 0; i < num - 1; i++)
        {
            if (i == 0)
            {
                //соединяем вторую ячейку с первой
                creatBridgeAll[0] = (GameObject)Instantiate(creatBridge, new Vector2(point1.transform.position.x+rPoint*(1+i), point1.transform.position.y), Quaternion.identity);
                djointAll[0] = creatBridgeAll[0].AddComponent<SpringJoint2D>();
                rigidbody2D[0] = creatBridge1.GetComponent<Rigidbody2D>();
                djointAll[0].connectedBody = rigidbody2D[0];
                DjointPar(djointAll[0]);
            }
            else
            {
                //соединяем остальные ячейку между собой
                creatBridgeAll[i] = (GameObject)Instantiate(creatBridge, new Vector2(point1.transform.position.x + rPoint * (1 + i), point1.transform.position.y), Quaternion.identity);
                djointAll[i] = creatBridgeAll[i].AddComponent<SpringJoint2D>();
                rigidbody2D[i] = creatBridgeAll[i - 1].GetComponent<Rigidbody2D>();
                djointAll[i].connectedBody = rigidbody2D[i];
                DjointPar(djointAll[i]);
            }            
        
        }
        //соединяем последнюю ячейку со второй полученной точкой
        SpringJoint2D djointLast = creatBridgeAll[num - 2].AddComponent<SpringJoint2D>();
        Rigidbody2D rigidbodyLast = point2.GetComponent<Rigidbody2D>();
        djointLast.connectedBody = rigidbodyLast;
        DjointPar(djointLast);
    
    }
    private void DjointPar(SpringJoint2D djoint)//Параметры джоинта
    {
        djoint.anchor = new Vector2(0, 0);
        djoint.connectedAnchor = new Vector2(0, 0);
        djoint.distance = 0;
        djoint.dampingRatio = 1;
        djoint.frequency = 7;
    }
    private void CreatAll()
    {
        float leng;
        for (int i = 0; i < points.ToArray().Length; i ++)
        {
            if (i+1 != points.ToArray().Length)
            {
                leng = Mathf.Sqrt(Mathf.Pow((points[i].transform.position.x - points[i + 1].transform.position.x), 2) +
                Mathf.Pow((points[i].transform.position.y - points[i + 1].transform.position.y), 2));
                PointAndDjoint(leng, points[i], points[i + 1]);
            }
            
        }
    }
    public void _Start()
    {
        start = true;
    }
}
