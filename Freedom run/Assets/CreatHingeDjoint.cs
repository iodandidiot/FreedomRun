using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatHingeDjoint : MonoBehaviour
{
    public GameObject player;
    public List<Vector2> pointsList=new List<Vector2>();    
    public List<GameObject> points=new List<GameObject>();
    private Vector2 mousePos;
    public GameObject creatBridge;
    public bool crB;
    public GameObject pointSprite;
    BoxCollider2D coll;
    public bool start = false;
    public GameObject test;
    public bool crossing=false;
    GameObject testInst;
    // Use this for initialization
    void Start()
    {
        pointsList.Capacity = 0;
        points.Capacity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointAndDjoint(float leng, GameObject point1, GameObject point2)//Функция получает длину отрезка, первую и последнюю точки
    {
        //соединяем полученую точку и первую ячейку
        GameObject creatBridge1 = (GameObject)Instantiate(creatBridge, new Vector2(point1.transform.position.x, point1.transform.position.y), Quaternion.identity);
        HingeJoint2D djoint = creatBridge1.AddComponent<HingeJoint2D>();
        //int num = (int)(leng / 0.26f);
        float rPoint = creatBridge1.GetComponent<CircleCollider2D>().radius;
        int num = (int)(leng / (2 * rPoint));
        Rigidbody2D rgPoint1 = point1.GetComponent<Rigidbody2D>();
        djoint.connectedBody = rgPoint1;
        DjointPar(djoint);
        GameObject[] creatBridgeAll = new GameObject[num - 1];
        HingeJoint2D[] djointAll = new HingeJoint2D[num - 1];
        Rigidbody2D[] rigidbody2D = new Rigidbody2D[num - 1];
        for (int i = 0; i < num - 1; i++)
        {
            if (i == 0)
            {
                //соединяем вторую ячейку с первой
                creatBridgeAll[0] = (GameObject)Instantiate(creatBridge, new Vector2(point1.transform.position.x + rPoint * (1 + i), point1.transform.position.y), Quaternion.identity);
                djointAll[0] = creatBridgeAll[0].AddComponent<HingeJoint2D>();
                rigidbody2D[0] = creatBridge1.GetComponent<Rigidbody2D>();
                djointAll[0].connectedBody = rigidbody2D[0];
                DjointPar(djointAll[0]);
            }
            else
            {
                //соединяем остальные ячейку между собой
                creatBridgeAll[i] = (GameObject)Instantiate(creatBridge, new Vector2(point1.transform.position.x + rPoint * (1 + i), point1.transform.position.y), Quaternion.identity);
                djointAll[i] = creatBridgeAll[i].AddComponent<HingeJoint2D>();
                rigidbody2D[i] = creatBridgeAll[i - 1].GetComponent<Rigidbody2D>();
                djointAll[i].connectedBody = rigidbody2D[i];
                DjointPar(djointAll[i]);
            }

        }
        //соединяем последнюю ячейку со второй полученной точкой
        HingeJoint2D djointLast = creatBridgeAll[num - 2].AddComponent<HingeJoint2D>();
        Rigidbody2D rigidbodyLast = point2.GetComponent<Rigidbody2D>();
        djointLast.connectedBody = rigidbodyLast;
        DjointPar(djointLast);

    }
    private void DjointPar(HingeJoint2D djoint)//Параметры джоинта
    {
        djoint.anchor = new Vector2(0, 0);
        djoint.connectedAnchor = new Vector2(0, 0);
    }
    private void CreatAll()
    {
        float leng;
        for (int i = 0; i < points.ToArray().Length; i++)
        {
            if (i + 1 != points.ToArray().Length)
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


    public void ClickToObj()
    {
        if (points.ToArray().Length < 2)
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointsList.Add(mousePos);
            points.Add((GameObject)Instantiate(pointSprite, mousePos, Quaternion.identity));
            crossing = false;  
          
        }
        if (points.ToArray().Length == 2)
        {
            CrossingOther(points[0].transform.position, points[1].transform.position);
        }   
    }

    void CrossingOther(Vector2 fPoint, Vector2 sPoint)
    {
        testInst=(GameObject)Instantiate(test, new Vector2(0,0), Quaternion.identity);
        CrossingTest testScr = testInst.GetComponent<CrossingTest>();
        testScr.Point = points[1];
        testScr.pointList = points;
        testScr.pointList1 = pointsList;
        EdgeCollider2D edge = testInst.gameObject.AddComponent<EdgeCollider2D>();
        edge.points = pointsList.ToArray();
        edge.isTrigger = true;
        Rigidbody2D fiz = testInst.gameObject.AddComponent<Rigidbody2D>();
        fiz.isKinematic = true;
        crossing = true;
        StartCoroutine("BuildDjoints");

    }

    IEnumerator BuildDjoints()
    {
        yield return new WaitForSeconds(1.1f);
        if (points[1]!=null) 
        {
            float leng;
            leng = Mathf.Sqrt(Mathf.Pow((points[0].transform.position.x - points[1].transform.position.x), 2) +
                    Mathf.Pow((points[0].transform.position.y - points[1].transform.position.y), 2));
            PointAndDjoint(leng, points[0], points[1]);
            Destroy(testInst);
            pointsList.RemoveRange(0, 2);
            points.RemoveRange(0, 2);
        }
        
        
    }
    
}
