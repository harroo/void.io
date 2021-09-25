
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour {

    public static ObjectManager instance;
    private void Awake () { instance = this; }

    public GameObject prefab;
    public Transform parent;

    public void LoadData (byte[] data) {

        Debug.Log("Loading from received buffer: " + data.Length);

        int index = 0;

        while (index < data.Length) {

            int size = BitConverter.ToInt32(data, index);
            index += 4;

            byte[] buf = new byte[size];
            Buffer.BlockCopy(data, index, buf, 0, size);
            index += size;

            LoadObject(buf);
        }
    }

    private List<Object> cards = new List<Object>();

    public void LoadObject (byte[] buf) {

        Object card = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Object>();
        card.transform.SetParent(parent);
        card.transform.localScale = new Vector3(1,1,1);
        card.transform.position = Vector3.zero;
        card.Config(buf);
        cards.Add(card);
    }

    public void CreateObject (int cardID) {

        Object card = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Object>();
        card.transform.SetParent(parent);
        card.transform.localScale = new Vector3(1,1,1);
        card.transform.position = Input.mousePosition;
        card.ID = cardID;
        cards.Add(card);
    }

    public void DeleteObject (int cardID) {

        Object card = Array.Find(cards.ToArray(), ctx => ctx.ID == cardID);
        Destroy(card.gameObject);
        cards.Remove(card);
    }

    public void UpdateObject (int cardID, byte[] cardData) {

        Object card = Array.Find(cards.ToArray(), ctx => ctx.ID == cardID);
        card.Config(cardData);

        card.transform.SetSiblingIndex(card.transform.parent.childCount - 1);
    }

    public void Clear () {

        foreach (var card in cards)
            Destroy(card.gameObject);

        cards.Clear();
    }
}
