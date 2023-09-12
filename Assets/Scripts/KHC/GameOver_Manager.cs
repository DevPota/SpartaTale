using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Manager : MonoBehaviour
{
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(3);
    }
}
