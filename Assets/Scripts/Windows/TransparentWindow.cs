using System;
using UnityEngine;
using UnityEngine.Events;
#if !UNITY_EDITOR
using Wanko.Native;
using static Wanko.Native.User32.SetWindowLongFlags;
using static Wanko.Native.User32.SpecialWindowHandles;
using static Wanko.Native.User32.WindowLongIndexFlags;
using static Wanko.Native.UxTheme;
#endif

namespace Wanko.Windows
{
    [DisallowMultipleComponent]
    internal sealed class TransparentWindow : MonoBehaviour
    {
#if !UNITY_EDITOR
        private IntPtr _hWnd;
#endif
        [field: SerializeField]
        public UnityEvent<IntPtr> SetWindowLong { get; private set; }

#if !UNITY_EDITOR
        private void Awake() =>
            _hWnd = User32.GetActiveWindow();

        private void Start()
        {
            MARGINS margins = new() { cxLeftWidth = -1 };

            DwmApi.DwmExtendFrameIntoClientArea(_hWnd, ref margins);
            User32.SetWindowPos(_hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
        }

        // Optimize; Make default dwNewLong configurable; Abstract and refactor
        private unsafe void LateUpdate()
        {
            User32.SetWindowLongPtr(_hWnd, GWL_EXSTYLE, (void*)(int)(WS_EX_LAYERED | WS_EX_TRANSPARENT));
            SetWindowLong?.Invoke(_hWnd);
        }
#endif
    }
}