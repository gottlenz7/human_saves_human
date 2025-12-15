using UnityEngine;
using UnityEngine.SceneManagement;
using static TriggerCollider;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float indentation = 0.7f;
    private TriggerCollider.Side exitSide;
    private string exitTrigger;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(int nextScene, Side sideOfTheWorld, string triggerName)
    {
        exitSide = sideOfTheWorld;
        exitTrigger = triggerName;
        SceneManager.LoadScene(nextScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        TriggerCollider[] triggers = FindObjectsOfType<TriggerCollider>();

        foreach (var trigger in triggers)
        {
            if (trigger.sideOfTheWorld == NewSide(exitSide, exitTrigger))
            {
                Player.Instance.rb.position = CalculateSpawnPosition(trigger);
                return;
            }
        }
    }

    private Vector2 CalculateSpawnPosition(TriggerCollider transition)
    {
        Collider2D col = transition.GetComponent<Collider2D>();
        Vector2 basePosition = col.bounds.center;

        switch (transition.sideOfTheWorld)
        {
            case TriggerCollider.Side.N:
                return basePosition + Vector2.down * indentation;
            case TriggerCollider.Side.S:
                return basePosition + Vector2.up * indentation;
            case TriggerCollider.Side.E:
                return basePosition + Vector2.left * indentation;
            case TriggerCollider.Side.W:
                return basePosition + Vector2.right * indentation;
            default:
                return basePosition;
        }
    }

    private TriggerCollider.Side NewSide(TriggerCollider.Side side, string trigger)
    {
        if (trigger == "load_scene !!")
        {
            switch (side)
            {
                case TriggerCollider.Side.S: return TriggerCollider.Side.E;
                case TriggerCollider.Side.E: return TriggerCollider.Side.S;
            }
        }

        switch (side)
        {
            case TriggerCollider.Side.N: return TriggerCollider.Side.S;
            case TriggerCollider.Side.S: return TriggerCollider.Side.N;
            case TriggerCollider.Side.E: return TriggerCollider.Side.W;
            case TriggerCollider.Side.W: return TriggerCollider.Side.E;
            default: return TriggerCollider.Side.S;
        }
    }
}
