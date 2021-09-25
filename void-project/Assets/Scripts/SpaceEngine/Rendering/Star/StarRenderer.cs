
using UnityEngine;

public class StarRenderer : MonoBehaviour {

    public SpriteRenderer flareRenderer, shapeRenderer;
    public ParticleSystem particleRenderer, surfaceRenderer;

    public void Render (StarRenderData renderData) {

        particleRenderer.gameObject.GetComponent<ParticleSystemRenderer>().material
            = StarRendererAssets.Get(renderData.burstID);

        flareRenderer.color = renderData.color;
        shapeRenderer.color = renderData.color;
        ParticleSystem.MainModule msettings = particleRenderer.main;
        msettings.startColor = renderData.color;
        ParticleSystem.MainModule ssettings = surfaceRenderer.main;
        ssettings.startColor = renderData.color;

        Vector3 scale = new Vector3(renderData.size, renderData.size, 1.0f);
        flareRenderer.transform.localScale = scale;
        shapeRenderer.transform.localScale = scale;
        particleRenderer.transform.localScale = scale;
        surfaceRenderer.transform.localScale = scale;
    }
}

public class StarRenderData {

    public float size;

    public int burstID;

    public Color color;
}
