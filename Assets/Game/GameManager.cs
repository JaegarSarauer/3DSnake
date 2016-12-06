﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameObject snakeObject;
    public Snake snake;

    private float _updateInterval = .5f;
    public float updateInterval {
        get {
            return _updateInterval;
        }
        set {
            _updateInterval = value;
            if (_updateInterval < .2f)
                _updateInterval = .2f;
        }
    }
    private float timePassed;
    private bool died = false;

    public int distanceFromWorldCenter = 8;

    public int score = 0;

    public Text scoreText;

    public void addScore() {
        score++;
        scoreText.text = score.ToString();
    }

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

	// Use this for initialization
	void Start () {
        timePassed = 0;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        timePassed += Time.deltaTime;
        if (checkUpdate() && !died) {
            gameUpdate();
        }
	}

    void gameUpdate() {
        timePassed = 0;
        checkWin();
        createFood();
        MovementController.instance.worldUpdate();
        Snake.instance.updateSnake();
        MovementController.instance.nextMove = MovementController.RotateDirection.NONE;
        checkDeath();
    }

    public void checkDeath() {
        if (Snake.instance.pointOnSnake(Snake.instance.transform.position) || Snake.instance.outsideOfWorld()) {
            SceneManager.instance.endGame(false);
            died = true;
        }

    }

    bool checkUpdate() {
        return (timePassed >= updateInterval);
    }

    public Vector3 GetRotateDirection(MovementController.RotateDirection dir) {
        var curAngle = new Vector3(0,0,0);
        switch (dir) {
            case MovementController.RotateDirection.UP:
                curAngle.x = 90 + curAngle.x;
                break;
            case MovementController.RotateDirection.DOWN:
                curAngle.x = -90 + curAngle.x;
                break;
            case MovementController.RotateDirection.LEFT:
                curAngle.y += 90;
                break;
            case MovementController.RotateDirection.RIGHT:
                curAngle.y -= 90;
                break;
            default://should hit if dir == NONE
                break;
        }
        return curAngle;
    }

    public void checkWin() {
        var maxSnakeSize = Mathf.Pow(distanceFromWorldCenter, 3) - 1;
        if (Snake.instance.snakeBody.Count >= maxSnakeSize)
            SceneManager.instance.endGame(true);
    }

    public void createFood() {
        if (GameObject.FindGameObjectWithTag("Food") != null)
            return;
        var tries = 10;
        Vector3 createPoint = new Vector3(0,0,0);
        while (tries-- >= 0) {
            createPoint = new Vector3(Mathf.RoundToInt(Random.Range(-distanceFromWorldCenter, distanceFromWorldCenter)), Mathf.RoundToInt(Random.Range(-distanceFromWorldCenter, distanceFromWorldCenter)), Mathf.RoundToInt(Random.Range(-distanceFromWorldCenter, distanceFromWorldCenter)));
            if (!Snake.instance.pointOnSnake(createPoint) || Snake.instance.canEat(createPoint))
                break;
        }
        if (tries <= 0) {
            return;
        }
        GameObject bodySection = (GameObject)Instantiate(Resources.Load("Food"));
        bodySection.transform.position = createPoint;
        bodySection.transform.parent = MovementController.instance.transform;
    }
}