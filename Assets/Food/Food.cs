using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void PreUpdate() {

    }
	
	// Update is called once per frame
	void Update () {
        if (Snake.instance.canEat(transform.position)) {
            SoundManager.instance.playSound(SoundManager.SFXID.EAT);
            GameManager.instance.addScore();
            Snake.instance.grow();
            GameManager.instance.updateInterval -= .02f;
            Destroy(gameObject);
        }

    }
    
    public void reallign() {
        Vector3 curPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(curPos.x), Mathf.RoundToInt(curPos.y), Mathf.RoundToInt(curPos.z));
    }
}
