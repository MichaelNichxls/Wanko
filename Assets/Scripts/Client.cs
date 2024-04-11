//using UnityEngine;
//using UnityEngine.Events;

//namespace Wanko
//{
//    // TODO: Rename
//    public class Client : MonoBehaviour
//    {
//        private ListenerThread _listenerThread;
//        private ClientStatus _clientStatus = ClientStatus.Inactive;

//        [field: SerializeField]
//        public string Protocol { get; private set; } = "inproc";
//        [field: SerializeField]
//        public string Host { get; private set; } = "wanko.nlg";
//        [field: SerializeField]
//        public int Port { get; private set; }
//        [field: SerializeField]
//        public string Topic { get; private set; }
//        [field: SerializeField]
//        public UnityEvent<string> MessageCallback { get; private set; }

//        public enum ClientStatus
//        {
//            Activating,
//            Active,
//            Deactivating,
//            Inactive
//        }

//        private void Start()
//        {
//            _listenerThread = new(Protocol, Host, Port, Topic, MessageCallback);

//            // TODO: Manually attach?
//            ClientEventManager.Instance.OnClientStarting.AddListener(OnClientStarting);
//            ClientEventManager.Instance.OnClientStarted.AddListener(OnClientStarted);
//            ClientEventManager.Instance.OnClientStopping.AddListener(OnClientStopping);
//            ClientEventManager.Instance.OnClientStopped.AddListener(OnClientStopped);

//            ClientEventManager.Instance.OnClientStarting.Invoke();
//        }

//        private void Update()
//        {
//            if (_clientStatus == ClientStatus.Active)
//                _listenerThread.DequeueMessage();
//        }

//        private void OnDestroy()
//        {
//            if (_clientStatus != ClientStatus.Inactive)
//                ClientEventManager.Instance.OnClientStopping.Invoke();
//        }

//        private void OnClientStarting()
//        {
//            Debug.Log("Starting client...");

//            _clientStatus = ClientStatus.Activating;
//            _listenerThread.Start();
//        }

//        private void OnClientStarted()
//        {
//            _clientStatus = ClientStatus.Active;
//            Debug.Log("Client started!");
//        }

//        private void OnClientStopping()
//        {
//            Debug.Log("Stopping client...");

//            _clientStatus = ClientStatus.Deactivating;
//            _listenerThread.Stop();
//        }

//        private void OnClientStopped()
//        {
//            Debug.Log("Client stopped!");
//            _clientStatus = ClientStatus.Inactive;
//        }
//    }
//}