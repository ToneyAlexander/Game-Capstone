using CCC.Abilities;
using UnityEngine;
using UnityEngine.UI;

public sealed class SkillBarSlotController : MonoBehaviour
{
    [SerializeField]
    private AbilitySlotDictionary abilityDictionary = null;

    [SerializeField]
    private GameObject image = null;

    private Image abilityImage;

    public void BindAbility(Ability ability)
    {
        if (ability != Ability.Null)
        {
            Debug.Log("[SkillBarSlotController.BindAbility] Binding ability = " + ability.AbilityName);
            Debug.Log(ability.AbilityName);
            var sprite = abilityDictionary.AbilityIconsAssetBundle.LoadAsset<Sprite>(ability.SpriteFilename);
            if (sprite)
            {
                abilityImage.sprite = sprite;
                abilityImage.color = new Color(255, 255, 255, 255);
            }
            else
            {
                Debug.LogError("[SkillBarSlotController.BindAbility] failed" +
                    " to load sprite with name '" + ability.SpriteFilename
                    + "'");
            }
        }
    }

    #region MonoBehavior Messages
    private void Awake()
    {
        abilityImage = image.GetComponent<Image>();
    }
    #endregion
}