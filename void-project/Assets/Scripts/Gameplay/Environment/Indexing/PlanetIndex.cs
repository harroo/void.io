
using System.Collections.Generic;

using UnityEngine;

public static class PlanetIndex {

    private static List<PlanetIndexer> planets = new List<PlanetIndexer>();

    public static void Add (PlanetIndexer planet) {

        planets.Add(planet);
    }
    public static void Remove (PlanetIndexer planet) {

        planets.Remove(planet);
    }

    public static bool CheckIfInRange (Vector3 pos, float range) {

        foreach (var planet in planets) {

            if ((pos - planet.pos).magnitude < (planet.size/2) + range) {

                return true;
            }
        }

        return false;
    }
}
