using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeBody : MonoBehaviour {
    public SnakeBody parent;
    public Snake head;

	// Use this for initialization
	void Start () {
	}


	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateSnake() {
        //lastPos = parentPos;
        if (parent != null)
            transform.position = parent.getLastPos();
        else if (head != null)
            transform.position = head.lastPos;
    }

    public Vector3 getLastPos() {
        Vector3 curPos = transform.position;
        updateSnake();
        return curPos;
    }



    
}
