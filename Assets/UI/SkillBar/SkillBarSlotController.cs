using CCC.Abilities;
using CCC.Stats;
using UnityEngine;
using UnityEngine.UI;

public sealed class SkillBarSlotController : MonoBehaviour
{
    private Ability ability;

    [SerializeField]
    private AbilitySlotDictionary abilityDictionary = null;

    [SerializeField]
    private Image abilityIcon = null;

    [SerializeField]
    private Image coolDownImage = null;

    public void BindAbility(Ability ability)
    {
        this.ability = ability;
        if (ability != Ability.Null)
        {
            var sprite = abilityDictionary.AbilityIconsAssetBundle.LoadAsset<Sprite>(ability.SpriteFilename);
            if (sprite)
            {
                abilityIcon.sprite = sprite;
                abilityIcon.color = new Color(255, 255, 255, 255);
            }
            else
            {
                Debug.LogError("[SkillBarSlotController.BindAbility] failed" +
                    " to load sprite with name '" + ability.SpriteFilename
                    + "'");
            }
        }
    }

    public void UpdateCooldown()
    {
        if ((ability != null) && 
            (ability.AbilityName != Ability.Null.AbilityName))
        {
            var remain = ability.cdRemain;
            var max = ability.Stats.Find(item => item.Name == Stat.AS_CD).Value;
            coolDownImage.fillAmount = remain / max;
        }
    }
}