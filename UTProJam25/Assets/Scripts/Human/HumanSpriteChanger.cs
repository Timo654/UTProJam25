using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class HumanSpriteChanger : MonoBehaviour
{
    [FormerlySerializedAs("spriteRenderer")] [SerializeField] private SpriteRenderer humanSpriteRenderer;
    

    public void UpdateSprite(Sprite sprite)
    {
        humanSpriteRenderer.sprite = sprite;
        humanSpriteRenderer.flipX = Random.Range(0, 2) == 1;
    }
}
