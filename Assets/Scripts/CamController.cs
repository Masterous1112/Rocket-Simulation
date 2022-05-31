using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private Camera cam;
    private Transform tf; 
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = cam.orthographicSize + (1 * Time.deltaTime);
        Vector3 camPos = cam.gameObject.GetComponent<Transform>().position;
        camPos.x += (1920f/1080f) * Time.deltaTime;
        camPos.y += Time.deltaTime;
        cam.gameObject.GetComponent<Transform>().position = camPos;
    }
}
