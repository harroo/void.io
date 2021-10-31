
public static partial class ShipID {

    public const int Pirate_Ravager = 9;
}

public class Pirate_Ravager : ShipData {

//INFO
    public override string shipName => "Ravager";
    public override int shipId => ShipID.Pirate_Ravager;
    public override int parentId => 5;
    public override int setId => 8;

//STATS:STD
    public override int healthPoints => 6;
    public override float regenSpeed => 0.5f;

//STATS::MOVEMENT
    public override float forwardForce => 0.5f;
    public override float turnForce => 0.2f;
    public override float defaultDrag => 1;
    public override float defaultAngluarDrag => 30;
    public override int colliderId => 9;
    public override float brakePower => 2;

//STATS:COMBAT
    public override int bulletDamage => 1;
    public override int bulletForce => 0;
    public override float reloadSpeed => 1.2f;
    public override WeaponType weaponType => WeaponType.Pirate_Standard;

//BOT
    public override int bulletId => 14;
    public override int rewardXp => 690;
    public override int deathEffectId => 2;
    public override float forwardForceBot => 0.5f;
    public override float turnForceBot => 0.0032f;
}
