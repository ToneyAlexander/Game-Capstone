using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCC.Commands
{
    public class ReleasePaddleMoveUpCommand : AbstractCommand<Player>
    {
        public ReleasePaddleMoveUpCommand(Player receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            //if Gamestate is playing
            Receiver.ReleaseMoveUp();
        }
    }
}
