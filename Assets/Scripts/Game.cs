using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
