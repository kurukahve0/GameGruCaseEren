using Palmmedia.ReportGenerator.Core.Parser;
using UnityEngine;

namespace Case_2
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject tapToStartText;
        
        public bool StartTextOpening {
            set
            {
                tapToStartText.SetActive(value);
            }
        }

        void GameSateListener(GameState currentState)
        {
            
        }
    }
}