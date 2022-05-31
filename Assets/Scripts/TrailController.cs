using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    private float opacity;
    private float time;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        time = 0f;
        opacity = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        opacity = 1.0f - (time / 5);
        sr.color = new Color(1, 1, 1, opacity);
        if (opacity <= 0){
            Destroy(this.gameObject);
        }
    }
}
