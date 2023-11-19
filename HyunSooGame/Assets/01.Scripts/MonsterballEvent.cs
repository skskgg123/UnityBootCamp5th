using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterballEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image arrowImage;
    public Image additionalImage;
    public TextMeshProUGUI buttonText;
    public float offsetY = 10f; // Arrow ��ȯ ���� ���� ����

    private Animator buttonAnimator;
    private string buttonSpecificText;

    private void Start()
    {
        arrowImage.enabled = false;
        additionalImage.enabled = false;

        // ��ư�� Animator�� ��������
        buttonAnimator = GetComponent<Animator>();

        // �� ��ư�� ���� Ư�� �ؽ�Ʈ ����
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
        Debug.Log("���콺�� ��ư ���� �ö󰬽��ϴ�.");

        // ���� ���� �� ó��
        buttonAnimator.SetBool("isAnim", true);

        // �̹����� �ؽ�Ʈ ǥ��
        ShowArrowImage();
        ShowAdditionalImage();
        ShowButtonText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���� ���� �� ó��
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
