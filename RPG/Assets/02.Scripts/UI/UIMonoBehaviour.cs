using RPG.GameSystems;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public abstract class UIMonoBehaviour : MonoBehaviour, IUI, IInitializable
    {
        public int sortingOrder 
        { 
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }
        public bool inputActionEnabled { get; set; }

        public event Action onShow;
        public event Action onHide;

        protected Canvas canvas;


        public void InputAction()
        {
            
        }

        public void Show()
        {
            UIManager.instance.Push(this);
            gameObject.SetActive(true);
            onShow?.Invoke();
        }

        public void Hide()
        {
            UIManager.instance.Pop(this);
            gameObject.SetActive(false);
            onHide?.Invoke();
        }

        public void Init()
        {
            canvas = GetComponent<Canvas>();
            UIManager.instance.Register(this);
        }

        private void Update()
        {
            if (inputActionEnabled)
                InputAction();
        }
    }
}