using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public int index;

    public void PlayGame ()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        if (name == "Berry") index = 0;
        if (name == "Cyrus") index = 1;
        if (name == "Korva") index = 2;
        if (name == "Lezo") index = 3;
        if (name == "Roann") index = 4;
        if (name == "Stokv") index = 5;

        PlayerPrefs.SetString("CharacterName", name);
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
