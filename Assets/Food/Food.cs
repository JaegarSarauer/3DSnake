using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Snake.instance.canEat(transform.position)) {
            Debug.Log("Ate At: " + transform.position + " and snake at: " + Snake.instance.transform.position);
            GameManager.instance.addScore();
            Snake.instance.grow();
            GameManager.instance.updateInterval -= .02f;
            Destroy(gameObject);
        }

    }
}
