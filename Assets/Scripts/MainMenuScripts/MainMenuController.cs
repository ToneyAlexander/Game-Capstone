using CCC.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void PlayMainGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    /// <summary>
    /// The Game that this MainMenu is a part of.
    /// </summary>
    [SerializeField]
    private Game game;
}
