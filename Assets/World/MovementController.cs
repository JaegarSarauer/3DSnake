using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class MovementController : MonoBehaviour {
    public static MovementController instance;

    public enum RotateDirection { UP, LEFT, DOWN, RIGHT, NONE}

    public RotateDirection nextMove;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

	// Use this for initialization
	void Start () {
        nextMove = RotateDirection.NONE;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
            HUDButtonHandler.instance.togglePause();

        if (GameManager.instance.isPaused)
            return;
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            nextMove = RotateDirection.UP;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            nextMove = RotateDirection.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            nextMove = RotateDirection.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            nextMove = RotateDirection.RIGHT;
        }
    }

    public void worldUpdate() {
        if (nextMove != RotateDirection.NONE) {
            SoundManager.instance.playSound(SoundManager.SFXID.WORLD_SHIFT);
            StartCoroutine(RotateWorld(GameManager.instance.GetRotateDirection(nextMove)));
        }
    }

    public IEnumerator RotateWorld(Vector3 rot) {
        var fromAngle = transform.eulerAngles;
        var t = 0f;
        while (t <= 1) {
            if (!GameManager.instance.isPaused) {
                var thisT = (Time.deltaTime / GameManager.instance.updateInterval);
                t += thisT;
                transform.RotateAround(transform.position, rot.normalized, 90 * thisT);
            }
            yield return null;
        }
        Vector3 angle = transform.eulerAngles;
        angle.x = Mathf.Round(angle.x / 90) * 90;
        angle.y = Mathf.Round(angle.y / 90) * 90;
        angle.z = Mathf.Round(angle.z / 90) * 90;
        transform.eulerAngles = angle;
        Snake.instance.reallignSnake();
        yield return null;
    }
}
