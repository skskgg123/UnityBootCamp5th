using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    // UI Text ������Ʈ
    public TextMeshProUGUI scoreText;

    public void UpdateScoreText()
    {
        // UI Text�� ���� ���� ǥ��
        if (scoreText != null)
        {
            scoreText.text = scoreText.ToString();
        }
    }
}
