using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterballEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image arrowImage;
    public Image additionalImage;
    public TextMeshProUGUI buttonText;
    public float offsetY = 10f; // Arrow 소환 높이 조절 변수

    private Animator buttonAnimator;
    private string buttonSpecificText;

    private void Start()
    {
        arrowImage.enabled = false;
        additionalImage.enabled = false;

        // 버튼의 Animator를 가져오기
        buttonAnimator = GetComponent<Animator>();

        // 각 버튼에 따른 특정 텍스트 설정
        if (gameObject.name == "Button1")
        {
            buttonSpecificText = "Play Game";
        }
        else if (gameObject.name == "Button2")
        {
            buttonSpecificText = "Exit Game";
        }
        else if (gameObject.name == "Button3")
        {
            buttonSpecificText = "Option";
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("마우스가 버튼 위에 올라갔습니다.");

        // 상태 변경 및 처리
        buttonAnimator.SetBool("isAnim", true);

        // 이미지와 텍스트 표시
        ShowArrowImage();
        ShowAdditionalImage();
        ShowButtonText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 상태 변경 및 처리
        buttonAnimator.SetBool("isAnim", false);

        HideArrowImage();
        HideAdditionalImage();
        HideButtonText();
    }

    void ShowArrowImage()
    {
        arrowImage.enabled = true;

        RectTransform buttonRectTransform = GetComponent<RectTransform>();
        RectTransform arrowRectTransform = arrowImage.rectTransform;
        arrowRectTransform.position = new Vector3(buttonRectTransform.position.x, buttonRectTransform.position.y + buttonRectTransform.rect.height + offsetY, buttonRectTransform.position.z);
    }

    void HideArrowImage()
    {
        arrowImage.enabled = false;
    }

    void ShowAdditionalImage()
    {
        additionalImage.enabled = true;
    }

    void HideAdditionalImage()
    {
        additionalImage.enabled = false;
    }

    void ShowButtonText()
    {
        buttonText.enabled = true;
        buttonText.text = buttonSpecificText;
    }

    void HideButtonText()
    {
        buttonText.enabled = false;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
