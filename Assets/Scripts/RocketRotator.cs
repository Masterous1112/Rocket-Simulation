using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketRotator : MonoBehaviour
{
    private Transform tf;
    private Rigidbody2D rb;
    private float lastTime;
    private float curTime;
    private Camera cam;
    private Text displayText;
    private Vector2 goal;
    private Vector2 mousePos;
    private Vector2 lastPos;
    [SerializeField] private GameObject trail;
    private bool onGround;
    [SerializeField] private LayerMask groundLayer;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = 0f;
        curTime = 0f;
        onGround = false;
        tf = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        displayText = GameObject.Find("DisplayText").GetComponent<Text>();
        slider = GameObject.Find("ForceSlider").GetComponent<Slider>();
        goal = tf.position;
        mousePos = Vector2.zero;
        lastPos = tf.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        onGround = false;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)tf.position, Vector2.down, 0.6f, groundLayer);
        
        if (hit.collider != null){
            Debug.Log(hit.collider.gameObject.name);
        }

        if (hit.collider != null && hit.collider.gameObject.name == "Ground"){
            onGround = true;
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        displayText.text = "Slider Position: " + slider.value;
        //if (Input.GetMouseButtonDown(0) && mousePos.y >= -3 && mousePos.y <= 16.5){
        if (true){
            goal = (Vector2)mousePos - (Vector2) tf.position;
            goal = goal.normalized;
            rb.AddForce(goal * slider.value * 1200);
        }
        if (!onGround){
            tf.rotation = getRotationBetweenPoints((Vector2)tf.position, ((Vector2)tf.position + (Vector2)rb.velocity));
        }
        else{
            tf.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    
        // if (distanceBetween(tf.position, lastPos) > 0.4){
        //     lastPos = tf.position;
        //     Instantiate(trail, tf.position, Quaternion.identity);
        // }
        

    }
    private void FixedUpdate() {
        curTime += Time.deltaTime;
        if (curTime - lastTime > 0.05){
            lastTime = curTime;
            if (rb.velocity != Vector2.zero){
                Instantiate(trail, tf.position, Quaternion.identity);
            }
        }
    }

    float distanceBetween(Vector2 point1, Vector2 point2){
        Vector2 difference = point1 - point2;
        return Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2));
    }
    private Quaternion getRotationBetweenPoints(Vector2 origin, Vector2 other){
        Vector2 difference = (origin - other).normalized;
        float side1 = difference.x;
        float side2 = difference.y;
        float angle = Mathf.Atan(side2/side1) * Mathf.Rad2Deg;
        if (side1 > 0){
            angle -= 180;
        }
        return Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
