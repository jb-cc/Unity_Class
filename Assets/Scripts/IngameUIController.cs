using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameUIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (scoreText == null)
        {
            // Find the TextMeshProUGUI component in the scene
            scoreText = FindObjectOfType<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {GameManager.Instance.Score}\n\n" +
                           $"Health: {GameManager.Instance.PlayerHealth}\n";
        }
    }
}
