using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y + 5, transform.position.z); 

        if(player.transform.localScale.x > 0f || player.transform.localScale.x < 0f )
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y - offset, playerPosition.z);
        
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}

