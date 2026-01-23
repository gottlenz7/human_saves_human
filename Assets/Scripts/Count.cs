using UnityEngine;

public class Count : MonoBehaviour
{
    public static Count Instance { get; private set; }

    public int enemiesCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
