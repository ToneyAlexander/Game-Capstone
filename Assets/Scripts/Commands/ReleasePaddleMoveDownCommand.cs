using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCC.Commands
{
    public class ReleasePaddleMoveDownCommand : AbstractCommand<Player>
    {
        public ReleasePaddleMoveDownCommand(Player receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            //if Gamestate is playing
            Receiver.ReleaseMoveDown();
        }
    }
}