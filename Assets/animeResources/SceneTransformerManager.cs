using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransformerManager : MonoBehaviour
{
    protected static SceneTransformerManager instance;
    public static SceneTransformerManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SceneTransformerManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    [SerializeField]
    private CanvasGroup SceneTransformerManagerCanvasGroup;
    [SerializeField]
    private UnityEngine.UI.Image progressBar;

    private string loadSceneName;

    public static SceneTransformerManager Create() 
    {
        var SceneTransformerManagerPrefab = Resources.Load<SceneTransformerManager>("SceneTransformerManager");
        return Instantiate(SceneTransformerManagerPrefab);
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }
    private IEnumerator Load(string sceneName)
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            SceneTransformerManagerCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}
