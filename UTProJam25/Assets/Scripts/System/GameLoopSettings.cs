using UnityEngine;

namespace System
{
    [CreateAssetMenu(fileName = "GameLoopSettings", menuName = "GameSettings", order = 0)]
    public class GameLoopSettings : ScriptableObject
    {
        public int gameDuration = 50;
    }
}