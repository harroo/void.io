
using UnityEngine;

public class OnStartNebulaRenderer : MonoBehaviour {

    public NebulaRenderer sRenderer;

    public float _size;

    private void Start () {

        sRenderer.Render(new NebulaRenderData{

            size = _size,

            color = new Color(
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f
            ),
        });
    }
}
