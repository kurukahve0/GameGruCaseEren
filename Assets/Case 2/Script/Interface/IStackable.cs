using UnityEngine;

namespace Case_2
{
    public interface IStackable
    {
        public bool IsMovementOpen { get; set; }
        public float XBoundsSize { get; }
        public float ZBoundsSize { get; }
        public void OpenPhysics(Vector3 torqueDirection);
        public void SetStackData(StackData stackData);
    }
}