using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Snake : MonoBehaviour {
    public static Snake instance;

    public Vector3 movementDirection;
    public Transform snakeTransform;

    public List<SnakeBody> snakeBody;
    public Vector3 lastPos;

    //debugging only
    public bool addOne;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Use this for initialization
    void Start() {
        snakeTransform = transform;
    }



    // Update is called once per frame
    void Update() {
        if (addOne) {
            grow();
            addOne = false;
        }
    }

    public void updateSnake() {
        lastPos = transform.position;

        movementDirection = GetMoveDirection(MovementController.instance.nextMove);
        transform.position += movementDirection;
        //round
        movementDirection = new Vector3(Mathf.Round(movementDirection.x), Mathf.Round(movementDirection.y), Mathf.Round(movementDirection.z));

        /*
        //all other pieces, transform from the parent.
        //for (var i = 1; i < snakeBody.Count; i++)
        for (var i = snakeBody.Count - 1; i >= 1; i--)
            snakeBody[i].updateSnake(snakeBody[i - 1].getLastPos());

        //if theres at least one body piece, update the transform with the head.
        if (snakeBody.Count > 0)
            snakeBody[0].updateSnake(lastPos);*/
        if (snakeBody.Count > 0)
            snakeBody[snakeBody.Count - 1].updateSnake();

    }

    public void reallignSnake() {
        var pos = transform.position;
        transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
        transform.eulerAngles = new Vector3(0, 0, 0);

        for (var i = snakeBody.Count - 1; i >= 0; i--) {
            pos = snakeBody[i].transform.position;
            snakeBody[i].transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
            snakeBody[i].transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void grow() {
        GameObject bodySection = (GameObject)Instantiate(Resources.Load("SnakeBody"));
        SnakeBody script = bodySection.GetComponent<SnakeBody>();
        bodySection.transform.parent = gameObject.transform.parent;
        if (snakeBody.Count == 0) {
            script.parent = null;
            script.head = this;
            bodySection.transform.position = transform.position + Vector3.back;
        } else {
            script.head = null;
            script.parent = snakeBody[snakeBody.Count - 1];
            bodySection.transform.position = snakeBody[snakeBody.Count - 1].transform.position + Vector3.back;
        }
        bodySection.transform.eulerAngles = new Vector3(0, 0, 0);
        snakeBody.Add(script);
    }

    public bool pointOnSnake(Vector3 createPoint) {
        for (var i = snakeBody.Count - 1; i >= 0; i--) {
            if (pointInPoint(snakeBody[i].transform.position, createPoint)) {
                return true;
            }
        }
        return false;
    }

    public bool canEat(Vector3 createPoint) {
        return (pointInPoint(transform.position, createPoint));
    }

    private bool pointInPoint(Vector3 p1, Vector3 p2) {
        return (Mathf.Abs(p1.x - p2.x) < .1 && Mathf.Abs(p1.y - p2.y) < .1 && Mathf.Abs(p1.z - p2.z) < .1);
    }

    public Vector3 GetMoveDirection(MovementController.RotateDirection dir) {
        switch (dir) {
            case MovementController.RotateDirection.UP:
                return Vector3.up;
            case MovementController.RotateDirection.DOWN:
                return -Vector3.up;
            case MovementController.RotateDirection.LEFT:
                return -Vector3.right;
            case MovementController.RotateDirection.RIGHT:
                return Vector3.right;
            default://should hit if dir == NONE
                return Vector3.forward;
        }
    }

    public bool outsideOfWorld() {
        var outside = GameManager.instance.distanceFromWorldCenter + .9;
        if (transform.position.x > outside || transform.position.x < -outside)
            return true;
        if (transform.position.y > outside || transform.position.y < -outside)
            return true;
        if (transform.position.z > outside || transform.position.z < -outside)
            return true;
        return false;
    }



}
