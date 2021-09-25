
using UnityEngine;

using UnityEngine.UI;

public class PlanetRendererAssets : MonoBehaviour {

    public static PlanetRendererAssets instance;
    private void Awake () { instance = this; }

    public Sprite[] baseTextures, overlayTextures;

    public static Sprite GetBase (int index)
        => instance.baseTextures[index];

    public static Sprite GetOverlay (int index)
        => instance.overlayTextures[index];

    public static int RandomBaseRange ()
        => Random.Range(0, instance.baseTextures.Length - 1);

    public static int RandomOverlayRange ()
        => Random.Range(0, instance.overlayTextures.Length - 1);
}
