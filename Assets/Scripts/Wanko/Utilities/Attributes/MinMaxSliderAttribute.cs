using UnityEngine;

namespace Wanko.Utilities.Attributes
{
    // TODO: move
    public sealed class MinMaxSliderAttribute : PropertyAttribute
    {
        public float Min { get; }
        public float Max { get; }

        public MinMaxSliderAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}