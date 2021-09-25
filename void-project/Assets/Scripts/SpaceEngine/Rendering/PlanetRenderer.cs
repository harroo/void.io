
using UnityEngine;
using UnityEngine.UI;

public class PlanetRenderer : MonoBehaviour {

    public SpriteRenderer baseRenderer, overlayRenderer;

    public void Render (PlanetRenderData renderData) {

        baseRenderer.sprite = PlanetRendererAssets.GetBase(renderData.baseID);
        overlayRenderer.sprite = PlanetRendererAssets.GetOverlay(renderData.baseID);

        baseRenderer.color = renderData.baseColor;
        overlayRenderer.color = renderData.overlayColor;

        Vector3 scale = new Vector3(renderData.size, renderData.size, 1.0f);
        baseRenderer.transform.localScale = scale;
        overlayRenderer.transform.localScale = scale;
    }
}

public class PlanetRenderData {

    public float size;

    public int baseID, overlayID;

    public Color baseColor, overlayColor;
}
