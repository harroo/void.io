
using UnityEngine;

public class NebulaRenderer : MonoBehaviour {

    public void Render (NebulaRenderData renderData) {

        var ps = GetComponent<ParticleSystem>();

        ps.GetComponent<Renderer>().sortingLayerName = "Foreground";

        ParticleSystem.MainModule settings = ps.main;
        settings.startColor = renderData.color;

        Vector3 scale = new Vector3(renderData.size, renderData.size, 1.0f);
        transform.localScale = scale;
    }
}

public class NebulaRenderData {

    public float size;

    public Color color;
}
