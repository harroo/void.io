
public static partial class ShipID {

    public const int Ruzbad_Disruptor = 6;
}

public class Ruzbad_Disruptor : ShipData {

//INFO
    public override string shipName => "Disruptor";
    public override int shipId => ShipID.Ruzbad_Disruptor;
    public override int parentId => 4;
    public override int setId => 4;
    public override int level => 2;

//STATS:STD
    public override int healthPoints => 2;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 1;
    public override float turnForce => 0.1f;
    public override float defaultDrag => 1;
    public override float defaultAngluarDrag => 12;
    public override int colliderId => shipId;
    public override float brakePower => 3;

//STATS:COMBAT
    public override int bulletDamage => 4;
    public override int bulletForce => 0;
    public override float reloadSpeed => 0.6f;
    public override WeaponType weaponType => WeaponType.Ruzbad_Standard;

//BOT
    public override int bulletId => 13;
    public override int rewardXp => 690;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 1;
    public override float turnForceBot => 0.0015f;
}

public class Ruzbad_Disruptor_Collider : ColliderData {

    public override int id => ShipID.Ruzbad_Disruptor;

    public override bool isCircle => true;

    public override float radius => 0.08f;
}
