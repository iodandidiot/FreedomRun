using UnityEngine;
using System.Collections;

public class clickForDjoint : MonoBehaviour {

    public CreatHingeDjoint hinge;

    void OnMouseDown() 
    {
        hinge.ClickToObj();
    } 
}
