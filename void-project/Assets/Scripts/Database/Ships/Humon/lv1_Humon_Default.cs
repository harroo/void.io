
public static partial class ShipID {

    public const int Humon_Default = 1;
}

public class Humon_Default : ShipData {

//INFO
    public override string shipName => "Humon";
    public override int shipId => ShipID.Humon_Default;
    public override int parentId => -1;
    public override int setId => 0;
    public override int level => 1;

//STATS:STD
    public override int healthPoints => 4;
    public override float regenSpeed => 1;

//STATS::MOVEMENT
    public override float forwardForce => 2;
    public override float turnForce => 0.02f;
    public override float defaultDrag => 1;
    public override float defaultAngluarDrag => 2;
    public override int colliderId => 1;
    public override float brakePower => 2;

//STATS:COMBAT
    public override int bulletDamage => 1;
    public override int bulletForce => 0;
    public override float reloadSpeed => 1;
    public override WeaponType weaponType => WeaponType.Humon_Standard;

//BOT
    public override int bulletId => 1;
    public override int rewardXp => 420;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 2;
    public override float turnForceBot => 0.002f;
}
