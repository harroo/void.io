
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

        Debug.Log("Loading from received buffer: " + data.Length);

        int index = 0;

        while (index < data.Length) {

            int size = BitConverter.ToInt32(data, index);
            index += 4;

            int type = BitConverter.ToInt32(data, index);
            index += 4;

            int id = BitConverter.ToInt32(data, index);
            index += 4;

            byte[] buf = new byte[size];
            Buffer.BlockCopy(data, index, buf, 0, size);
            index += size;

            LoadObject(type, buf);
        }
    }

    private List<Object> objs = new List<Object>();

    public void LoadObject (int type, byte[] buf) {

        Object obj = Instantiate(GetPrefab(type), Vector3.zero, Quaternion.identity).GetComponent<Object>();
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
