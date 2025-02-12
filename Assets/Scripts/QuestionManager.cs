using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button continueButton;
    public Button[] answerButtons;
    public TextMeshProUGUI lifeText; // Texto de la vida

    private List<QuestionData> questions = new List<QuestionData>();
    private int currentQuestionIndex = 0;
    private int health = 15; // Vida inicial

    void Start()
    {
        LoadCSV("Lesión de rodilla - Full 1"); // Cargar el CSV
        ShowQuestion(); // Mostrar la primera pregunta
        continueButton.onClick.AddListener(ShowAnswers);
        UpdateLifeText();
    }

    void LoadCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null)
        {
            Debug.LogError("No se encontró el archivo CSV.");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Omitir la cabecera
        {
            string[] values = lines[i].Split(',');
            if (values.Length < 7) continue; // Verificar estructura

            QuestionData question = new QuestionData
            {
                questionText = values[0],
                answers = new string[] { values[1], values[3], values[5] },
                scoreChanges = new int[]
                {
                    int.Parse(values[2]),
                    int.Parse(values[4]),
                    int.Parse(values[6])
                }
            };
            questions.Add(question);
        }
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            EndGame();
            return;
        }

        questionText.text = questions[currentQuestionIndex].questionText;
        questionText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);

        foreach (Button btn in answerButtons)
            btn.gameObject.SetActive(false);
    }

    void ShowAnswers()
    {
        questionText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentQuestionIndex].answers[i];
            int scoreChange = questions[currentQuestionIndex].scoreChanges[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => SelectAnswer(scoreChange));

            answerButtons[i].gameObject.SetActive(true);
        }
    }

    void SelectAnswer(int scoreChange)
    {
        health += scoreChange;
        UpdateLifeText();

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
            ShowQuestion();
        else
            EndGame();
    }

    void UpdateLifeText()
    {
        lifeText.text = "Dias Lesionado: " + health;
    }

    void EndGame()
    {
        // Ocultar todos los elementos excepto la vida final
        questionText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        foreach (Button btn in answerButtons)
            btn.gameObject.SetActive(false);

        // Mostrar la vida en el centro y en grande
        lifeText.gameObject.SetActive(true);
        lifeText.fontSize = 200; // Hacer la fuente más grande
        lifeText.alignment = TextAlignmentOptions.Center;
        lifeText.rectTransform.anchoredPosition = Vector2.zero; // Centrar en pantalla
    }
}

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public string[] answers;
    public int[] scoreChanges;
}