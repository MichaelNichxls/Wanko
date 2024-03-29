# inproc_comm.py

import zmq

from threading import Event, Thread
from typing import Optional, override

# TODO: IPC later down the line
# NOTE: Decorators are a thing
class PipelineThread(Thread):
    # TODO: Pass config instance through ctor
    # TODO: default addrs?
    def __init__(
        self,
        context: Optional[zmq.Context] = None,
        *,
        recv_addr: Optional[str] = None, # make actual params optional
        send_addr: Optional[str] = None
    ):
        super().__init__()

        self.stop_event = Event()

        self.recv_addr = recv_addr
        self.send_addr = send_addr
        self.context: zmq.Context = context or zmq.Context.instance()
        self.pull_socket: zmq.Socket = self.context.socket(zmq.PULL)
        self.push_socket: zmq.Socket = self.context.socket(zmq.PUSH)

        self.poller = zmq.Poller()
        self.poller.register(self.pull_socket, zmq.POLLIN) # | POLLOUT
        self.poller.register(self.push_socket, zmq.POLLIN) # | POLLOUT
    
    def __enter__(self):
        return self
    
    def __exit__(self, *exc_info):
        # TODO: Log
        self.stop_event.set()

        self.poller.unregister(self.pull_socket)
        self.poller.unregister(self.push_socket)

        self.pull_socket.close()
        self.push_socket.close()
    
    @override
    def run(self) -> None:
        # TODO: Log
        if self.recv_addr is not None:
            self.pull_socket.connect(self.recv_addr)
        
        if self.send_addr is not None:
            self.push_socket.bind(self.send_addr)
    
    # def set_exit_event()