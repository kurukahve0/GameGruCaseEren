using System;

namespace Case_1
{
    public static class EventManager
    {
        public static Action<int> OnChangeSizeValue;
        public static Action<SquareController> OnSelectSquare;
        public static Action OnRefreshNeighbour;
        public static Action<int> OnChangeMatchCount;
    }
}