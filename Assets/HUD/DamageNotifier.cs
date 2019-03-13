using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageNotifier : MonoBehaviour
{

    [SerializeField]
    private TMP_FontAsset font;

    [SerializeField]
    private Color mainColor;

    [SerializeField]
    private Color glowColor;

    [SerializeField]
    private Camera cam;

    public void CreateDamageNotifier(float damage, GameObject parent)
    {
        GameObject newDamage = InitializeText(damage, parent);
        Modifier(newDamage);
    }

    private GameObject InitializeText(float dmg, GameObject parent)
    {

        GameObject damageNotifierCanvas = new GameObject("damageNotifierCanvas");
        Canvas c = damageNotifierCanvas.AddComponent<Canvas>();
        c.transform.position = parent.transform.position;
        GameObject newDamage = new GameObject("damageText");
        newDamage.transform.SetParent(damageNotifierCanvas.transform);
        int damage = (int)dmg;
        TextMeshPro text = newDamage.AddComponent<TextMeshPro>();
        RectTransform trans = newDamage.GetComponent<RectTransform>();

        trans.sizeDelta = new Vector2(4, 4);
        Vector3 positionVariation = new Vector3(Random.Range(-3, 3), Random.Range(-2, 2), 0);
        trans.position = parent.transform.position + positionVariation;
        text.font = font;
        text.fontStyle = FontStyles.Bold;
        text.fontSize = 12;
        text.text = damage.ToString();
        text.color = mainColor;
        text.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.2f);
        text.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, 1.0f);
        text.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, glowColor);
        return newDamage;
    }

    private void Modifier(GameObject newDamage)
    {
        StartCoroutine(ShrinkText(newDamage, .05f));
        StartCoroutine(FadeAway(newDamage, .5f));
        StartCoroutine(LiftText(newDamage, 2f));
    }

    private IEnumerator ShrinkText(GameObject newDamage, float time)
    {
        yield return new WaitForSeconds(time);
        TextMeshPro text = newDamage.GetComponent<TextMeshPro>();
        text.fontSize -= 4;
    }

    private IEnumerator LiftText(GameObject newDamage, float time)
    {
        float elapsedTime = 0;
        RectTransform trans = newDamage.GetComponent<RectTransform>();
        Vector3 start = trans.localPosition;
        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            trans.localPosition = Vector2.Lerp(start, new Vector3(start.x, start.y+2, start.z), t);
            newDamage.transform.rotation = Quaternion.LookRotation(newDamage.transform.position - Camera.main.transform.position);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(newDamage);
    }

    private IEnumerator FadeAway(GameObject newDamage, float time)
    {
        yield return new WaitForSeconds(time);
        TextMeshPro text = newDamage.GetComponent<TextMeshPro>();
        for (float i = 1; i > 0; i -= Time.deltaTime)
        {
            text.color = new Color(mainColor.r, mainColor.g, mainColor.b, i);
            yield return null;
        }
    }
}
