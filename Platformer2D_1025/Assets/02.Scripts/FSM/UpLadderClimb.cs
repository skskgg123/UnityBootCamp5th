using Platformer.GameElements;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM
{
    /// <summary>
    /// ���� ������ ��ٸ��� Ÿ�� ����
    /// </summary>
    public class UpLadderClimb : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.UpLadderClimb;

        public override bool canExecute => base.canExecute &&
                                           (machine.currentStateID == CharacterStateID.Idle ||
                                            machine.currentStateID == CharacterStateID.Move ||
                                            machine.currentStateID == CharacterStateID.Dash ||
                                            machine.currentStateID == CharacterStateID.Jump ||
                                            machine.currentStateID == CharacterStateID.Fall ||
                                            machine.currentStateID == CharacterStateID.DownJump ||
                                            machine.currentStateID == CharacterStateID.DoubleJump) &&
                                           controller.isUpLadderDetected;

        private Ladder _ladder; // ��ٸ� Ÿ�� ���� ��ٸ� ������ �ȵ� �� �����Ƿ� ó�� Ÿ�� �����Ҷ� ĳ��
        private float _vertical; // ��Ʈ�ѷ��� ���� �Է����� ������ ����
        private bool _doExit;

        public UpLadderClimb(CharacterMachine machine) : base(machine)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.hasDoubleJumped = false;
            controller.Stop();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            animator.Play("LadderUp");
            animator.speed = 0.0f;

            _ladder = controller.upLadder;
            // �÷��̾��� ��ġ�� ��ٸ� ����Ÿ�� ���������� ������ ������ġ �״��, ������ ���������� �̵�
            Vector2 startPos = transform.position.y > _ladder.upEnter.y ? new Vector2(_ladder.top.x, transform.position.y) : _ladder.upEnter;
            transform.position = startPos;
            _doExit = false;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            animator.speed = 1.0f;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            // �������� ������ȯ��
            if (_vertical <= 0 && controller.vertical > 0)
            {
                animator.Play("LadderUp");
            }
            // �Ʒ������� ������ȯ��
            else if (_vertical >= 0 && controller.vertical < 0)
            {
                animator.Play("LadderDown");
            }

            _vertical = controller.vertical;
            animator.speed = Mathf.Abs(_vertical); // ���� �Է¿� ���� �ִϸ��̼� ���


            controller.hasJumped = controller.horizontal == 0.0f; // �����Է� ���ý� ���� �����ϵ���

            if (_doExit)
                nextID = CharacterStateID.Idle;

            return nextID;
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();

            transform.position += Vector3.up * _vertical * Time.fixedDeltaTime;

            if (transform.position.y >= _ladder.upExit.y)
            {
                transform.position = _ladder.top;
                _doExit = true;
            }
            else if (transform.position.y <= _ladder.downExit.y)
            {
                _doExit = true;
            }
        }
    }
}