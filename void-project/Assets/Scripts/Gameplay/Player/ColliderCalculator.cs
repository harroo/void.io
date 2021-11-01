
using System;

using UnityEngine;

public class ColliderCalculator : MonoBehaviour {

    public BoxCollider2D boxCollider;
    public CircleCollider2D circleCollider;

    public void Render (int id) {

        ColliderData cinfo = ColliderIndex.Get(id);
        if (cinfo == null) return;

        boxCollider.enabled = !cinfo.isCircle;
        circleCollider.enabled = cinfo.isCircle;

        boxCollider.size = new Vector2(cinfo.sizeX, cinfo.sizeY);
        circleCollider.radius = cinfo.radius;
    }
}
