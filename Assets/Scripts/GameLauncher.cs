using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour
{
    private static GameLauncher instance;

    public static GameLauncher Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }


    [SerializeField] private GameView View;
    [SerializeField] private float vikingAttackTime;
    [SerializeField] private List<CardModel> initialCards = new List<CardModel>();

    private VikingModel vikingModel;
    private VikingController vikingController;

    private ContainerModel containerModel;

    void Start()
    {
        InitModels();
        InitViews();
        InitControllers();
        InitSceneObject();
        LaunchScene();
    }

    void LaunchScene()
    {
        Time.timeScale = 1;
    }

    void InitModels()
    {
        vikingModel = new VikingModel(vikingAttackTime);
        containerModel = new ContainerModel();
    }
    void InitViews()
    {
        View.Init(Restart);
    }
    void InitControllers()
    {
        vikingController = View.GetVikingView().gameObject.GetComponent<VikingController>();
        vikingController.Init(vikingModel, View.GetVikingView());

        ContainerController.Instance.Init(containerModel, View.GetContainer(), View.GetTemplate());
    }
    void InitSceneObject()
    {
        foreach (CardModel card in initialCards)
        {
            ContainerController.Instance.CreateCard(card);
        }
    }

    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void WinGame()
    {
        Time.timeScale = 0;
        View.ShowWinPanel();
    }
}
