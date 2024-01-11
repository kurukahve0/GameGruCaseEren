using System;
using Palmmedia.ReportGenerator.Core.Parser;
using UnityEngine;

namespace Case_2
{
    public class StackTransformData
    {
        public Vector3 LocalPosition { get; private set; }
        public Vector3 LocalScale { get; private set; }
        
        StackTransformData(Vector3 localPosition,Vector3 localScale)
        {
            LocalPosition = localPosition;
            LocalScale = localScale;
        }
    }
}