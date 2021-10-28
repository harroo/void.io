
using TMPro;

using System;

using UnityEngine;

public class BotShip : Object {

    private float forwardForce;
    private float turnForce;
    private float brakePower;

    public override string Type => "BOT_SHIP";

    public override void Config (byte[] buf) {

        if (buf.Length == 4) {

            animator.UpdateSprite(BitConverter.ToInt32(buf, 0));

            return;
        }

        transform.position = new Vector3(
            BitConverter.ToSingle(buf, 0),
            BitConverter.ToSingle(buf, 4),
            0
        );

        transform.eulerAngles = new Vector3(
            0,
            0,
            BitConverter.ToSingle(buf, 8)
        );

        forwardForce = BitConverter.ToSingle(buf, 12);
        turnForce = BitConverter.ToSingle(buf, 16);

        animator.aset.idle = BitConverter.ToUInt16(buf, 20);
        animator.aset.forward = BitConverter.ToUInt16(buf, 22);
        animator.aset.left = BitConverter.ToUInt16(buf, 24);
        animator.aset.right = BitConverter.ToUInt16(buf, 26);

        nameIndexCache = BitConverter.ToInt32(buf, 28);

        GetComponent<ObjectMortality>().healthPoints = BitConverter.ToUInt16(buf, 32);
        GetComponent<ObjectMortality>().deathEffectID = BitConverter.ToUInt16(buf, 34);

        reloadSpeed = BitConverter.ToSingle(buf, 36);
        bulletDamage = BitConverter.ToUInt16(buf, 40);
        bulletForceAdd = BitConverter.ToUInt16(buf, 42);
        bulletID = BitConverter.ToUInt16(buf, 44);

        colliderId = BitConverter.ToUInt16(buf, 46);

        xpReward = BitConverter.ToUInt16(buf, 48);
    }

    private int nameIndexCache, colliderId, xpReward;
    private float reloadSpeed; private int bulletDamage, bulletForceAdd, bulletID;

    public BotShipAnimationController animator;
    public TextMeshPro usernameDisplay;

    private Rigidbody2D rigidBody;

    public override void Asign () {

        usernameDisplay.text = BotShipSender.names[nameIndexCache];

        GetComponent<ColliderCalculator>().Render(colliderId);

        GetComponent<ObjectMortality>().awardAmountMin = xpReward;
        GetComponent<ObjectMortality>().awardAmountMax = xpReward + 256;
    }

    public override void Spawn () {

        animator = GetComponent<BotShipAnimationController>();
        rigidBody = GetComponent<Rigidbody2D>();

        BotShipCreator.Add(this);

        animator.SetIdle();
    }

    public override void Despawn () {

        BotShipCreator.Remove(this);
    }

    private int actionID;
    private float actionTime;
    private float reloadTime;

    public override void Tick () {

        if (actionTime < 0) { actionTime = UnityEngine.Random.Range(0.0f, 8.0f);

            ChooseNewAction();

        } else actionTime -= Time.deltaTime;

        switch (actionID) {

            case 0: FlyForward(); break;
            case 1: TurnLeft(); break;
            case 2: TurnRight(); break;
            case 3: FlyForward(); TurnLeft(); break;
            case 4: FlyForward(); TurnRight(); break;
            case 5: case 6: case 7: case 8: Idle(); break;
        }

        if (reloadTime < 0) { reloadTime = reloadSpeed;

            if (UnityEngine.Random.Range(0, 3) != 0) return;

            Object obj = ObjectManager.instance.ClosestInRange(this, 8.0f);

            if (obj == null) return;

            if (obj.Type == "ASTEROID" || obj.Type == "PLAYER_SHIP") {

                var cache = transform.up;
                transform.up = new Vector2(
                    obj.transform.position.x - transform.position.x,
                    obj.transform.position.y - transform.position.y
                );
                transform.Translate(Vector3.up * 0.15f);

                BulletCreator.CreateBullet_BOT(bulletID, bulletForceAdd, bulletDamage, transform.position, transform.eulerAngles);

                transform.Translate(Vector3.up * -0.15f);
                transform.up = cache;
            }

        } else reloadTime -= Time.deltaTime;
    }

    private void ChooseNewAction () {

        actionID = UnityEngine.Random.Range(0, 6);
    }

    private void FlyForward () {

        // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        // if (hit.collider != null) {
        //
        //     float distance = (transform.position - hit.collider.transform.position).magnitude;
        //     if (distance < 1.0f) ChooseNewAction();
        // }

        rigidBody.AddForce(transform.up * forwardForce);

        animator.SetForward();
    }

    private void TurnLeft () {

        rigidBody.AddTorque(turnForce);

        animator.SetLeft();
    }

    private void TurnRight () {

        rigidBody.AddTorque(-turnForce);

        animator.SetRight();
    }

    private void Idle () {

        animator.SetIdle();
    }
}
