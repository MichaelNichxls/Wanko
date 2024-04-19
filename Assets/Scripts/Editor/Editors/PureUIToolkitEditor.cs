using UnityEngine;
using UnityEngine.UIElements;

namespace Wanko.Editor.Editors
{
    public abstract class PureUIToolkitEditor : UnityEditor.Editor
    {
        [field: SerializeField]
        public VisualTreeAsset VisualTree { get; protected set; }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            root.Add(VisualTree.Instantiate());

            return root;
        }
    }
}