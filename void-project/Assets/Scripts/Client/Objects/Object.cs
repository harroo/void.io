
using System;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public InputField title, field;

    public GameObject maxButton, minButton, pane;

    public bool minimized {

        get {
            return pane.activeSelf;
        }
        set {
            pane.SetActive(value);
            minButton.SetActive(pane.activeSelf);
            maxButton.SetActive(!pane.activeSelf);
        }
    }

    [Space()]
    public int ID;

    public void Config (byte[] buf) {

        Vector2 cardPos = new Vector2(
            BitConverter.ToSingle(buf, 0),
            BitConverter.ToSingle(buf, 4)
        );
        cardPos.x *= Camera.main.pixelWidth;
        cardPos.y *= Camera.main.pixelHeight;
        transform.position = Camera.main.ViewportToWorldPoint(cardPos);

        ID = BitConverter.ToInt32(buf, 8);

        int titleSize = BitConverter.ToInt32(buf, 12);
        int fieldSize = BitConverter.ToInt32(buf, 16);

        byte[] titleBuf = new byte[titleSize];
        Buffer.BlockCopy(buf, 20, titleBuf, 0, titleSize);

        byte[] fieldBuf = new byte[fieldSize];
        Buffer.BlockCopy(buf, 20 + titleSize, fieldBuf, 0, fieldSize);

        title.text = Encoding.ASCII.GetString(titleBuf);
        field.text = Encoding.ASCII.GetString(fieldBuf);

        minimized = buf[titleSize + fieldSize + 20] == 1;

        AnchorCache();
    }

    public byte[] GetData () {

        Vector2 cardPos = Camera.main.WorldToViewportPoint(transform.position);
        cardPos.x /= Camera.main.pixelWidth;
        cardPos.y /= Camera.main.pixelHeight;

        byte[] titleData = Encoding.ASCII.GetBytes(title.text);
        byte[] fieldData = Encoding.ASCII.GetBytes(field.text);

        byte[] data = new byte[titleData.Length + fieldData.Length + 25];

        Buffer.BlockCopy(BitConverter.GetBytes(cardPos.x), 0, data, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(cardPos.y), 0, data, 4, 4);

        Buffer.BlockCopy(BitConverter.GetBytes(ID), 0, data, 8, 4);

        Buffer.BlockCopy(BitConverter.GetBytes(titleData.Length), 0, data, 12, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(fieldData.Length), 0, data, 16, 4);

        Buffer.BlockCopy(titleData, 0, data, 20, titleData.Length);
        Buffer.BlockCopy(fieldData, 0, data, 20 + titleData.Length, fieldData.Length);

        data[titleData.Length + fieldData.Length + 20] = (byte)(minimized ? 1 : 0);

        return data;
    }

    private string contentCache, titleCache;
    private Vector3 posCache;
    private bool minCache;
    private float timer;
    private void Update () {

        if (!MainClient.connected) return;

        if (timer < 0) { timer = 0.25f;

            if (
                contentCache != field.text ||
                titleCache != title.text ||
                posCache != transform.position ||
                minCache != minimized
            ) {

                MainClient.SendCardUpdate(ID, GetData());

                AnchorCache();
            }

        } else timer -= Time.deltaTime;
    }

    public void AnchorCache () {

        contentCache = field.text;
        titleCache = title.text;
        posCache = transform.position;
        minCache = minimized;
    }

    public void DeleteThisCard () {

        MainClient.SendCardDelete(ID);
    }
}
