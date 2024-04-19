using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Wanko.Runtime.Managers;
using Wanko.Runtime.Window;

namespace Wanko.Editor.MenuItems
{
    public static class WindowMenuItems
    {
        [MenuItem("GameObject/Window/Transparent Window", false, 10)]
        public static void CreateTransparentWindow(MenuCommand command)
        {
            CreateWindowManager(new MenuCommand(null));

            GameObject gameObject = new(nameof(TransparentWindow));
            gameObject.AddComponent<TransparentWindow>();

            GameObjectUtility.SetParentAndAlign(gameObject, command.context as GameObject);
            Undo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");

            Selection.activeGameObject = gameObject;
        }

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