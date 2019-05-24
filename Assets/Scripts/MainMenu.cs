using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public int index;

    public void PlayGame ()
    {
        GameState.instance.emitLobbyEntered();
    }

    public void PlayGame2()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        PlayerPrefs.SetString("CharacterName", name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
