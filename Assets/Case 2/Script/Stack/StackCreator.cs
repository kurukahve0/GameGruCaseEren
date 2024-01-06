using System;
using System.Collections.Generic;
using Case_2.Script.Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Case_2
{
    public class StackCreator : MonoBehaviour
    {
        [SerializeField] private Transform stackParent;
        public List<StackController> activeStack;
        private float zDistance;


        private void Start()
        {
            zDistance = activeStack[0].MeshRenderer.bounds.size.z;
        }

        public void CreateStack()
        {
            float xBackSize = activeStack[^2].MeshRenderer.bounds.size.x;
            float xFrontSize = activeStack[^1].MeshRenderer.bounds.size.x;
            float distanceStacks =
                Mathf.Abs(activeStack[^2].transform.localPosition.x - activeStack[^1].transform.localPosition.x);


            bool isRight = activeStack[^2].transform.localPosition.x - activeStack[^1].transform.localPosition.x < 0; // sağ sol ayarı için


            CreateNecessaryStackPiece(xBackSize, distanceStacks, isRight);
            CreateTrashStackPiece(xBackSize, xFrontSize, distanceStacks, isRight);
        }


        public void CreateNewStack()
        {
            var stack = Instantiate(GameManager.Instance.GameData.StackPrefab, stackParent);
            Vector3 stackPosition = Vector3.zero;
            stackPosition.z += (activeStack.Count - 1) * zDistance;
            stackPosition.x = GameManager.Instance.GameData.CreateXPositions[Random.Range(0, 2)];
            stack.transform.localPosition = stackPosition;
            activeStack.Add(stack);
            stack.IsMovementOpen = true;
        }

        void CreateNecessaryStackPiece(float xBackSize, float distanceStacks, bool isRight)
        {
            RemoveLastStack();

            int factor = isRight ? 1 : -1; // sağsol pozisyon ayarlamak için

            var stack = Instantiate(GameManager.Instance.GameData.StackPrefab, stackParent);
            Vector3 stackPosition = activeStack[^1].transform.localPosition;
            stackPosition.z += (activeStack.Count) * zDistance;
            stackPosition.x = activeStack[^1].transform.localPosition.x + factor * distanceStacks / 2f;
            stack.transform.localPosition = stackPosition;
            stack.transform.localScale = new Vector3(xBackSize - distanceStacks, stack.transform.localScale.y,
                stack.transform.localScale.z);
            activeStack.Add(stack);
        }

        void CreateTrashStackPiece(float xBackSize, float xFrontSize, float distanceStacks, bool isRight)
        {
            int factor = isRight ? 1 : -1; // sağsol pozisyon ayarlamak için
            var stack = Instantiate(GameManager.Instance.GameData.StackPrefab, stackParent);
            Vector3 stackPosition = activeStack[^2].transform.localPosition;
            stackPosition.z += (activeStack.Count - 1) * zDistance;
            stackPosition.x = activeStack[^2].transform.localPosition.x + factor * xFrontSize / 2 +
                              factor * distanceStacks / 2f;
            stack.transform.localPosition = stackPosition;
            stack.transform.localScale = new Vector3(xFrontSize - (xBackSize - distanceStacks),
                stack.transform.localScale.y, stack.transform.localScale.z);
            
            stack.OpenPhysics(isRight);
        }
        

        void RemoveLastStack()
        {
            var lastStack = activeStack[^1];
            activeStack.Remove(lastStack);
            lastStack.gameObject.SetActive(false);
        }
    }
}