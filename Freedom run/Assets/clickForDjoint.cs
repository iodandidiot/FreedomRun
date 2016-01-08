using UnityEngine;
using System.Collections;

public class clickForDjoint : MonoBehaviour {

    GameObject djoints;

    void OnMouseDown() 
    {
        djoints = GameObject.Find("HingeDjoint");
        CreatHingeDjoint createDjoints = djoints.GetComponent<CreatHingeDjoint>();

        createDjoints.ClickToObj();
    } 
}
