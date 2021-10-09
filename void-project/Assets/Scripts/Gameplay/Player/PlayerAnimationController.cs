
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    public SpriteRenderer sRenderer;

    public PlayerAnimationSet s1_set;

    private PlayerAnimationSet currentSet () {

        switch (PlayerStats.shipID) {

            case 1: default: return s1_set;
        }
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

        PlayerStats.SetTextureIndex(PlayerRenderingAssets.Index(sprite));
    }
}

[System.Serializable]
public class PlayerAnimationSet {

    public Sprite idle, forward, left, right;
}
