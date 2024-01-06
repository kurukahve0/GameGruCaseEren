using Case_2.Script.Level;
using UnityEngine;

namespace Case_2.Data
{
    [CreateAssetMenu(fileName = "Game Data")]
    public class GameData : ScriptableObject
    {

        public LevelController LevelPrefab;
        
        [Header("Stack")]
        public StackController StackPrefab;
        public float[] CreateXPositions = new float [2] { -2.5f, 2.5f };
    }
}