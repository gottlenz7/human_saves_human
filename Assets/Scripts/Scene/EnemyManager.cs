using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour 
{
    public static EnemyManager Instance;

    public TextMeshProUGUI apples, keys;
    public int applesCount, keysCount;

    private int currentEnemiesCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentEnemiesCount = Count.Instance.enemiesCount;
    }

    public void LoseEnemy()
    {
        currentEnemiesCount -= 1;

        if (currentEnemiesCount == 0)
        {
            applesCount += Random.Range(2, 3);
            keysCount += 1;

            UpdateText();

            enabled = false;
        }
    }

    public void LoseApples()
    {
        applesCount -= 1;
        UpdateText();
    }

    private void UpdateText()
    {
        if (apples != null && keys != null)
        {
            apples.text = "x " + applesCount;
            keys.text = "x " + keysCount;
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
