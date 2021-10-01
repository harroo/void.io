using UnityEngine;
public class Despawn : MonoBehaviour {
    public float aliveTime;
    private void Update () {
        if (aliveTime < 0) Destroy(gameObject);
        else aliveTime -= 1 * Time.deltaTime;
    }
}
