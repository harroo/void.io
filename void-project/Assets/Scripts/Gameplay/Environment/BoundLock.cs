
using UnityEngine;

public class BoundLock : MonoBehaviour {

    public int bounds = 64;

    private void Update () {

        if (transform.position.x > bounds) transform.position = new Vector3(bounds, transform.position.y, transform.position.z);
        if (transform.position.x < -bounds) transform.position = new Vector3(-bounds, transform.position.y, transform.position.z);

        if (transform.position.y > bounds) transform.position = new Vector3(transform.position.x, bounds, transform.position.z);
        if (transform.position.y < -bounds) transform.position = new Vector3(transform.position.x, -bounds, transform.position.z);
    }
}
