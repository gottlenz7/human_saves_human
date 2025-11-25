using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public int prevScene, nextScene;
    public Transform topCollider, bottomCollider;

    private Vector2 playerPosition;
    private string sceneName;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerPosition = Player.Instance.transform.position;

        if (collision.CompareTag("PlayerT"))
        {
            if (playerPosition.y > -2)
            {
                Player.Instance.rb.position = new Vector2(0f, bottomCollider.position.y + 0.7f);
                SceneManager.LoadScene(nextScene);
            }

            else
            {
                if (sceneName == "forest_loc")
                    Player.Instance.rb.position = new Vector2(3.4f, -1f);
                else 
                    Player.Instance.rb.position = new Vector2(0f, topCollider.position.y - 0.7f);

                SceneManager.LoadScene(prevScene);
            }
        }
    }
}
