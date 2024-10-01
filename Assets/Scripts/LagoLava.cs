using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public class LagoLava : MonoBehaviour
{
    private Transform refPosExit;
    private Transform refPosEnter;
    public GameObject lavaPrefab;
    float spd = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        refPosEnter = GameObject.Find("RefPosEnterLagoLava").transform;
        refPosExit = GameObject.Find("RefPosExitLagoLava").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)new Vector2(-spd * Time.deltaTime, 0f);

        if (this.transform.position.x <= refPosExit.position.x)
        {
            transform.position = (Vector3)new Vector2(refPosEnter.position.x, refPosEnter.position.y);
        }
    }
}