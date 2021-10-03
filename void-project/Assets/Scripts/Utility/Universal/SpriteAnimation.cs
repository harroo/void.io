
using UnityEngine;

public class SpriteAnimation : MonoBehaviour {

    public bool loop;
    public bool destroyOnFinish;
    public float frameDelay;
    public Sprite[] frames;

    private float timer;
    private int index;
    private SpriteRenderer image;

    private void Start () {

        image = GetComponent<SpriteRenderer>();
    }

    private void Update () {

        if (timer < 0) { timer = frameDelay;

            image.sprite = frames[index++];

            if (index == frames.Length) {

                if (loop) index = 0;
                else if (destroyOnFinish) Destroy(gameObject);
            }

        } else timer -= Time.deltaTime;
    }
}
