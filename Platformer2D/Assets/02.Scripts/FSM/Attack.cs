using Platformer.Animations;
using Platformer.Stats;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platformer.FSM.Character
{
    public class Attack : CharacterStateBase
    {  
        public override CharacterStateID id => CharacterStateID.Attack;
        public override bool canExecute
        {
            get
            {
                if (base.canExecute == false)
                    return false;

                // �޺������� �׿��ִ� ��Ȳ���� ������ ������ �ð����� ����� �ð��� �޺� �ʱ�ȭ �ð��� �Ѿ���� �޺� �ȵ�
                float elapsedTime = Time.time - _exitTimeMark; // ������ �������� ����� �ð�
                if (_comboStack > 0 && elapsedTime >= _comboResetTime )
                {
                    _comboStack = 0;
                    return false;
                }

                // ���� ������ �ִ�ġ�� ������ �޺� �ȵ�
                if (_comboStack > _comboStackMax)
                {
                    return false;
                }

                // ùŸ�� ������ ����
                // �ļ�Ÿ�� ���� ���� ��Ʈ���� ���� ����
                if ((_comboStack == 0 || (_comboStack > 0 && _hasHit)) &&
                    (machine.currentStateID == CharacterStateID.Idle ||
                     machine.currentStateID == CharacterStateID.Move ||
                     machine.currentStateID == CharacterStateID.Crouch ||
                     machine.currentStateID == CharacterStateID.Jump ||
                     machine.currentStateID == CharacterStateID.DoubleJump ||
                     machine.currentStateID == CharacterStateID.DownJump ||
                     machine.currentStateID == CharacterStateID.Fall))
                {
                    return true;
                }

                return false;
            }
        }


        private int _comboStackMax;    // �ִ� �޺� ����
        private int _comboStack;       // ���� �޺� ����
        private float _comboResetTime; // ���� ���� �޺� �ʱ�ȭ �ð�
        private float _exitTimeMark;   // ������ ���� ���� �ð�
        private bool _hasHit;          // ���� ���� ��Ʈ���� �ߴ��� ? 

        public class AttackSetting
        {
            public int targetMax; // �ִ� Ÿ���� ��
            public LayerMask targetMask; // Ÿ�� ���� ����ũ
            public float damageGain; // ���� ���
            public Vector2 castCenter; // Ÿ�� ���� ����(�簢��) ���� �߽�
            public Vector2 castSize; // Ÿ�� ���� ���� ũ��
            public float castDistance; // Ÿ�� ���� ���� �� �Ÿ�
        }

        private AttackSetting[] _attackSettings;
        private List<IHp> _targets = new List<IHp>();
        private CharacterAnimationEvents _animationEvents;

        public Attack(CharacterMachine machine, AttackSetting[] attackSettings, float comboResetTime) : base(machine)
        {
            _attackSettings = attackSettings;
            _comboResetTime = comboResetTime;
            _comboStackMax = attackSettings.Length -1;
            _animationEvents = animator.GetComponent<CharacterAnimationEvents>();
            _animationEvents.onHit = () =>
            {
                foreach (var target in _targets)
                {
                    if (target == null)
                        continue;

                    float damage = Random.Range(controller.damageMin, controller.damageMax) * _attackSettings[_comboStack -1].damageGain;
                    target.DepleteHp(controller, damage);
                }
                _hasHit = true;
            };
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = controller.isGrounded;
            _hasHit = false;

            AttackSetting setting = _attackSettings[_comboStack - 1];
            RaycastHit2D[] hits =
                Physics2D.BoxCastAll(origin: rigidbody.position + new Vector2(setting.castCenter.x * controller.direction, setting.castCenter.y),
                                     size: setting.castSize,
                                     angle: 0.0f,
                                     direction: Vector2.right * controller.direction,
                                     distance: setting.castDistance,
                                     layerMask: setting.targetMask);

            Vector2 origin = rigidbody.position + new Vector2(setting.castCenter.x * controller.direction, setting.castCenter.y);
            Vector2 size = setting.castSize;
            float distance = setting.castDistance;
            // L-T -> R-T
            Debug.DrawLine(origin + new Vector2(-size.x / 2.0f * controller.direction, +size.y / 2.0f),
                           origin + new Vector2(+size.x / 2.0f * controller.direction, +size.y / 2.0f) + Vector2.right * controller.direction * distance);
            // L-B -> R-B
            Debug.DrawLine(origin + new Vector2(-size.x / 2.0f * controller.direction, -size.y / 2.0f),
                           origin + new Vector2(+size.x / 2.0f * controller.direction, -size.y / 2.0f) + Vector2.right * controller.direction * distance);
            // L-T -> L-B
            Debug.DrawLine(origin + new Vector2(-size.x / 2.0f * controller.direction, +size.y / 2.0f),
                           origin + new Vector2(-size.x / 2.0f * controller.direction, -size.y / 2.0f));
            // R-T -> R-B
            Debug.DrawLine(origin + new Vector2(+size.x / 2.0f * controller.direction, +size.y / 2.0f) + Vector2.right * controller.direction * distance,
                           origin + new Vector2(+size.x / 2.0f * controller.direction, -size.y / 2.0f) + Vector2.right * controller.direction * distance);

            // ��ü ������ ���̵� �߿��� �ִ� Ÿ�� �� ������ ������� ���
            _targets.Clear();
            for (int i = 0; i < hits.Length; i++)
            {
                if (_targets.Count >= setting.targetMax)
                    break;

                if (hits[i].collider.TryGetComponent(out IHp target))
                    _targets.Add(target);
            }

            animator.SetFloat("comboStack", _comboStack++); // �ִϸ��̼� �Ķ���� ���� �� �޺����� �ױ�
            animator.Play("Attack");


        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _exitTimeMark = Time.time;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                nextID = CharacterStateID.Idle;

            return nextID;
            
        }
    }
}