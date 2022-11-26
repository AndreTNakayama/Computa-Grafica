using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float speedRotate;
    public Camera camera;
    public int life;
    public Text txtLife;
    public Rigidbody rb;
    private bool inFloor = false;
    public Quaternion originalRotationValue;

    // Start is called before the first frame update
    void Start()
    {
        txtLife.text = "Vidas: " + life;
        originalRotationValue = transform.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0.0f, speedRotate, 0.0f);
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0.0f, -speedRotate, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inFloor)
            {
                rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
                inFloor = false;
            }
        }
        /*
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0.0f, 0.0f, speed);
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0.0f, 0.0f, -speed);
        }
        */
        transform.position += transform.forward * speed;
        camera.transform.position = transform.position + new Vector3(0.0f, 1.2f, -1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Colidiu!");
            transform.position = new Vector3(0.0f, 0.6f, -19.5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * speedRotate); 
            life--;

            txtLife.text = "Vidas: " + life;

            if(life == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (collision.gameObject.tag == "Win")
        {
            Debug.Log("Ganhou!");
            transform.position = new Vector3(0.0f, 0.6f, -19.5f);
            
            life = 3;

            txtLife.text = "Vidas: " + life;

            SceneManager.LoadScene("WinScene");
        }

        if(collision.gameObject.tag == "Floor")
        {
            inFloor = true;
        }
    }
}