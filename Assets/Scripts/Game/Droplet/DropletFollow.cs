using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletFollow : MonoBehaviour
{
    public GameObject followingGameobject;
    public GameObject followerGameobject;
    public List<Vector3> positionList;
    public int distanceFromFollower = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RegisterMove();
    }

    private void RegisterMove()
    {
        positionList.Add(transform.position);
        if (positionList.Count > distanceFromFollower)
        {
            positionList.RemoveAt(0);
            if(followerGameobject != null){
                followerGameobject.transform.position = positionList[0];
            }
        }
    }

    public void AddDrop(GameObject dropletDropPrefab){
        if(followerGameobject != null){
            DropletFollow followerFollow = followerGameobject.GetComponent<DropletFollow>();
            followerFollow.AddDrop(dropletDropPrefab);
        }
        else{
            GameObject newDrop = Instantiate(dropletDropPrefab, transform);
            DropletFollow followerFollow = newDrop.GetComponent<DropletFollow>();
            followerFollow.followingGameobject = gameObject;
            followerGameobject = newDrop;
        }
    }
}
