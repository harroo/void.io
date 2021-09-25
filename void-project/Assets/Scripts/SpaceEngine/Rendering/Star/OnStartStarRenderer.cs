
using UnityEngine;

public class OnStartStarRenderer : MonoBehaviour {

    public StarRenderer sRenderer;

    public float _size;

    private void Start () {

        sRenderer.Render(new StarRenderData{

            size = _size,

            burstID = StarRendererAssets.RandomRange(),

            color = new Color(
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f
            ),
        });
    }
}
