#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Wanko.Controllers;

namespace Wanko.Editor
{
    [CustomEditor(typeof(UIController))]
    public sealed class UIControllerEditor : UnityEditor.Editor
    {
        [field: SerializeField]
        public VisualTreeAsset VisualTree { get; private set; }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            VisualTree.CloneTree(root);

            return root;
        }
    }
}
#endif