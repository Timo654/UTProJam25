using UnityEngine;

[CreateAssetMenu(fileName = "Human", menuName = "Humans/New Human")]
public class HumanData : ScriptableObject
{
    public HumanType humanType; // or instrument?
    public Sprite[] sprites; // walking animation, 0 is idle, rest are walking steps
}
