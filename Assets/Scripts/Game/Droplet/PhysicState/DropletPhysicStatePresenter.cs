using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropletPhysicStatePresenter : MonoBehaviour
{

    [SerializeField]
    private Image gasImage;
    [SerializeField]
    private Image liquidImage;
    [SerializeField]
    private Image solidImage;

    [SerializeField]
    private DropletController dropletController;
    private DropletPhysicStateMachine physicStateMachine;

    private void Start() {
        physicStateMachine = dropletController.physicStateMachine;

        if(physicStateMachine != null){
            physicStateMachine.StateChanged += OnStateChanged;
        }

        UpdateView();
    }

    private void OnDestroy()
    {
        if (physicStateMachine != null)
        {
            physicStateMachine.StateChanged -= OnStateChanged;
        }
    }

    private void OnStateChanged(){
        UpdateView();
    }

    private void UpdateView(){
        
        if(physicStateMachine.CurrentState == physicStateMachine.gasState){
            solidImage.gameObject.SetActive(false);
            liquidImage.gameObject.SetActive(false);
            gasImage.gameObject.SetActive(true);
        }
        else if(physicStateMachine.CurrentState == physicStateMachine.liquidState){
            solidImage.gameObject.SetActive(false);
            liquidImage.gameObject.SetActive(true);
            gasImage.gameObject.SetActive(false);
        }
        else if(physicStateMachine.CurrentState == physicStateMachine.solidState){
            solidImage.gameObject.SetActive(true);
            liquidImage.gameObject.SetActive(false);
            gasImage.gameObject.SetActive(false);
        }
    }
}
