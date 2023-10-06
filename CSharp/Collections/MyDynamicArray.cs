using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// PascalCase : 사용자 정의 자료형 이름, 함수 이름, 프로퍼티 이름, public/protected 멤버 변수 이름
// camelCase : 지역변수, (Unity API 활용시 public/protected 멤버 변수 이름도 이렇게 주로 씀)
// _camelCase : private 멤버 변수 이름
// snake_case : 상수 정의시
// UPPER_SNAKE_CASE : 상수 정의시
// m_Hungerian , iNum, fX <- 요즘에는 잘안씀(가독성이 별로 좋지못함) : static 멤버변수 s_Instance

namespace Collections
{
    internal class MyDynamicArray
    {
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();

                _items[index] = value;
            }
        }
        public int Count => _count; //내가 실제로 추가한 아이템 갯수
        public int Capacity => _items.Length; // 이 배열의 길이

        private int _count;
        private const int DEFAULT_SIZE = 1;
        private object[] _items = new object[DEFAULT_SIZE];

        public void Add(object item)
        {
            if (_count >= _items.Length)
            {
                object[] tmp = new object[_count * 2]; //만약 아이템을 삽입할 공간이 부족하다면, 두배짜리 더 큰 배열을 만든다
                Array.Copy(_items, tmp, _count); //생성된 배열에 기존 아이템들 복제
                _items = tmp; //배열 참조를 새로 만들어진 배열로 바꿈
            }

            _items[_count++] = item; //가장 마지막에 삽입하려는 아이템 추가
        }

        public object Find(Predicate<object> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }
            return default;
        }

        public int FindIndex(Predicate<object> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }
            return -1;
        }

        public bool Contanins(object item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i] == item)
                    return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException();

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
        }

        public bool Remove(object item)
        {
            int index = FindIndex(x => x == item);

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }
    }
}
