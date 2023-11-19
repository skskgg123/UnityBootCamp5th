using UnityEngine;

public class CameraDisplaySwitch : MonoBehaviour
{
    // Display를 전환할 때 사용할 디스플레이 인덱스
    public int targetDisplayIndex = 2;

    void Start()
    {
        SwitchDisplayForAllCameras();
    }

    void SwitchDisplayForAllCameras()
    {
        // 현재 씬에 있는 모든 카메라 배열
        Camera[] cameras = Camera.allCameras;

        // 모든 카메라에 대해 설정
        foreach (Camera camera in cameras)
        {
            // 카메라의 Display를 전환
            camera.targetDisplay = targetDisplayIndex;
        }
    }
}