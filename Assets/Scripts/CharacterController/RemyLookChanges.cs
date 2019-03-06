using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyLookChanges : MonoBehaviour
{

    public Material remyBodyMaterial;
    public Material remyBottomMaterial;
    public Material remyHairMaterial;
    public Material remyTopMaterial;
    public Material remyShoesMaterial;

    //Skin stuff
    private List<Color> skinColors = new List<Color>();
    private Color[] skinArray;

    private Color skinWhite;
    private Color skinBrown;
    private Color skinDarkBrown;
    private Color skinYellow;
    private Color skinShallowYellow;

    //hair stuff
    private List<Color> hairColors = new List<Color>();
    private Color[] hairArray;

    private Color hairWhite;
    private Color hairBrown;
    private Color hairBlack;
    private Color hairYellow;
    private Color hairShallowYellow;
    private Color hairBlue;

    //top stuff
    private List<Color> topColors = new List<Color>();
    private Color[] topArray;

    private Color topWhite;
    private Color topBlack;
    private Color topGray;
    private Color topYellow;
    private Color topRed;


    private Color thisColor;


    // Start is called before the first frame update
    void Start()
    {

        Skin();
        Hair();
        Top();

    }

    // Update is called once per frame
    void Skin()
    {
        skinColors.Add(skinWhite = Color.white);

        //skinColors.Add(skinBrown = new Color(0.553f, 0.333f, 0.141f));

        skinColors.Add(skinYellow = new Color(0.945f, 0.761f, 0.492f));

        skinColors.Add(skinShallowYellow = new Color(255.0f / 255.0f, 219f / 255f, 172f / 255f));

        skinColors.Add(skinDarkBrown = new Color(141f / 255f, 85f / 255f, 36f / 255f));

        skinArray = skinColors.ToArray();
        thisColor = skinColors[(int)(Random.value * skinArray.Length)];


        remyBodyMaterial.SetColor("_Color", thisColor);

        Debug.Log("Body: " + remyBodyMaterial.color);
    }

    void Hair()
    {
        hairColors.Add(hairWhite = Color.white);

        //hairColors.Add(hairBrown = new Color(0.553f, 0.333f, 0.141f));

        hairColors.Add(hairYellow = new Color(0.945f, 0.761f, 0.492f));

        hairColors.Add(hairShallowYellow = new Color(255f / 255f, 219f / 255f, 172f / 255f));

        hairColors.Add(hairBlack = Color.black);

        hairColors.Add(hairBlue = Color.blue);

        hairArray = hairColors.ToArray();
        thisColor = hairArray[(int)(Random.value * hairArray.Length)];


        remyHairMaterial.SetColor("_Color", thisColor);

        Debug.Log("Hair: " + remyHairMaterial.color);
    }

    void Top()
    {

        topColors.Add(topBlack = Color.black);

        topColors.Add(topGray = Color.gray);

        topColors.Add(topRed = Color.red);

        topColors.Add(topYellow = Color.yellow);

        topArray = topColors.ToArray();
        thisColor = topArray[(int)(Random.value * hairArray.Length)];


        remyTopMaterial.SetColor("_Color", thisColor);

        Debug.Log("Top: " + remyHairMaterial.color);
    }


}
