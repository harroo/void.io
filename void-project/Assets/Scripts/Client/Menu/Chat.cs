
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class Chat : MonoBehaviour {

    private List<string> chatList = new List<string>();

    public static Chat instance;
    private void Awake () { instance = this; }

    public GameObject overlay;
    public Text field, notif;

    public int messageCap = 32;

    public void AddMessage (string msg) {

        chatList.Add(msg);

        Render();

        if (!overlay.activeSelf) {

            notif.text = (int.Parse(notif.text != "" ? notif.text : "0") + 1).ToString();
        }
    }

    private void Render () {

        field.text = "";

        while (chatList.Count > messageCap)
            chatList.RemoveAt(0);

        for (int i = 0; i < chatList.Count; ++i) {

            field.text += chatList[i] + "\n";
        }
    }

    public void SendFieldSubmit (InputField field) {

        if (field.text == "") return;

		if (field.text.Length > 1 && field.text[0] == 'd') {

            try {

    			string diceS = field.text.Substring(1);
    			int diceD = int.Parse(diceS);

    			if (diceD < 2) {

    				AddMessage("<color=red>ERROR! Invalid input detected..</color>");

    				return;
    			}

    			MainClient.SendChatMessage(username + " is rolling a <color=white><b>D" + diceS + "</b></color>...");

                max = diceD;
                Invoke("RollDice", (new System.Random().Next(1000, 3000) / 1000.0f));

            } catch {

                AddMessage("<color=red>ERROR! Invalid input detected..</color>");
            }

        } else MainClient.SendChatMessage(username + " " + field.text);

        field.text = "";
    }

    private static int max;
    private void RollDice () {

        int roll = new System.Random().Next(1, max + 1);

        //ik this line is rlly long and not neat, i just kept addin to it and then decided it was rlly funny so i left it, this coment rlly long to hahahah
        MainClient.SendChatMessage(username + " rolled: <color=#" + ColorUtility.ToHtmlStringRGB(Color.Lerp(Color.red, Color.green, (float)((float)roll / (float)max))).ToLower() + "ff><b>" + roll.ToString() + "</b></color>");
    }

    private string username {

        get {

            if (System.IO.File.Exists("name.conf")) {

                string name = System.IO.File.ReadAllText("name.conf");

                if (name.Length < 24) return "[<color=#00A9A5ff>" + name + "</color>]";
            }

            return "[<color=#00A9A5ff>" + System.Environment.UserName + "</color>@<color=#00A9A5ff>" + System.Environment.MachineName + "</color>]";
        }
    }

    public static void Send (string msg) {

        MainClient.SendChatMessage(instance.username + " " + msg);
    }
}
