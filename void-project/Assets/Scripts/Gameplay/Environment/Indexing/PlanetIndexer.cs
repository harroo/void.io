
using UnityEngine;

public class PlanetIndexer : MonoBehaviour {

    private void Start () {

        PlanetIndex.Add(this);
    }
    private void OnDestroy () {

        PlanetIndex.Remove(this);
    }

    public Vector3 pos => transform.position;
    public float size => transform.Find("Base Renderer").localScale.x;
}
