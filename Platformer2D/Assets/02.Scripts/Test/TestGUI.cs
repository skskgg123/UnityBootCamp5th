using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = Platformer.Controllers.CharacterController;

namespace Platformer.Test
{
    public class TestGUI : MonoBehaviour
    {
// if ��ó���⹮ : if ������ ���� ��� �ش� ���� ������ ��. �ƴϸ� ����.
#if UNITY_EDITOR
        [SerializeField] private CharacterController _controller;

        private void Awake()
        {
            // Find �� �׽�Ʈ�Ҷ����� �����ؾ���.. ���̾��Ű ��ü Ž���ϹǷ� ���� ������
            if ((GameObject.Find("Player")?.TryGetComponent(out _controller) ?? false) == false)
            {
                Debug.LogWarning($"[TestGUI] : Failed to get Player component.");
            }
        }

        private void OnGUI()
        {
            GUI.Box(new Rect(10.0f, 10.0f, 180.0f, 140.0f), "Test");

            if (GUI.Button(new Rect(20.0f, 40.0f, 140.0f, 80.0f), "Hurt"))
            {
                _controller?.DepleteHp(null, 10.0f);
            }
        }
#endif
    }
}