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
    /// Change the current scene of this MainMenuController's Game.
    /// </summary>
    /// <param name="sceneReference">
    /// The SceneReferene that represents the Scene to change to.
    /// </param>
    public void ChangeScene(SceneReference sceneReference)
    {
        // Only MonoBehaviours can start coroutines
        StartCoroutine(game.ChangeToScene(sceneReference));
    }

    /// <summary>
    /// The Game that this MainMenu is a part of.
    /// </summary>
    [SerializeField]
    private Game game;
}
