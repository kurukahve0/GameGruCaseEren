using UnityEngine;

namespace Case_2
{
    public class StackController : MonoBehaviour
    {
        #region Variable

        public bool IsMovementOpen { get; set; } = false;
        public MeshRenderer MeshRenderer=>meshRenderer;
        
        [Header("Definitions")] 
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Rigidbody rigidbody;
        
        
        
        private float movementSpeed=2f;
        private bool movingRight;
        #endregion
    
        #region MonoBehaviour

        void Start()
        {
        
        }

        void Update()
        {
            UpdateObjectPosition();
        }
        

        #endregion
    
        #region Func

        void UpdateObjectPosition()
        {
            if(!IsMovementOpen)
                return;
            float movement = movementSpeed * Time.deltaTime;
            
            transform.Translate((movingRight?Vector3.right: Vector3.left) * movement);
            
            if (transform.position.x >=  GameManager.Instance.GameData.CreateXPositions[1])
            {
                movingRight = false;
            }
            else if (transform.position.x <=  GameManager.Instance.GameData.CreateXPositions[0])
            {
                movingRight = true;
            }
        }

        public void OpenPhysics(bool isRight)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddTorque((isRight ?- Vector3.forward : Vector3.forward)*2f,ForceMode.Impulse);
        }


        #endregion
    }
}