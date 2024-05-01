using UnityEngine;

namespace Wanko.Runtime.Window
{
    public interface IWindowClickthroughHandler
    {
        bool SetClickthrough(Vector2 position);
    }
}