using UnityEngine;


namespace Case_2
{
    public class StackData
    {
        public Vector3 LocalPosition { get; private set; }
        public Vector3 LocalScale { get; private set; }
        public Material Material { get; private set; }
        
        public StackData(Vector3 localPosition,Vector3 localScale,Material material)
        {
             LocalPosition = localPosition;
             LocalScale = localScale;
             Material = material;
        }


        
        
    }
}