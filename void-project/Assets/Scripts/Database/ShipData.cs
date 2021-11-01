
public class ShipData {

//INFO
    public virtual string shipName => "";
    public virtual int shipId => -1;
    public virtual int parentId => 0;
    public virtual int setId => 0;
    public virtual int level => 0;

//STATS:STD
    public virtual int healthPoints => 0;
    public virtual float regenSpeed => 0;

//STATS::MOVEMENT
    public virtual float forwardForce => 0;
    public virtual float turnForce => 0;
    public virtual float defaultDrag => 0;
    public virtual float defaultAngluarDrag => 0;
    public virtual int colliderId => 0;
    public virtual float brakePower => 0;

//STATS:COMBAT
    public virtual int bulletDamage => 0;
    public virtual int bulletForce => 0;
    public virtual float reloadSpeed => 0;
    public virtual WeaponType weaponType => WeaponType.Humon_Standard;

//BOT
    public virtual int bulletId => 0;
    public virtual int rewardXp => 0;
    public virtual int deathEffectId => 0;
    public virtual float forwardForceBot => 0;
    public virtual float turnForceBot => 0;
    public virtual bool forceNoBot => false;
}
