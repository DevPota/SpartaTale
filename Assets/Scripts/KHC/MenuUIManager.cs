using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    #region Singleton
    public static MenuUIManager I { get; private set; } = null;

    private void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        MenuUIButton startButton  = GameObject.Find("start").GetComponent<MenuUIButton>();
        MenuUIButton outButton    = GameObject.Find("out").GetComponent<MenuUIButton>();
        MenuUIButton creditButton = GameObject.Find("credit").GetComponent<MenuUIButton>();
        MenuUIButton titleButton  = GameObject.Find("title").GetComponent<MenuUIButton>();

        startButton.Init(() => { MenuUIManager.I.LoadScene(2); });
        outButton.Init(Application.Quit);
        creditButton.Init(() => { /*MenuUIManager.I.LoadScene(3);*/ });
        titleButton.Init(() => { });
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
