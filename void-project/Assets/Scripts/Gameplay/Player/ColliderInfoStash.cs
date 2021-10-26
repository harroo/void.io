
using UnityEngine;

using System;

public class ColliderInfoStash : MonoBehaviour {

    public static ColliderInfoStash instance;
    private void Awake () { instance = this; }

    public ColliderInfo[] infos;

    public static ColliderInfo Get (int id)
        => Array.Find(instance.infos, ctx => ctx.id == id);
}
