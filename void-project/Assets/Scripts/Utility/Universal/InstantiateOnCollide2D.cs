
using UnityEngine;

public class InstantiateOnCollide2D : MonoBehaviour {

    public string[] killingTags;

    public GameObject objectToSpawnOnDestroy;

    private void OnCollisionEnter2D (Collision2D collision) {

        bool damageTaken = false;

        if (killingTags.Length == 0) damageTaken = true;
        else if (System.Array.IndexOf(killingTags, collision.collider.tag) > -1)
            damageTaken = true;

        if (damageTaken) {

            if (objectToSpawnOnDestroy != null)
                Instantiate(objectToSpawnOnDestroy, transform.position, transform.rotation);
        }
    }
}
