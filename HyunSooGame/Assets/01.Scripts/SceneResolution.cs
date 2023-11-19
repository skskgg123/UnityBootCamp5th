using UnityEngine;

public class CameraDisplaySwitch : MonoBehaviour
{
    // Display�� ��ȯ�� �� ����� ���÷��� �ε���
    public int targetDisplayIndex = 2;

    void Start()
    {
        SwitchDisplayForAllCameras();
    }

    void SwitchDisplayForAllCameras()
    {
        // ���� ���� �ִ� ��� ī�޶� �迭
        Camera[] cameras = Camera.allCameras;

        // ��� ī�޶� ���� ����
        foreach (Camera camera in cameras)
        {
            // ī�޶��� Display�� ��ȯ
            camera.targetDisplay = targetDisplayIndex;
        }
    }
}