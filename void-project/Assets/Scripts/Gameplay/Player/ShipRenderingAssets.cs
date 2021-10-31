
using UnityEngine;

using UnityEngine.UI;

public class ShipRenderingAssets : MonoBehaviour {

    public static ShipRenderingAssets instance;
    private void Awake () { instance = this; }

    public ShipSpriteSet[] sets;

    public static ShipSpriteSet Get (int index)
        => instance.sets[index];

    public static int Index (ShipSpriteSet set)
        => System.Array.IndexOf(instance.sets, set);

    public static int RandomRange ()
        => Random.Range(0, instance.sets.Length - 1);

    public static Sprite GetSprite (int index) {

        int counter = 0;
        foreach (var set in instance.sets) {

            if (counter == index) return set.idle; else counter++;
            if (counter == index) return set.forward; else counter++;
            if (counter == index) return set.left; else counter++;
            if (counter == index) return set.right; else counter++;
        }
        return null;
    }
    public static int GetIndex (Sprite sprite) {

        int counter = 0;
        foreach (var set in instance.sets) {

            if (sprite == set.idle) return counter; else counter++;
            if (sprite == set.forward) return counter; else counter++;
            if (sprite == set.left) return counter; else counter++;
            if (sprite == set.right) return counter; else counter++;
        }
        return -1;
    }
    public static int RandomIndexRange ()
        => Random.Range(0, (instance.sets.Length * 4) - 1);
}

[System.Serializable]
public class ShipSpriteSet {

    public Sprite idle, forward, left, right;

    // public string name;
}
