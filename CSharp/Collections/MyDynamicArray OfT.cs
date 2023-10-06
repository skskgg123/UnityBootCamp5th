using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Collections
{
    //재네릭으로 갓!
    internal class MyDynamicArray<T>
        where T : IComparable<T> // where 제한자 : 타입을 제한하는 한정자 (T에 넣을 타입은 IComparable<T> 로 공변 가능해야한다.)
    {
        public T this[int index]
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
        private T[] _items = new T[DEFAULT_SIZE];

        public void Add(T item)
        {
            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2]; //만약 아이템을 삽입할 공간이 부족하다면, 두배짜리 더 큰 배열을 만든다
                Array.Copy(_items, tmp, _count); //생성된 배열에 기존 아이템들 복제
                _items = tmp; //배열 참조를 새로 만들어진 배열로 바꿈
            }

            _items[_count++] = item; //가장 마지막에 삽입하려는 아이템 추가
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }
            return default;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }
            return -1;
        }

        public bool Contanins(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                // Default 비교연산 - C# 기본 제공 비교 연산자를 쓸 때
                if (Comparer<T>.Default.Compare(_items[i], item) == 0) 
                    return true;

                // IComparable 비교연산 - 내가 비교연산 내용을 직접 구현해서 쓸 때
                if (item.CompareTo(_items[i]) == 0)
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

        public bool Remove(T item)
        {
            int index = FindIndex(x => item.CompareTo(x) == 0);

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }
    }
}
