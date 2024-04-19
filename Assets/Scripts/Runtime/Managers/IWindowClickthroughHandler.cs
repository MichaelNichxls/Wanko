using UnityEngine;

namespace Wanko.Runtime.Managers
{
    public interface IWindowClickthroughHandler
    {
        bool SetClickthrough(Vector3 position);
    }
}