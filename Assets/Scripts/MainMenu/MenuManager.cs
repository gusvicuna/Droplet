using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Animator cameraAnimator;
    [SerializeField]
    private BlackPanel blackPanel;
    [SerializeField]
    private Animator dropletRockAnimator;
    [SerializeField]
    private InputField nameInputField;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame(){
        StartCoroutine(NewGame());
    }

    public void StartNewTest(){
        StartCoroutine(NewTest());
    }

    public IEnumerator NewGame(){
        dropletRockAnimator.SetTrigger("Exit");
        cameraAnimator.SetTrigger("Outro");
        yield return new WaitForSeconds(1);
        blackPanel.gameObject.SetActive(true);
        blackPanel.StartFadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Lvl1");
    }

    public IEnumerator NewTest(){
        dropletRockAnimator.SetTrigger("Exit");
        cameraAnimator.SetTrigger("Outro");
        yield return new WaitForSeconds(1);
        CheckNameData();
        blackPanel.gameObject.SetActive(true);
        blackPanel.StartFadeIn();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Lvl1");
    }

    public void CheckNameData(){
        if(!PlayerPrefs.HasKey(nameInputField.text)){
            PlayerData playerData = new PlayerData(nameInputField.text);
            Serializer.Save<PlayerData>($"{nameInputField.text}.pdata",playerData);
            PlayerPrefs.SetString(nameInputField.text,"created");
        }
        PlayerPrefs.SetString("currentPlayer", nameInputField.text);
        Debug.Log(PlayerPrefs.GetString(nameInputField.text));
    }

    public void ClearData(){
        PlayerPrefs.DeleteAll();
    }
}
