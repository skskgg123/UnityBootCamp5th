using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    // UI Text 컴포넌트
    public TextMeshProUGUI scoreText;

    public void UpdateScoreText()
    {
        // UI Text에 현재 점수 표시
        if (scoreText != null)
        {
            scoreText.text = scoreText.ToString();
        }
    }
}
