using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuizQuestion {
    public string question;
    public string[] options;
    public string answer;
}

[System.Serializable]
public class QuizData {
    public QuizQuestion[] questions;
}

public class PopUpTrigger : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private Button option3Button;
    [SerializeField] private Button option4Button;
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private TextMeshProUGUI correctanswer;
    [SerializeField] private TextMeshProUGUI falseanswer;
    [SerializeField] private GameObject redGate; 
    [SerializeField] private GameObject blueGate;

    private QuizData quizData;
    private int currentQuestionIndex = -1;

    [SerializeField] private healthSystem lifeSystem; // Reference to the LifeSystem script

    void Start()
    {
        canvas.SetActive(false);
        resultCanvas.SetActive(false);
        redGate.GetComponent<Teleporter>().DisableTeleporter(); // Disable the red gate teleporter by default
        blueGate.GetComponent<Teleporter>().DisableTeleporter();
    }

    private void OnInteractButtonClicked()
    {
        string selectedAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        if (selectedAnswer == quizData.questions[currentQuestionIndex].answer)
        {
            Debug.Log("Win");
            resultCanvas.SetActive(true);
            StartCoroutine(HideResultCanvas());
            canvas.SetActive(false);
            correctanswer.gameObject.SetActive(true);
            falseanswer.gameObject.SetActive(false);
            redGate.GetComponent<Teleporter>().EnableTeleporter();
            blueGate.GetComponent<Teleporter>().EnableTeleporter();
            currentQuestionIndex++;
        }
        else
        {
            Debug.Log("Lose");
            resultCanvas.SetActive(true);
            StartCoroutine(HideResultCanvas());
            canvas.SetActive(false);
            correctanswer.gameObject.SetActive(false);
            falseanswer.gameObject.SetActive(true);
            redGate.GetComponent<Teleporter>().DisableTeleporter();
            blueGate.GetComponent<Teleporter>().DisableTeleporter();
            
        }
    }

    private IEnumerator HideResultCanvas()
    {
        yield return new WaitForSeconds(0.75f);
        resultCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AccessButton"))
        {
            if (currentQuestionIndex == -1)
            {
            string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Data/questions.json");
            Debug.Log("JSON string: " + jsonString);
            quizData = JsonUtility.FromJson<QuizData>(jsonString);
            currentQuestionIndex = UnityEngine.Random.Range(0, quizData.questions.Length);
            }
            questionText.text = quizData.questions[currentQuestionIndex].question;
            canvas.SetActive(true);

            string[] options = quizData.questions[currentQuestionIndex].options;
            Debug.Log("Option 0" + options[0]);
            Debug.Log("Option 1" + options[1]);
            Debug.Log("Option 2" + options[2]);
            Debug.Log("Option 3" + options[3]);

            option1Button.GetComponentInChildren<TextMeshProUGUI>().text = options[0];
            option2Button.GetComponentInChildren<TextMeshProUGUI>().text = options[1];
            option3Button.GetComponentInChildren<TextMeshProUGUI>().text = options[2];
            option4Button.GetComponentInChildren<TextMeshProUGUI>().text = options[3];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AccessButton"))
        {
            canvas.SetActive(false);
        }
    }
}
