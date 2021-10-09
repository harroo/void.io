
using UnityEngine;

public class StarIndexer : MonoBehaviour {

    private void Start () {

        StarIndex.Add(this);
    }
    private void OnDestroy () {

        StarIndex.Remove(this);
    }

    public Vector3 pos => transform.position;
    public float size => transform.Find("Shape").localScale.x;
}
