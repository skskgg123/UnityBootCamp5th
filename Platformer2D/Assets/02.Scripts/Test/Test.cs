using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Test : UnityEngine.MonoBehaviour
    {
        GameObject _dummy;
        [SerializeField] private List<int> _list;
        private void Awake()
        {
            _dummy = new GameObject();
        }

        private void Reset()
        {
            _list = new List<int>()
            { 
                0, 1, 2, 3, 4
            };
        }

        private void OnEnable()
        {

        }

        private void Start()
        {

        }
    }

    public class Engine
    {
        public static Engine instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Engine();
                return _instance;
            }
        }
        private static Engine _instance;

        List<MonoBehaviour> monos = new List<MonoBehaviour>();

        public void RegisterMono(MonoBehaviour mono)
        {
            // MonoBehaviour �� �����ڿ����ε带 �����ڰ� ���� �����ؼ� 
            // ������ �ʱ�ȭ�κ��� �ۼ��� �� ������. 
            // �ֳ��ϸ� MonoBehaviour �� � GameObject �� Component �μ� �ٿ�������
            // AddComponent �� ���� �Լ��� ���ؼ� ���������� ���ǵ� �����ڰ� ȣ��� ���̱� ������.
            // ���, MonoBehaviour �� ó�� �����ǰ� ���� Awake() ȣ���ϰ� ����. 
            // -> �����ڿ��� �ϴ� �������ʱ�ȭ�� Awake() ���� �����ϸ� ��.
            mono.Awake();
            monos.Add(mono);
        }

        // Game Logic
        void Update()
        {
            foreach (var mono in monos)
            {
                // Update �Ϸ��� MonoBehaviour �� �ѹ��� Start ���� ������
                // Start �ѹ��� ȣ������
                if (mono.hasStarted == false)
                {
                    mono.Start();
                    mono.hasStarted = true;
                }

                mono.Update();
            }
        }

        // Physics Logic
        void FixedUpdate()
        {
            foreach (var mono in monos)
            {
                mono.FixedUpdate();
            }
        }
    }

    public class GameObject
    {
        public bool enable
        {
            get => _enable;
            set
            {
                _enable = value;

                foreach (var mono in monos)
                {
                    if (mono.enable)
                        mono.enable = value;
                }
            }
        }
        private bool _enable;

        List<MonoBehaviour> monos = new List<MonoBehaviour>();

        public void AddComponent<T>()
            where T : MonoBehaviour
        {
            monos.Add(Activator.CreateInstance<T>());
        }
    }

    public class MonoBehaviour
    {
        public bool enable
        {
            get => _enable;
            set
            {
                _enable = value;

                // MonoBehaviour �� Ȱ��/��Ȱ�� �ɶ����� ȣ���� �Լ��� : 
                if (value)
                    OnEnable();
                else
                    OnDisable();
            }
        }
        private bool _enable;
        public bool hasStarted;

        public MonoBehaviour()
        {
            //falfjhsadflaslfj
            Engine.instance.RegisterMono(this);
        }

        public void Awake() { }
        public void OnEnable() { }
        public void Start() { }
        public void Update() { }
        public void FixedUpdate() { }
        public void OnDisable() { }
    }

    
}