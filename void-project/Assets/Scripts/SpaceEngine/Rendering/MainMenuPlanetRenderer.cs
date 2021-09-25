
using UnityEngine;

public class MainMenuPlanetRenderer : MonoBehaviour {

    public PlanetRenderer pRenderer;

    private void Start () {

        pRenderer.Render(new PlanetRenderData{

            size = 1.6f,

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
