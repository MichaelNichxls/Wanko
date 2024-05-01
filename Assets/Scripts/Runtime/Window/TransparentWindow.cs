using UnityEngine;
#if !UNITY_EDITOR
using Wanko.Runtime.Native;
using static Wanko.Runtime.Native.User32.SetWindowLongFlags;
using static Wanko.Runtime.Native.User32.SpecialWindowHandles;
using static Wanko.Runtime.Native.User32.WindowLongIndexFlags;
using static Wanko.Runtime.Native.UxTheme;
#endif

namespace Wanko.Runtime.Window
{
    [DisallowMultipleComponent]
    public sealed class TransparentWindow : MonoBehaviour
    {
#if !UNITY_EDITOR
        private unsafe void Start()
        {
            MARGINS margins = new() { cxLeftWidth = -1 };

            DwmApi.DwmExtendFrameIntoClientArea(WindowManager.Instance.hWnd, ref margins);
            User32.SetWindowPos(WindowManager.Instance.hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);

            Camera.main.backgroundColor = Color.clear;

            User32.SetWindowLongPtr(WindowManager.Instance.hWnd, GWL_EXSTYLE, (void*)(int)WS_EX_LAYERED);
            WindowManager.Instance.SetClickthrough(true);
        }
#endif
    }
}