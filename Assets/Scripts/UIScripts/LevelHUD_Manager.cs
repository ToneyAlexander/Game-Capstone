using UnityEngine;
using UnityEngine.UI;

public class LevelHUD_Manager : MonoBehaviour
{
    /// <summary>
    /// For the player's name.
    /// </summary>
    [SerializeField]
    private BloodlineController bloodlineController = null;

    /// <summary>
    /// For the player's level.
    /// </summary>
    [SerializeField]
    private LevelExpStore levelExpStore = null;

    private Text textField;

    private Text nameField;

    #region MonoBehavior Messages
    private void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Level"))
            {
                textField = transform.GetChild(i).GetComponent<Text>();
            }
            else if (transform.GetChild(i).name.Equals("Name"))
            {
                nameField = transform.GetChild(i).GetComponent<Text>();
            }
        }
	}

    private void Update()
    {
		LevelUpdater();
    }
    #endregion

    private void LevelUpdater()
	{
		textField.text = levelExpStore.Level.ToString();
        nameField.text = bloodlineController.playerName;
	}
}
