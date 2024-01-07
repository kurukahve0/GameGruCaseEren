using Case_2.Script.Level;
using UnityEngine;
using UnityEngine.Serialization;

namespace Case_2.Data
{
    [CreateAssetMenu(fileName = "Game Data")]
    public class GameData : ScriptableObject
    {
        [Space(10)]
        [Header("Level")]
        public LevelController LevelPrefab;
        
        
        [Space(10)]
        [Header("Stack")]
        public StackController StackPrefab;
        public float[] CreateXPositions = new float [2] { -2.5f, 2.5f };
        public Vector3 StackBaseScale;
        public float StackMovementSpeed;
        
        
        [Space(10)] 
        [Header("Chibi")] 
        public float ChibiMovementSpeed;
        public float ChibiXLerpSpeed;
       public float ChibiCreateStackTriggerDistance;

    }
}