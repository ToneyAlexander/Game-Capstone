using CCC.Abilities;

public class UseAbilityCommand : ICommand
{
    private Ability ability;
    private AbilityUser abilityUser;

    public UseAbilityCommand(AbilityUser abilityUser, Ability ability)
    {
        this.ability = ability;
        this.abilityUser = abilityUser;
    }

    public void InvokeCommand()
    {
        abilityUser.Use(ability);
    }
}
