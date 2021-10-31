
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    public SpriteRenderer sRenderer;

    private int idCache;

    private void Start () { idCache = -1; }

    private ShipSpriteSet currentSet () {

        if (idCache != PlayerStats.shipID)
            idCache = PlayerStats.shipID;

        return ShipRenderingAssets.Get(ShipIndex.Get(idCache).setId);
    }

    private bool W, A, D;

    private void Update () {

        W = Input.GetKey(KeyCode.W);
        A = Input.GetKey(KeyCode.A);
        D = Input.GetKey(KeyCode.D);

        if (W) Set(currentSet().forward);
        if (A) Set(currentSet().left);
        if (D) Set(currentSet().right);

        if (!W && !A && !D) Set(currentSet().idle);
    }

    private void Set (Sprite sprite) {

        sRenderer.sprite = sprite;

        PlayerStats.SetTextureIndex(ShipRenderingAssets.GetIndex(sprite));
    }
}
