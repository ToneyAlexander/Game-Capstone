using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Processes ICommands.
/// </summary>
[CreateAssetMenu]
public sealed class CommandProcessor : ScriptableObject
{
    private readonly Queue<ICommand> commands = new Queue<ICommand>();

    public void ProcessCommand(ICommand command)
    {
        command.InvokeCommand();
        commands.Enqueue(command);
    }
}
