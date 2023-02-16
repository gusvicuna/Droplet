using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool isPaused = false;
    private bool hasGameLoaded = false;

    [SerializeField]
    private int level = 0;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject finishMenu;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private GameObject continueToGame;
    [SerializeField]
    private GameObject blackPanel;
    [SerializeField]
    private GameObject gameOverMenu;

    private PlayerData playerData;

    private DropletController dropletController;
    // Start is called before the first frame update

    private void Awake() {
        if(Instance != null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dropletController = GameObject.FindGameObjectWithTag("Player").GetComponent<DropletController>();
        dropletController.OnFinished.AddListener(OnFinishedLevel);
        dropletController.Die.AddListener(OnDeath);
        LoadGame();
        StartCoroutine("WaitForLoadedGame");
    }

    private void LoadGame()
    {
        if(level>=0){
            LoadPlayerData();
        }
        hasGameLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && hasGameLoaded){
            StartCoroutine("StartGame");
        }
    }

    public void PauseGame(){
        if(isPaused){
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else{
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        isPaused = !isPaused;
    }

    public void OnFinishedLevel(){
        finishMenu.SetActive(true);
        finishMenu.GetComponent<FinishUI>().ShowScore();
        Time.timeScale = 0;
    }

    public void OnDeath(){
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    private IEnumerator WaitForLoadedGame(){
        Time.timeScale = 0;
        while(!hasGameLoaded){
            yield return null;
        }
        Time.timeScale = 1;
        continueToGame.SetActive(true);
    }

    private IEnumerator StartGame(){
        blackPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
        loadingScreen.SetActive(false);
        blackPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        blackPanel.SetActive(false);
    }

    public void BackToMenu(){
        SavePlayerData();
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry(){
        SavePlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueToNextLevel(){
        SavePlayerData();
        SceneManager.LoadScene($"Lvl{level+1}");
    }

    private void LoadPlayerData(){
        playerData = Serializer.Load<PlayerData>($"{PlayerPrefs.GetString("currentPlayer")}.pdata");
        dropletController.LoadPlayerData(playerData, level);
    }

    private void SavePlayerData(){
        playerData.levels[level]["deathCounts"] = dropletController.score.deathCounts;
        playerData.levels[level]["score"] = dropletController.score.score;
        playerData.levels[level]["whiteFlowersCount"] = dropletController.score.whiteFlowersCount;
        playerData.levels[level]["yellowFlowersCount"] = dropletController.score.yellowFlowersCount;
        playerData.levels[level]["blueFlowersCount"] = dropletController.score.blueFlowersCount;
        playerData.levels[level]["timeFromStart"] = dropletController.score.timeFromStart;
        playerData.levels[level]["finalMass"] = dropletController.health.CurrentHealth;
        Serializer.Save<PlayerData>($"{PlayerPrefs.GetString("currentPlayer")}.pdata",playerData);
    }
}
