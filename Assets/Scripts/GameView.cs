using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject cardTemplate;
    [SerializeField] private Transform container;
    [SerializeField] private VikingView vikingView;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button restartButton;

    public void Init(UnityAction onRestart)
    {
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(onRestart);
    }

    public GameObject GetTemplate()
    {
        return cardTemplate;
    }
    public Transform GetContainer()
    {
        return container;
    }
    public VikingView GetVikingView()
    {
        return vikingView;
    }

    public void ShowWinPanel()
    {
        winPanel.gameObject.SetActive(true);
    }
}
