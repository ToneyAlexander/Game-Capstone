using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCC.Commands
{
    public class PaddleMoveDownCommand : AbstractCommand<Player>
    {
        public PaddleMoveDownCommand(Player receiver) : base(receiver) { }

        public override void InvokeCommand()
        {
            //if Gamestate is playing
            Receiver.MoveDown();
        }
    }
}
