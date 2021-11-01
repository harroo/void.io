
using System;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

public static class ColliderIndex {

    [RuntimeInitializeOnLoadMethod]
    public static void InitializeColliderData () {

        List<ColliderData> colliderList = new List<ColliderData>();

        colliderList.Add(new ColliderData());

        foreach (Type type in Assembly.GetAssembly(typeof(ColliderData)).GetTypes()) {

			if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ColliderData))) {

                colliderList.Add((ColliderData)Activator.CreateInstance(type));
            }
        }

        colliderDataBuffer = colliderList.ToArray();
    }

    public static ColliderData Get (int colliderId) {

        if (colliderData.ContainsKey(colliderId))
            return colliderData[colliderId];
        else
            return new ColliderData();
    }

    public static ColliderData[] colliderDataBuffer;

    public static void InitializeColliders () {

        if (colliderData.Count != 0) return;

        for (int i = 0; i < colliderDataBuffer.Length; ++i) {

            if (colliderDataBuffer[i].colliderId == -1) continue;

            colliderData.Add(colliderDataBuffer[i].colliderId, colliderDataBuffer[i]);
        }

        colliderDataBuffer = new ColliderData[0];
    }

    public static Dictionary<int, ColliderData> colliderData
        = new Dictionary<int, ColliderData>();
}
