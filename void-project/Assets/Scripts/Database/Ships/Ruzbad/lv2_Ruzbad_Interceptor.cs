
public static partial class ShipID {

    public const int Ruzbad_Interceptor = 7;
}

public class Ruzbad_Interceptor : ShipData {

//INFO
    public override string shipName => "Interceptor";
    public override int shipId => ShipID.Ruzbad_Interceptor;
    public override int parentId => 4;
    public override int setId => 5;
    public override int level => 2;

//STATS:STD
    public override int healthPoints => 1;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 4;
    public override float turnForce => 0.2f;
    public override float defaultDrag => 1;
    public override float defaultAngluarDrag => 14;
    public override int colliderId => 7;
    public override float brakePower => 3;

//STATS:COMBAT
    public override int bulletDamage => 3;
    public override int bulletForce => 0;
    public override float reloadSpeed => 0.6f;
    public override WeaponType weaponType => WeaponType.Ruzbad_Standard;

//BOT
    public override int bulletId => 13;
    public override int rewardXp => 690;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 4;
    public override float turnForceBot => 0.0032f;
}
