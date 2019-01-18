using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCC.Commands
{
    public class PaddleMoveUpCommand : AbstractCommand<Player>
    {
        public PaddleMoveUpCommand(Player receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            //if Gamestate is playing
            Receiver.MoveUp();
        }
    }
}
