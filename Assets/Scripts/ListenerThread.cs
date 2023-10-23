using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine.Events;

namespace Wanko
{
    // TODO: Make interface
    public class ListenerThread
    {
        private readonly string _protocol;
        private readonly string _host;
        private readonly int _port;
        private readonly string _topic;
        private readonly UnityEvent<string> _messageCallback;
        private readonly ConcurrentQueue<string> _messageQueue = new();

        private Thread _clientThread;
        private bool _isClientCanceled;

        // TODO: Make default param
        // TODO: Null annotations
        public ListenerThread(string protocol, string host, int port, string topic = "", UnityEvent<string> messageCallback = null)
        {
            _protocol = protocol;
            _host = host;
            _port = port;
            _topic = topic;
            _messageCallback = messageCallback;
        }

        // TODO: Make interface
        public void Start()
        {
            _isClientCanceled = false;
            _clientThread = new Thread(Listener);
            _clientThread.Start();

            ClientEventManager.Instance.OnClientStarted.Invoke();
        }

        public void Stop()
        {
            _isClientCanceled = true;
            _clientThread?.Join();
            _clientThread = null;

            ClientEventManager.Instance.OnClientStopped.Invoke();
        }

        private void Listener()
        {
            ForceDotNet.Force();

            using (SubscriberSocket subscriber = new())
            {
                subscriber.Connect($"{_protocol}://{_host}:{_port}");
                subscriber.Subscribe(_topic);

                while (!_isClientCanceled)
                {
                    // TODO: Exception handling
                    if (!subscriber.TryReceiveFrameString(out string _))
                        continue;

                    subscriber.TryReceiveFrameString(out string message);
                    _messageQueue.Enqueue(message);
                }
            }

            NetMQConfig.Cleanup();
        }

        public void DequeueMessage()
        {
            while (!_messageQueue.IsEmpty)
            {
                if (_messageQueue.TryDequeue(out string message))
                {
                    UnityEngine.Debug.Log(message);
                    _messageCallback?.Invoke(message);
                }
                else
                    break;
            }
        }
    }
}