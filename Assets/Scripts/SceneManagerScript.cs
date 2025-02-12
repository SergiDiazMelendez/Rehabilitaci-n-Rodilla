using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public Button actionButton;
    private bool textChanged = false;

    void Start()
    {
        actionButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (!textChanged)
        {
            // Cambiar el texto solo la primera vez que se pulsa el botón
            displayText.text = "Te dan 15 días de recuperación, el objetivo es quedarte en 0, adelante empieza a jugar";
            textChanged = true;
        }
        else
        {
            // Si el texto ya ha cambiado, cambia de escena
            SceneManager.LoadScene("Game");
        }
    }
}
