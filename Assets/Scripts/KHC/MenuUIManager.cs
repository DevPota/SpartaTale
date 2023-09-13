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

    [SerializeField] Animator fade;

    private void Start()
    {
        MenuUIButton startButton  = GameObject.Find("start").GetComponent<MenuUIButton>();
        MenuUIButton outButton    = GameObject.Find("out").GetComponent<MenuUIButton>();
        MenuUIButton creditButton = GameObject.Find("credit").GetComponent<MenuUIButton>();
        MenuUIButton titleButton  = GameObject.Find("title").GetComponent<MenuUIButton>();

        startButton.Init(() => { MenuUIManager.I.TransitionGame(); });
        outButton.Init(MenuUIManager.I.TransitionOut);
        creditButton.Init(() => { MenuUIManager.I.TransitionCredit(); });
        titleButton.Init(() => { });
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void TransitionGame()
    {
        fade.gameObject.SetActive(true);
        fade.SetTrigger("FadeIn");

        Invoke("LoadGame", 2.0f);
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene(6);
    }

    public void TransitionCredit()
    {
        fade.gameObject.SetActive(true);
        fade.SetTrigger("FadeIn");

        Invoke("LoadCredit", 2.0f);
    }

    public void LoadOut()
    {
        Application.Quit();
    }

    public void TransitionOut()
    {
        fade.gameObject.SetActive(true);
        fade.SetTrigger("FadeIn");

        Invoke("LoadOut", 2.0f);
    }
}
