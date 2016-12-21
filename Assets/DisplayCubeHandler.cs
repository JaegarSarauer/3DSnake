using UnityEngine;
using System.Collections;

public class DisplayCubeHandler : MonoBehaviour {
    Vector3 moveTo;

    // Use this for initialization
    void Start () {
        moveTo = newMoveTo();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime);
        if (pointInPoint(transform.position, moveTo))
            moveTo = newMoveTo();
    }

    private Vector3 newMoveTo() {
        return new Vector3(Camera.main.transform.position.x + Random.Range(-5, 5), Camera.main.transform.position.y + Random.Range(-5, 5), Camera.main.transform.position.z + Random.Range(-5, 5));
    }
    

    private bool pointInPoint(Vector3 p1, Vector3 p2) {
        return (Mathf.Abs(p1.x - p2.x) < .1 && Mathf.Abs(p1.y - p2.y) < .1 && Mathf.Abs(p1.z - p2.z) < .1);
    }
}
