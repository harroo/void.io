
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour {

    public static ObjectManager instance;
    private void Awake () { instance = this; }

    public ObjectPrefab[] prefabs;
    public GameObject GetPrefab (int type)
        => Array.Find(prefabs, ctx => ctx.type == type).prefab;

    public void LoadData (byte[] data) {

        Console.Log("Loading from received buffer: " + data.Length);

        data = CompressionUtility.GZipCompression.Decompress(data);

        Console.Log("After Decompress: " + data.Length);

        int index = 0;

        while (index < data.Length) {

            int size = BitConverter.ToInt32(data, index);
            index += 4;

            int type = BitConverter.ToInt32(data, index);
            index += 4;

            int id = BitConverter.ToInt32(data, index);
            index += 4;

            float px = BitConverter.ToSingle(data, index); index += 4;
            float py = BitConverter.ToSingle(data, index); index += 4;
            float pz = BitConverter.ToSingle(data, index); index += 4;

            byte[] buf = new byte[size];
            Buffer.BlockCopy(data, index, buf, 0, size);
            index += size;

            LoadObject(id, type, buf, new Vector3(px, py, pz));
        }
    }

    private List<Object> objs = new List<Object>();

    public void LoadObject (int objID, int type, byte[] buf, Vector3 pos) {

        Object obj = Instantiate(GetPrefab(type), Vector3.zero, Quaternion.identity).GetComponent<Object>();
        obj.ID = objID;
        obj.transform.position = new Vector3(pos.x, pos.y, 0);
        obj.transform.eulerAngles = new Vector3(0, 0, pos.z);
        obj.Config(buf);
        objs.Add(obj);
    }

    public void CreateObject (int objID, int type) {

        Object obj = Instantiate(GetPrefab(type), Vector3.zero, Quaternion.identity).GetComponent<Object>();
        obj.ID = objID;
        objs.Add(obj);
    }

    public void CreateObject (int objID, int type, byte[] buf) {

        Object obj = Instantiate(GetPrefab(type), Vector3.zero, Quaternion.identity).GetComponent<Object>();
        obj.ID = objID;
        obj.Config(buf);
        objs.Add(obj);
    }

    public void CreateEffectObject (int objID, int type, byte[] buf) {

        Object obj = Instantiate(GetPrefab(type), Vector3.zero, Quaternion.identity).GetComponent<Object>();
        obj.ID = objID;
        obj.Config(buf);
    }

    public void DeleteObject (int objID) {

        Object obj = Array.Find(objs.ToArray(), ctx => ctx.ID == objID);
        Destroy(obj.gameObject);
        objs.Remove(obj);
    }

    public void UpdateObject (int objID, byte[] objData) {

        Object obj = Array.Find(objs.ToArray(), ctx => ctx.ID == objID);
        obj.Config(objData);
    }

    public void UpdateObjectPos (int objID, float x, float y, float rot) {

        Object obj = Array.Find(objs.ToArray(), ctx => ctx.ID == objID);
        obj.UpdatePos(x, y, rot);
    }


    public Object ById (int objID) {

        Object obj = null;
        try { obj = Array.Find(objs.ToArray(), ctx => ctx.ID == objID); } catch {}
        return obj;
    }

    public Object Closest (Object tobj) {

        Object closestObject = null;
        float closestDistance = Mathf.Infinity, distance = 0;

        foreach (var obj in objs) {

            if (obj.Type == "BULLET") continue;
            if (obj == tobj) continue;

            distance = (tobj.transform.position - obj.transform.position).magnitude;
            if (distance < closestDistance) {

                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }

    public Object ClosestInRange (Object tobj, float range) {

        Object closestObject = null;
        float closestDistance = Mathf.Infinity, distance = 0;

        foreach (var obj in objs) {

            if (obj.Type == "BULLET") continue;
            if (obj == tobj) continue;

            distance = (tobj.transform.position - obj.transform.position).magnitude;
            if (distance < closestDistance && distance < range) {

                closestDistance = distance;
                closestObject = obj;
            }
        }

        return closestObject;
    }


    public void Clear () {

        foreach (var obj in objs)
            Destroy(obj.gameObject);

        objs.Clear();
    }
}

[Serializable]
public class ObjectPrefab {

    public int type;
    public GameObject prefab;
}
