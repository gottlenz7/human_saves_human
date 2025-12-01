using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class TriggerCollider : MonoBehaviour
{
    public static TriggerCollider Instance { get; private set; }
    public enum Side
    {
        N,
        S,
        E,
        W
    }

    public int nextScene;
    public Side sideOfTheWorld;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerT"))
        {
            GameManager.Instance.LoadScene(nextScene, sideOfTheWorld, this.name);
        }
    }
}
