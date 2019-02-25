using CCC.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotController : MonoBehaviour
{

    private Image coolDownImage;

    [SerializeField]
    private Image abilityImage;

    public float cooldownTimer;

    [SerializeField]
    private AbilityPrototype ability;

    private bool isInCoolDown;

    private void Start()
    {
        isInCoolDown = false;
        abilityImage.GetComponent<Image>().sprite = ability.Icon; //set icon in bar
        coolDownImage = abilityImage.transform.GetChild(1).gameObject.GetComponent<Image>();
    }

    void Update()
    {
        updateCoolDown();
    }


    private void updateCoolDown()
    {
        if (Input.GetKeyDown(KeyCode.M) && isInCoolDown != true)
        {
            isInCoolDown = true;
            coolDownImage.fillAmount = 1;

        }
        if (isInCoolDown)
        {
            coolDownImage.fillAmount -= (1 / cooldownTimer * Time.deltaTime);

            if (coolDownImage.fillAmount <= 0)
            {
                isInCoolDown = false;
            }
        }
    }
}
