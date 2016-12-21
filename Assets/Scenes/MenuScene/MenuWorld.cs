using UnityEngine;
using System.Collections;

public class MenuWorld : MonoBehaviour {
    private Vector3 startPos;
    
	void Start () {

    }
	
	void Update () {
        transform.Rotate(0, .3f, .3f);
	}
}
