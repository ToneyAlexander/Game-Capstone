using CCC.GameManagement;
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

    /// <summary>
    /// Quit this MainMenuController's Game.
    /// </summary>
    public void QuitGame()
    {
        game.Quit();
    }

    /// <summary>
    /// The Game that this MainMenu is a part of.
    /// </summary>
    [SerializeField]
    private Game game;
}
