using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer debugSprite; // debug
    private BeatManager m_beatManager;
    private int attackBeatIndex = 0; // index it uses in beatManager, ADJUST IF NEEDED
    private void Awake()
    {
        m_beatManager = FindAnyObjectByType<BeatManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool didHit = m_beatManager.CheckIfHitClose(attackBeatIndex);
            debugSprite.color = didHit ? Color.green : Color.red;
        }
    }
}
