using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a Controller for a main menu.
/// </summary>
public sealed class MainMenuController : MonoBehaviour {

    public void PlayKongGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
