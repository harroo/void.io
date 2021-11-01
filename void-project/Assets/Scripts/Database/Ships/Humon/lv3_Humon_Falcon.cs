
public static partial class ShipID {

    public const int Humon_Falcon = 14;
}

public class Humon_Falcon : ShipData {

//INFO
    public override string shipName => "Falcon";
    public override int shipId => ShipID.Humon_Falcon;
    public override int parentId => ShipID.Humon_Jet;
    public override int setId => 10;
    public override int level => 3;

//STATS:STD
    public override int healthPoints => 2;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 5;
    public override float turnForce => 0.2f;
    public override float defaultDrag => 1.2f;
    public override float defaultAngluarDrag => 14;
    public override int colliderId => shipId;
    public override float brakePower => 3;

//STATS:COMBAT
    public override int bulletDamage => 1;
    public override int bulletForce => 0;
    public override float reloadSpeed => 6.9f;
    public override WeaponType weaponType => WeaponType.Humon_Rapid;

//BOT
    public override int bulletId => 1;
    public override int rewardXp => 820;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 5;
    public override float turnForceBot => 0.002f;
}

public class Humon_Falcon_Collider : ColliderData {

    public override int id => ShipID.Humon_Falcon;

    public override bool isCircle => true;

    public override float radius => 0.09f;
}
