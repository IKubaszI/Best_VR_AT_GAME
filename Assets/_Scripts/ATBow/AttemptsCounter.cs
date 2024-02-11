using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptsCounter : MonoBehaviour
{
    public int maxAttempts = 5;
    private int currentAttempts;
    public TMP_Text attemptsText;

    public int CurrentAttempts
    {
        get { return currentAttempts; }
        set { currentAttempts = value; }
    }

    void Start()
    {
        currentAttempts = maxAttempts;
        attemptsText = GetComponent<TMP_Text>();
        UpdateAttemptsText();
    }

    public bool DecreaseAttempt()
    {
        if (currentAttempts > 0)
        {
            currentAttempts--;
            UpdateAttemptsText();

            if (currentAttempts <= 0)
            {
                currentAttempts = 0;
            }
            return true;
        }
        return false;
    }

 public   void UpdateAttemptsText()
    {
        attemptsText.text = "Strzały: 0" + currentAttempts.ToString();
    }
}
