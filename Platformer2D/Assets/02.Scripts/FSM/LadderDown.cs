using UnityEngine;

namespace Platformer.FSM.Character
{
    public class LadderDown : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.LadderDown;
        public override bool canExecute => base.canExecute &&
                                           (machine.currentStateID == CharacterStateID.Idle ||
                                            machine.currentStateID == CharacterStateID.Move ||
                                            machine.currentStateID == CharacterStateID.Jump ||
                                            machine.currentStateID == CharacterStateID.DoubleJump ||
                                            machine.currentStateID == CharacterStateID.DownJump ||
                                            machine.currentStateID == CharacterStateID.DownJump) &&
                                            controller.isDownLadderDetected;

        private float _downDistance;
        private float _downStartPosY;

        public LadderDown(CharacterMachine machine, float downDistance)
            : base(machine)
        {
            _downDistance = downDistance;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = true;
            controller.isMovable = true;
            controller.hasJumped = true;
            controller.hasDoubleJumped = true;
            controller.Stop();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            animator.Play("Ladder");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (controller.isGrounded ||
                controller.isDownLadderDetected == false)
            {
                nextID = CharacterStateID.Idle;
            }

            return nextID;
        }
    }
}