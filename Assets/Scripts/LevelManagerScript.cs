using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
  public void Play()
  {
  SceneManager.LoadScene("Intro");
  }
  public void Exit()
  {
  SceneManager.LoadScene("MainMenu");
  }
}
