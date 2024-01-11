using System.Collections.Generic;
using Case_2.Data;
using Lean.Pool;
using UnityEngine;

namespace Case_2
{
    public class LevelController : MonoBehaviour
    {
        #region Variable

        public StackController NewStack { get; private set; }

        public List<StackController> activeStacks;

        public Transform FinishLineTransform => finishLineTransform;

        // iki stack arasındaki z pozisyon farkı
        private float ZDistance => activeStacks[^1].transform.localPosition.z + activeStacks[^1].ZBoundsSize / 2;
        private GameData GameData => GameManager.Instance.GameData;

        private int materialCounter;

        [Header("Definitions")] [SerializeField]
        private Transform finishLineTransform;

        #endregion

        #region StackCreate

        public void CreateStackPiece()
        {
            if (!NewStack)
                return;

            float xBackSize = activeStacks[^1].XBoundsSize;
            float xFrontSize = NewStack.XBoundsSize;
            float distanceStacks =
                Mathf.Abs(NewStack.transform.localPosition.x - activeStacks[^1].transform.localPosition.x);


            bool isRight =
                activeStacks[^1].transform.localPosition.x - NewStack.transform.localPosition.x <
                0; // sağ sol ayarı için


            if (distanceStacks > xBackSize) // yerleştirememe durumu
            {
                FallNewStack();
            }
            else if (distanceStacks < GameData.StacekCreateTolerance) // toleranslı yerleştirme durumu
            {
                PerfectPlacementForNewStack();
            }
            else // iki farklı stack durumu
            {
                CreateTrashStackPiece(xBackSize, xFrontSize, distanceStacks, isRight);
                CreateNecessaryStackPiece(xBackSize, distanceStacks, isRight);
            }

            materialCounter++;
            LevelManager.Instance.IsStackCreateOpen = false;
        }


        public void CreateFullPieceStack()
        {
            NewStack = LeanPool.Spawn(GameData.StackPrefab, transform);
            Vector3 stackPosition = Vector3.zero;
            stackPosition.z = ZDistance + NewStack.ZBoundsSize / 2;
            stackPosition.x = GameData.CreateXPositions[Random.Range(0, 2)];
            StackData transformData = new StackData(stackPosition, GetNewStackScale(), GetStackMaterial());
            NewStack.SetStackData(transformData);
            NewStack.IsMovementOpen = true;
        }

        void CreateNecessaryStackPiece(float xBackSize, float distanceStacks, bool isRight)
        {
            RemoveNewStack();

            int factor = isRight ? 1 : -1; // sağsol pozisyon ayarlamak için

            var stack = LeanPool.Spawn(GameData.StackPrefab, transform);
            // bir önceki stacke göre boyut ve Pozisyon ayaralma
            Vector3 stackPosition = activeStacks[^1].transform.localPosition;
            stackPosition.z = ZDistance + stack.ZBoundsSize / 2;
            stackPosition.x += factor * distanceStacks / 2f;

            Vector3 stackScale = new Vector3(xBackSize - distanceStacks
                , stack.transform.localScale.y
                , stack.transform.localScale.z);

            StackData transformData = new StackData(stackPosition, stackScale, GetStackMaterial());
            stack.SetStackData(transformData);
            activeStacks.Add(stack);
            SoundManager.Instance.PlaySound(SoundType.CutSound, false);
        }

        void CreateTrashStackPiece(float xBackSize, float xFrontSize, float distanceStacks, bool isRight)
        {
            int factor = isRight ? 1 : -1; // sağsol pozisyon ayarlamak için
            var stack = LeanPool.Spawn(GameData.StackPrefab, transform);
            // stack parçasına göre boyut ve Pozisyon ayaralma
            Vector3 stackPosition = activeStacks[^1].transform.localPosition;
            stackPosition.z = ZDistance + stack.ZBoundsSize / 2;
            stackPosition.x += factor * xFrontSize / 2 +
                               factor * distanceStacks / 2f;
            Vector3 stackScale = new Vector3(xFrontSize - (xBackSize - distanceStacks),
                stack.transform.localScale.y, stack.transform.localScale.z);
            StackData transformData = new StackData(stackPosition, stackScale, GetStackMaterial());
            stack.SetStackData(transformData);
            stack.OpenPhysics(!isRight ? Vector3.forward : -Vector3.forward);
        }

        void PerfectPlacementForNewStack()
        {
            Vector3 perfectPositions = activeStacks[^1].transform.localPosition;
            perfectPositions.z = ZDistance + NewStack.ZBoundsSize / 2;
            NewStack.transform.localPosition = perfectPositions;
            activeStacks.Add(NewStack);
            NewStack.IsMovementOpen = false;
            NewStack = null;
            SoundManager.Instance.PlaySound(SoundType.CutSound, true);
        }

        #endregion

        #region StackSetting

        public void ClearStacks()
        {
            for (int i = 1; i < activeStacks.Count; i++)
            {
                LeanPool.Despawn(activeStacks[i]);
            }

            if (NewStack)
                LeanPool.Despawn(NewStack);
        }

        void FallNewStack()
        {
            NewStack.OpenPhysics(Vector3.zero);
        }


        void RemoveNewStack()
        {
            LeanPool.Despawn(NewStack.gameObject);
            NewStack = null;
        }


        Vector3 GetNewStackScale()
        {
            Vector3 scale = GameData.StackBaseScale;
            if (activeStacks.Count > 1) // ilk stack scale normalden büyük
            {
                scale = activeStacks[^1].transform.localScale;
            }

            return scale;
        }

        Material GetStackMaterial()
        {
            return GameData.stackMaterial[materialCounter];
        }

        #endregion
    }
}