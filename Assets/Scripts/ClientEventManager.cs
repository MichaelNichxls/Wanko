using UnityEngine;
using UnityEngine.Events;

namespace Wanko
{
    // TODO: Attach to different GameObject
    // TODO: Rename and redo this class
    public class ClientEventManager : MonoBehaviour
    {
        public static ClientEventManager Instance;

        // TODO: Rename
        [field: SerializeField]
        public UnityEvent OnClientStarting { get; private set; }
        [field: SerializeField]
        public UnityEvent OnClientStarted { get; private set; }
        [field: SerializeField]
        public UnityEvent OnClientStopping { get; private set; }
        [field: SerializeField]
        public UnityEvent OnClientStopped { get; private set; }

        private void Awake() =>
            Instance = this;
    }
}