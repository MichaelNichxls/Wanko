using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wanko.Window;

namespace Wanko.Editor.MenuItems
{
    public static class WindowMenuItem
    {
        [MenuItem("GameObject/Window/Window Manager", false, 10)]
        public static void CreateWindowManager(MenuCommand command)
        {
            GameObject parent = command.context as GameObject;
            StageHandle stage = parent == null
                ? StageUtility.GetCurrentStageHandle()
                : StageUtility.GetStageHandle(parent);

            if (stage.FindComponentOfType<WindowManager>() is var windowManager && windowManager == null)
            {
                GameObject gameObject = new(nameof(WindowManager));
                windowManager = gameObject.AddComponent<WindowManager>();

                if (parent == null)
                    StageUtility.PlaceGameObjectInCurrentStage(gameObject);
                else
                    GameObjectUtility.SetParentAndAlign(gameObject, parent);

                Undo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");
            }

            Selection.activeGameObject = windowManager.gameObject;
        }
    }
}