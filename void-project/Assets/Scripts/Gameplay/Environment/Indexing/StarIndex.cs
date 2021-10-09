
using System.Collections.Generic;

using UnityEngine;

public static class StarIndex {

    private static List<StarIndexer> stars = new List<StarIndexer>();

    public static void Add (StarIndexer star) {

        stars.Add(star);
    }
    public static void Remove (StarIndexer star) {

        stars.Remove(star);
    }

    public static bool CheckIfInRange (Vector3 pos, float range) {

        foreach (var star in stars) {

            if ((pos - star.pos).magnitude < star.size + range) {

                return true;
            }
        }

        return false;
    }
}
