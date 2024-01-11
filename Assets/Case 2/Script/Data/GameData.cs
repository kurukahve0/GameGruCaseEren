using System.Collections.Generic;
using UnityEngine;

namespace Case_2.Data
{
    [CreateAssetMenu(fileName = "Game Data")]
    public class GameData : ScriptableObject
    {
        [Space(10)]
        [Header("Level")]
        public LevelController LevelPrefab;

        public float LevelLenght=30.85f;
        
        
        [Space(10)]
        [Header("Stack")]
        public StackController StackPrefab;
        public float[] CreateXPositions = new float [2] { -2.5f, 2.5f };
        public Vector3 StackBaseScale;
        public float StackMovementSpeed;
        public float StacekCreateTolerance;
        public List<Material> stackMaterial;
        
        [Space(10)] 
        [Header("Chibi")] 
        public float ChibiMovementSpeed;
        public float ChibiXLerpSpeed;
        public float ChibiCreateStackTriggerDistance;

        [Space(10)] 
        [Header("Game Sound")] 
        public List<SoundData> SoundData; // fazla ses eklenmesi durumunda





    }
}