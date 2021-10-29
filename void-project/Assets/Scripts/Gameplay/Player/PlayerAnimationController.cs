
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    public SpriteRenderer sRenderer;

    public AnimationSetInfo[] sets;

    private AnimationSetInfo setCache;

    private void Start () { setCache = sets[0]; }

    private PlayerAnimationSet currentSet () {

        if (setCache.id != PlayerStats.shipID)
            setCache = System.Array.Find(sets, s => s.id == PlayerStats.shipID);

        return setCache.set;
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
public class AnimationSetInfo {

    public string name;

    public int id;

    public PlayerAnimationSet set;
}

[System.Serializable]
public class PlayerAnimationSet {

    public Sprite idle, forward, left, right;
}
