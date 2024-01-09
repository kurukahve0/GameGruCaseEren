using Palmmedia.ReportGenerator.Core.Parser;
using UnityEngine;

namespace Case_2
{
    public class StackController : MonoBehaviour
    {
        #region Variable

        public bool IsMovementOpen { get; set; } = false;
       // public MeshRenderer MeshRenderer=>meshRenderer;
        public float XBoundsSize => meshRenderer.bounds.size.x;
        public float ZBoundsSize => meshRenderer.bounds.size.z;

        public Material Material
        {
            set => meshRenderer.material = value;

            get => meshRenderer.material;
        }
        
        [Header("Definitions")] 
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Rigidbody rigidbody;
        
        
        
        private float movementSpeed=>GameManager.Instance.GameData.StackMovementSpeed;
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

        public void OpenPhysics(Vector3 torqueDirection)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddTorque((torqueDirection)*2f,ForceMode.Impulse);
            IsMovementOpen = false;
        }


        #endregion
    }
}