
public static partial class ShipID {

    public const int Humon_Rhino = 12;
}

public class Humon_Rhino : ShipData {

//INFO
    public override string shipName => "Rhino";
    public override int shipId => ShipID.Humon_Rhino;
    public override int parentId => ShipID.Humon_Tank;
    public override int setId => 11;
    public override int level => 3;

//STATS:STD
    public override int healthPoints => 9;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 0.69f;
    public override float turnForce => 0.069f;
    public override float defaultDrag => 1;
    public override float defaultAngluarDrag => 12;
    public override int colliderId => shipId;
    public override float brakePower => 3;

//STATS:COMBAT
    public override int bulletDamage => 1;
    public override int bulletForce => 0;
    public override float reloadSpeed => 1;
    public override WeaponType weaponType => WeaponType.Humon_Standard;

//BOT
    public override int bulletId => 1;
    public override int rewardXp => 820;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 0.69f;
    public override float turnForceBot => 0.00069f;
}

public class Humon_Rhino_Collider : ColliderData {

    public override int id => ShipID.Humon_Rhino;

    public override bool isCircle => true;

    public override float radius => 0.09f;
}
