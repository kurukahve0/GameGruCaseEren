using System;
using System.Collections.Generic;
using Case_2.Script.Level;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Case_2
{
    public class StackCreator : MonoBehaviour
    {
        #region Variable

        [SerializeField] private Transform stackParent;
        public List<StackController> activeStacks;
        private float zDistance => activeStacks[^1].transform.localPosition.z + activeStacks[^1].ZBoundsSize/ 2;
        public StackController NewStack { get; private set; }

        #endregion
        
        #region StackCreate

        public void CreateStack()
        {
            float xBackSize = activeStacks[^1].XBoundsSize;
            float xFrontSize = NewStack.XBoundsSize;
            float distanceStacks =
                Mathf.Abs(activeStacks[^1].transform.localPosition.x - NewStack.transform.localPosition.x);


            bool isRight = activeStacks[^1].transform.localPosition.x - NewStack.transform.localPosition.x < 0; // sağ sol ayarı için



            if (Mathf.Abs(activeStacks[^1].transform.localPosition.x - NewStack.transform.localPosition.x)<.3f) // toleranslı yerleştirme
            {
                PerfectPlacementForNewStack();
            }
            else
            {
                CreateTrashStackPiece(xBackSize, xFrontSize, distanceStacks, isRight);
                CreateNecessaryStackPiece(xBackSize, distanceStacks, isRight);
                

            }
            
        }


        public void CreateNewStack()
        {
  
            NewStack = Instantiate(GameManager.Instance.GameData.StackPrefab, stackParent);
            Vector3 stackPosition = Vector3.zero;
            stackPosition.z =  zDistance+NewStack.ZBoundsSize / 2;
            stackPosition.x = GameManager.Instance.GameData.CreateXPositions[Random.Range(0, 2)];
            NewStack.transform.localPosition = stackPosition;
            NewStack.transform.localScale = GetNewStackScale();
            //activeStacks.Add(newStack);
            NewStack.IsMovementOpen = true;
        }

        void CreateNecessaryStackPiece(float xBackSize, float distanceStacks, bool isRight)
        {
            RemoveNewStack();
    
            int factor = isRight ? 1 : -1; // sağsol pozisyon ayarlamak için

            var stack = Instantiate(GameManager.Instance.GameData.StackPrefab, stackParent);
            Vector3 stackPosition = activeStacks[^1].transform.localPosition;
            stackPosition.z = zDistance+stack.ZBoundsSize / 2;
            stackPosition.x = activeStacks[^1].transform.localPosition.x + factor * distanceStacks / 2f;
            stack.transform.localPosition = stackPosition;
            stack.transform.localScale = new Vector3(xBackSize - distanceStacks, stack.transform.localScale.y,
                stack.transform.localScale.z);
            activeStacks.Add(stack);
        }

        void CreateTrashStackPiece(float xBackSize, float xFrontSize, float distanceStacks, bool isRight)
        {
            int factor = isRight ? 1 : -1; // sağsol pozisyon ayarlamak için
            var stack = Instantiate(GameManager.Instance.GameData.StackPrefab, stackParent);
            Vector3 stackPosition = activeStacks[^1].transform.localPosition;
            stackPosition.z = zDistance+stack.ZBoundsSize/ 2;
            stackPosition.x = activeStacks[^1].transform.localPosition.x + factor * xFrontSize / 2 +
                              factor * distanceStacks / 2f;
            stack.transform.localPosition = stackPosition;
            stack.transform.localScale = new Vector3(xFrontSize - (xBackSize - distanceStacks),
                stack.transform.localScale.y, stack.transform.localScale.z);
            
            stack.OpenPhysics(isRight);
        }
        

        #endregion

        #region StackSetting

        void PerfectPlacementForNewStack()
        {
            Vector3 perfectPositions = activeStacks[^1].transform.localPosition;
            perfectPositions.z=zDistance+NewStack.ZBoundsSize / 2;
            NewStack.transform.localPosition = perfectPositions;
            activeStacks.Add(NewStack);
            NewStack.IsMovementOpen = false;
            NewStack = null;
            
        }
        
        void RemoveNewStack()
        {
            NewStack.gameObject.SetActive(false);
            NewStack = null;
        }
        

        Vector3 GetNewStackScale()
        {
            Vector3 scale = GameManager.Instance.GameData.StackBaseScale;
            if (activeStacks.Count>1) // ilk stack scale normalden büyük
            {
                scale = activeStacks[^1].transform.localScale;
            }

            return scale;
        }
    }

    #endregion
}