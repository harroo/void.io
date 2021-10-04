
using UnityEngine;

using UnityEngine.UI;

public class StarRendererAssets : MonoBehaviour {

    public static StarRendererAssets instance;
    private void Awake () { instance = this; }

    public Material[] burstMaterials;

    public static Material Get (int index)
        => instance.burstMaterials[index];

    public static int RandomRange ()
        => Random.Range(0, instance.burstMaterials.Length - 1);

    public static int Range ()
        => instance.burstMaterials.Length;
}
