public class Damage
{
    public float physicalDmg;
    public float physicalDmgReal;
    public float magicDmg;
    public float magicDmgReal;
    public bool rangedAttack;
    public bool meleeAttack;
    public bool spell;

    public Damage(float pD, float mD, bool ranged, bool melee, bool spl)
    {
        physicalDmg = physicalDmgReal = pD;
        magicDmg = magicDmgReal = mD;
        rangedAttack = ranged;
        meleeAttack = melee;
        spell = spl;
    }
}
