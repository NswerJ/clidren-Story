using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SulmeongPanel;
    public void StartButton()
    {
        SceneManager.LoadScene("Stage");
    }
    public void Sulmeong()
    {
        SulmeongPanel.SetActive(true);
    }
    public void PanelExit()
    {
        SulmeongPanel.SetActive(false);
    }
    public void menu()
    {
        SceneManager.LoadScene("StartScence");
    }
    public void restart()
    {
        SceneManager.LoadScene("stage");
    }
}
