
using UnityEngine;

public class ObjectMortality : MonoBehaviour {

    private void Start () {

        if (!GlobalValues.Hosting) Destroy(this);
    }

    public int healthPoints, awardAmountMin, awardAmountMax, deathEffectID = -1;

    private int LatestAttackingID;

    private void Kill () {

        if (LatestAttackingID != 0) {

            TcpStream.Send_AwardXP(LatestAttackingID, Random.Range(awardAmountMin, awardAmountMax));
        }

        GetComponent<Object>().DeleteThisObject();

        if (deathEffectID != -1) {

            EffectCreator.CreateEffect(deathEffectID, transform.position, transform.eulerAngles);
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {

        if (collision.collider.tag == "DAMAGING") {

            healthPoints -= collision.collider.GetComponent<Damager>().damage;

            Object obj = collision.collider.GetComponent<Object>();
            LatestAttackingID = obj == null ? 0 : obj.GetAttackingID();

            if (healthPoints <= 0) Kill();

        } else if (collision.collider.tag != "INSTA_KILL") {

            Object obj = collision.collider.GetComponent<Object>();
            LatestAttackingID = obj == null ? 0 : obj.GetAttackingID();

            Kill(); //insane amnt damage, instant kill
        }
    }
}
