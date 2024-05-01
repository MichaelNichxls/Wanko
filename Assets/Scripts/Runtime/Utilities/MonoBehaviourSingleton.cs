using UnityEngine;

namespace Wanko.Runtime.Utilities
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : MonoBehaviourSingleton<T>
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this as T;
        }
    }
}