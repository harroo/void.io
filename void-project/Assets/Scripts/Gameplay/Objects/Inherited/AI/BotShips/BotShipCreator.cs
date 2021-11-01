
using UnityEngine;

using System;
using System.Collections.Generic;

public class BotShipCreator : MonoBehaviour {

    public static BotShipCreator instance;
    private void Awake () { instance = this; }

    public int max;

    private float timer;

    private void Update () {

        if (timer < 0) { timer = 1.0f;

            if (asos.Count >= max) return;

            BotShipSender.CreateBotShip(
                UnityEngine.Random.Range(-60, 60),
                UnityEngine.Random.Range(-60, 60),
                GetRandomShipData()
            );

        } else timer -= Time.deltaTime;
    }

    private ShipData GetRandomShipData () {

        while (true) {

            int index = 0, rng = UnityEngine.Random.Range(0, ShipIndex.shipData.Count);
            foreach (var shipData in ShipIndex.shipData.Values) {

                if (shipData.forceNoBot) break;

                if (index == rng) return shipData;
                index++;
            }
        }
    }

    private List<Object> asos = new List<Object>();

    public static void Add (Object aso) { if (instance != null) instance.asos.Add(aso); }
    public static void Remove (Object aso) { if (instance != null) instance.asos.Remove(aso); }
}

public static class BotShipSender {

    public static void CreateBotShip (float x, float y, ShipData sd) {

        CreateBotShip(
            x, y, sd.forwardForceBot, sd.turnForceBot,
            new BotShipAnimationSet{
                idle=ShipRenderingAssets.GetIndex(ShipRenderingAssets.Get(sd.setId).idle),
                forward=ShipRenderingAssets.GetIndex(ShipRenderingAssets.Get(sd.setId).forward),
                left=ShipRenderingAssets.GetIndex(ShipRenderingAssets.Get(sd.setId).left),
                right=ShipRenderingAssets.GetIndex(ShipRenderingAssets.Get(sd.setId).right)
            },
            sd.healthPoints, sd.deathEffectId, sd.reloadSpeed, sd.bulletForce,
            sd.bulletDamage, sd.bulletId, sd.colliderId, sd.rewardXp
        );
    }

    public static void CreateBotShip (
        float x, float y, float forwardForce, float turnForce,
        BotShipAnimationSet set, int hp, int effectID, float reloadSpeed,
        float bulletForceAdd, int bulletDamage, int bulletID,
        int colliderId, int xpReward) {

        byte[] botShipData = new byte[50];
        Buffer.BlockCopy(BitConverter.GetBytes(x), 0, botShipData, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(y), 0, botShipData, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(UnityEngine.Random.Range(0f, 180f)), 0, botShipData, 8, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(forwardForce), 0, botShipData, 12, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(turnForce), 0, botShipData, 16, 4);

        Buffer.BlockCopy(BitConverter.GetBytes((ushort)set.idle), 0, botShipData, 20, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)set.forward), 0, botShipData, 22, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)set.left), 0, botShipData, 24, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)set.right), 0, botShipData, 26, 2);

        Buffer.BlockCopy(BitConverter.GetBytes(UnityEngine.Random.Range(0, names.Length)), 0, botShipData, 28, 4);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)hp), 0, botShipData, 32, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)effectID), 0, botShipData, 34, 2);
        Buffer.BlockCopy(BitConverter.GetBytes(reloadSpeed), 0, botShipData, 36, 4);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)bulletDamage), 0, botShipData, 40, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)bulletForceAdd), 0, botShipData, 42, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)bulletID), 0, botShipData, 44, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)colliderId), 0, botShipData, 46, 2);
        Buffer.BlockCopy(BitConverter.GetBytes((ushort)xpReward), 0, botShipData, 48, 2);

        TcpStream.Send_CreateObject(UnityEngine.Random.Range(-999999999, 999999999), 11, botShipData);
    }

    public static readonly string[] names = {

        "Ulrich", "Jamie", "SSSniperWolf", "NotZoey", "Krome", "Sixface", "Pewdiepie", "America", "69",
        "France", "England", "Dwarf", "SG-1", "69420", "MoonDancer", "Sabina Moon Gower", "Whatever",
        "Rahim", "Eric", "Otis", "Ruby", "Aimie", "Steve", "Bill", "Billard", "Alex", "Luke", "Tash",
        "Robbie", "Roby", "SurferDude", "Bagforce", "Sparticus", "Rome", "Terk", "Eurpoe", "Germany",
        "Natsuki", "Monika", "Bildo", "Cecera", "Donald Trump", "Joe Biden", "Obama", "Osama", "Bob",
        "Bob 2", "Jimy", "Jessy", "Mario", "Laweegy", "Luigi", "Joe", "Joe Mama", "Adrion", "Adrian",
        "Joe Bob", "Jamason", "Hayley", "Bilo", "Jen", "Kira", "L", "N", "I", "You", "Eye", "El Mayo",
        "Dead", "Windows User", "Linux User", "Mac User", "Gei", "Gie", "Username!", ":Emoji:", "o",
        "Shrek", "Srek", "Serms", "Serm", "DT", "dtfm8", "m9", "<3", "UwU", "owo", "Ulrich", "Ulrich",
        "$#%^&(#)!", "Bork", "steve", "missel", "Wayne Kerr", "ninja", "tojimo", "sexpert", "Sex Kid",
        "Batman", "Superman", "Wonderwoman", "Wonder Woman", "Dude", "Duder", "Jimbob", "Jaybob", ":)",
        "Adam", "Jean", "Ola", "Minch", "Munich", "Anja", "Curvey", "Carly", "Shredder", "Schreidier",
        "Chriss Pratt", "Robbin", "Catwoman", "Brad Bitt", "Tom", "Bayley", "Jade", "Cody", "Dale",
        "Tyrone", "6Tyrone9", "Dirty D", "DD", "Kemper", "BTK", "Son of Sam", "Manson", "Charles", "G",
        "Bill", "Tench", "Ford", "Holden", "IT Guy", "ICT", "404", "69man", "Yeah", "Nope", "Sure"
    };
}
