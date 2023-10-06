using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // naming convention : I + PascalCase
    // 인터페이스는 청구역할을 해주는 기능들을 추상화 하는 용도의 사용자정의 자료형.
    // 기능의 추상화이므로 각 기능의 구현은 없고 어떤 기능이 있어야 하는지에 대해 선언만 함.
    internal interface IAttacker
    {
        float AttackPower { get; } //float를 반환하기만 하는 get 접근자

        void Attack(IHp target);
    }
}
