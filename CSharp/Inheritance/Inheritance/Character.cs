﻿using System;

namespace Inheritance
{
    // abstract 키워드 :
    // 추상용도로 사용하는 것이므로 반드시 상속자가 해당 내용을 직접 구현해주어야 한다고 명시.
    internal abstract class Character : IAttacker, IHp
    {
        //? nullable : null 값 할당 가능하다고 명시하는 연산자
        public string? NickName { get; set; }

        //한줄로 생략할 경우 위에 구조
        /*public string NickName
        {
            get
            {
                return _nickName;
            }
            private set
            {
                _nickName = value;
            }
        }
        private string _nickName;*/

        public float Exp
        {
            get
            {
                return _exp;
            }
            private set
            {
                if (value < 0)
                    value = 0;

                _exp = value;
            }
        }

        public float AttackPower
        {
            get
            {
                return _attackPower;
            }
        }

        public float HpValue => throw new NotImplementedException();

        public float HpMax => throw new NotImplementedException();

        public float HpMin => throw new NotImplementedException();

        private float _exp;
        private float _attackPower;

        //======================================================================
        //                        public Methods
        //======================================================================


        public float GetExp()
        {
            return _exp;
        }

        public void SetExp(float value)
        {
            if(value < 0)
                value = 0;

            _exp = value;
        }

        public void Jump()
        {
            Console.WriteLine("Jump!");
        }

        public void Attack(IHp target)
        {
            target.DepleteHp(_attackPower);
        }

        public void RecoverHp(float value)
        {
            throw new NotImplementedException();
        }

        public void DepleteHp(float value)
        {
            throw new NotImplementedException();
        }
    }
}