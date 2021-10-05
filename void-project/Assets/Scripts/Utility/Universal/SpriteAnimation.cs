
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

            if (index == frames.Length) {

                if (loop) index = 0;
                else if (destroyOnFinish) Destroy(gameObject);

                timer = 0;
                return;
            }

            image.sprite = frames[index++];

        } else timer -= Time.deltaTime;
    }
}
