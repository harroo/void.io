using UnityEngine;
public class RotateController : MonoBehaviour {
    public float rotateSpeedOnX;
    public float rotateSpeedOnY;
    public float rotateSpeedOnZ;
    private void Update () {
        transform.Rotate(
            rotateSpeedOnX * Time.deltaTime,
            rotateSpeedOnY * Time.deltaTime,
            rotateSpeedOnZ * Time.deltaTime
        );
    }
}
