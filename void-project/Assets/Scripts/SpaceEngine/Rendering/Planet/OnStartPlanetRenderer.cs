
using UnityEngine;

public class OnStartPlanetRenderer : MonoBehaviour {

    public PlanetRenderer pRenderer;

    public float _size;

    private void Start () {

        pRenderer.Render(new PlanetRenderData{

            size = _size,

            baseID = PlanetRendererAssets.RandomBaseRange(),
            overlayID = PlanetRendererAssets.RandomOverlayRange(),

            baseColor = new Color(
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f
            ),
            overlayColor = new Color(
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f
            ),
        });
    }
}
