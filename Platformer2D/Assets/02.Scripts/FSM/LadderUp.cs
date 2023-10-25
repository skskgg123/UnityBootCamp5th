using UnityEngine;

namespace Platformer.FSM.Character
{
    public class LadderUp : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.LadderUp;
        public override bool canExecute => base.canExecute &&
                                           (machine.currentStateID == CharacterStateID.Idle ||
                                            machine.currentStateID == CharacterStateID.Move ||
                                            machine.currentStateID == CharacterStateID.Jump ||
                                            machine.currentStateID == CharacterStateID.DoubleJump ||
                                            machine.currentStateID == CharacterStateID.DownJump ||
                                            machine.currentStateID == CharacterStateID.DownJump) &&
                                            controller.isUpLadderDetected;

        private float _upDistance;
        private float _upStartPosY;

        public LadderUp(CharacterMachine machine, float upDistance)
            : base(machine)
        {
            _upDistance = upDistance;
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
                controller.isUpLadderDetected == false)
            {
                nextID = CharacterStateID.Idle;
            }

            return nextID;
        }
    }
}