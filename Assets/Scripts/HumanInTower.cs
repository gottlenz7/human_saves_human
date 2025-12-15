using UnityEngine;

public class HumanInTower : MonoBehaviour
{
    public Sprite Davidka, Lerka;

    private SpriteRenderer human;

    private void Start()
    {
        human = GetComponent<SpriteRenderer>();

        if (PlayerVisual.Instance.isMan)
            human.sprite = Lerka;
        else
            human.sprite = Davidka;
    }
}
