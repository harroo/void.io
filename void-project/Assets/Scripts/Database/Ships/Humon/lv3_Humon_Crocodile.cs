
public static partial class ShipID {

    public const int Humon_Crocodile = 11;
}

public class Humon_Crocodile : ShipData {

//INFO
    public override string shipName => "Crocodile";
    public override int shipId => ShipID.Humon_Crocodile;
    public override int parentId => ShipID.Humon_Tank;
    public override int setId => 9;
    public override int level => 3;

//STATS:STD
    public override int healthPoints => 7;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 1.5f;
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
    public override int rewardXp => 820;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 1;
    public override float turnForceBot => 0.0015f;
}

public class Humon_Crocodile_Collider : ColliderData {

    public override int id => ShipID.Humon_Crocodile;

    public override bool isCircle => true;

    public override float radius => 0.09f;
}
