
public static partial class ShipID {

    public const int Ruzbad_Default = 4;
}

public class Ruzbad_Default : ShipData {

//INFO
    public override string shipName => "Ruzbad";
    public override int shipId => ShipID.Ruzbad_Default;
    public override int parentId => -1;
    public override int setId => 3;
    public override int level => 1;

//STATS:STD
    public override int healthPoints => 2;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 2;
    public override float turnForce => 0.02f;
    public override float defaultDrag => 1;
    public override float defaultAngluarDrag => 2;
    public override int colliderId => shipId;
    public override float brakePower => 2;

//STATS:COMBAT
    public override int bulletDamage => 3;
    public override int bulletForce => 0;
    public override float reloadSpeed => 0.5f;
    public override WeaponType weaponType => WeaponType.Ruzbad_Standard;

//BOT
    public override int bulletId => 13;
    public override int rewardXp => 420;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 2;
    public override float turnForceBot => 0.002f;
}

public class Ruzbad_Default_Collider : ColliderData {

    public override int id => ShipID.Ruzbad_Default;

    public override bool isCircle => true;

    public override float radius => 0.08f;
}
