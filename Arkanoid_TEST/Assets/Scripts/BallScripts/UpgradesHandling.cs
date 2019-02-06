using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesHandling : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject particle;
    private GameObject directionalArrow;
    private GameObject paddle;
    private Rigidbody rb;
    private float rotationSpeed = 100f;

    public static UpgradesHandling Instance;

    public static bool forceUpgrade;
    public static bool widthUpgrade;
    public static bool paddleCollided;
    public static bool stickUpgrade;


    private void Awake()
    {
        Instance = this;
        particle = gameObject.transform.GetChild(0).gameObject;
        particle.SetActive(false);
        directionalArrow = gameObject.transform.GetChild(1).gameObject;
        directionalArrow.SetActive(false);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        paddle = GameObject.FindGameObjectWithTag("racket");
        rb = GetComponent<Rigidbody>();
        paddleCollided = false;
        forceUpgrade = false;
        widthUpgrade = false;
        stickUpgrade = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "racket")
        {
            if (forceUpgrade == false && paddleCollided == true)
            {
                ForceUpgradeEnd();
            }
            if (forceUpgrade == true)
            {
                paddleCollided = true;
                if(widthUpgrade == false)
                {
                    collision.transform.GetComponent<Renderer>().material.color = Color.white;
                }
            }
            if(stickUpgrade == true)
            {            
                rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = true;
                paddle.GetComponent<Renderer>().material.color = Color.white;
                directionalArrow.SetActive(true);
                paddleCollided = true;
            }
        }  
    }

    private void Update()
    {
        if(widthUpgrade == true)
        {
            StopCoroutine(WidthUpgrade());
            StartCoroutine(WidthUpgrade());
            widthUpgrade = false;
        }
        if(stickUpgrade == true && paddleCollided == true)
        {
            directionalArrow.SetActive(true);
            Quaternion rotation = Quaternion.Euler(0,0,Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime);
            directionalArrow.transform.RotateAround(transform.position, Vector3.forward,rotation.ToEuler());
            float euler = directionalArrow.transform.eulerAngles.z;
            euler = Mathf.Clamp(rotation, -60, 60);
            transform.SetParent(paddle.transform);
            if (Input.GetButtonDown("Fire1"))
            {
                float zRotation = directionalArrow.transform.rotation.z;
                
            }
        }
    }

    private void LateUpdate()
    {
        if (forceUpgrade == true && paddleCollided == true)
        {
            ForceUpgrade();
            forceUpgrade = false;
        }
    }

    public void ForceUpgrade()
    {
        particle.SetActive(true);
        foreach (var item in BlockSpawning.spawnedBlocks)
        {
            item.GetComponent<Collider>().isTrigger = true;
            Debug.Log(item.name);
        }
        mainCamera.GetComponent<CameraShake>().enabled = true;
    }

    public void ForceUpgradeEnd()
    {
        foreach (var item in BlockSpawning.spawnedBlocks)
        {
            item.GetComponent<Collider>().isTrigger = false;
            Debug.Log(item.name);
        }
        mainCamera.GetComponent<CameraShake>().enabled = false;
        particle.SetActive(false);
        forceUpgrade = false;
        paddleCollided = false;
    }

    public IEnumerator WidthUpgrade()
    {
        paddle.transform.localScale = new Vector3(5, 0.15f, 1);
        yield return new WaitForSeconds(10);
        paddle.transform.localScale = new Vector3(3.5f, 0.15f, 1);
        paddle.GetComponent<Renderer>().material.color = Color.white;
    }

    public void WidthUpgradeEnd()
    {       
        paddle.transform.localScale = new Vector3(3.5f, 0.15f, 1);
    }

    public void AllUpgradesEnd()
    {
        ForceUpgradeEnd();
        WidthUpgradeEnd();
    }
}
