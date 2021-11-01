
public static partial class ShipID {

    public const int Humon_Artemis = 15;
}

public class Humon_Artemis : ShipData {

//INFO
    public override string shipName => "Artemis";
    public override int shipId => ShipID.Humon_Artemis;
    public override int parentId => ShipID.Humon_Crocodile;
    public override int setId => 13;
    public override int level => 4;

//STATS:STD
    public override int healthPoints => 8;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 2;
    public override float turnForce => 0.1f;
    public override float defaultDrag => 1;
    public override float defaultAngluarDrag => 12;
    public override int colliderId => shipId;
    public override float brakePower => 3;

//STATS:COMBAT
    public override int bulletDamage => 2;
    public override int bulletForce => 0;
    public override float reloadSpeed => 1;
    public override WeaponType weaponType => WeaponType.Humon_Standard;

//BOT
    public override int bulletId => 1;
    public override int rewardXp => 1024;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 2;
    public override float turnForceBot => 0.0015f;

    public override bool forceNoBot => true;
}

public class Humon_Artemis_Collider : ColliderData {

    public override int id => ShipID.Humon_Artemis;

    public override bool isCircle => true;

    public override float radius => 0.09f;
}
