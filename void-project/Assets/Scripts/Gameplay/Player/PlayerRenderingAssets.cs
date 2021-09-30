
using UnityEngine;

using UnityEngine.UI;

public class PlayerRenderingAssets : MonoBehaviour {

    public static PlayerRenderingAssets instance;
    private void Awake () { instance = this; }

    public Sprite[] textures;

    public static Sprite Get (int index)
        => instance.textures[index];

    public static int Index (Sprite sprite)
        => System.Array.IndexOf(instance.textures, sprite);

    public static int RandomRange ()
        => Random.Range(0, instance.textures.Length - 1);
}
