using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCommand<TReceiver> : ICommand
{
    private TReceiver receiver;
    protected AbstractCommand(TReceiver receiver)
    {
        this.receiver = receiver;
    }

    protected TReceiver Receiver
    {
        get { return receiver; }
    }

    public abstract void InvokeCommand();
}