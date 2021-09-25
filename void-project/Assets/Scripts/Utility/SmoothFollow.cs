
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    public float lerpSpeed;

    public Transform target;

    public Vector3 offset;

    private void LateUpdate () {

        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpSpeed * Time.deltaTime);
    }
}
