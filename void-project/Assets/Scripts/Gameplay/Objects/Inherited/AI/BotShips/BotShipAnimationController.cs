
using UnityEngine;

public class BotShipAnimationController : MonoBehaviour {

    public BotShip ship;

    public SpriteRenderer sRenderer;

    public BotShipAnimationSet aset;

    public void SetIdle () { Set(aset.idle); }
    public void SetForward () { Set(aset.forward); }
    public void SetLeft () { Set(aset.left); }
    public void SetRight () { Set(aset.right); }

    private void Set (int index) {

        sRenderer.sprite = PlayerRenderingAssets.Get(index);

        if (GlobalValues.Hosting)
            UdpStream.Send_ObjectUpdate(ship.ID, System.BitConverter.GetBytes(index));
    }

    public void UpdateSprite (int index) {

        sRenderer.sprite = PlayerRenderingAssets.Get(index);
    }
}

[System.Serializable]
public class BotShipAnimationSet {

    public int idle, forward, left, right;
}
