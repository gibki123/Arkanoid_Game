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
    private Vector3 initialPaddleSize;
    private Vector3 upgradedPaddleSize;
    private float maxAngle = 75f;

    public static UpgradesHandling Instance;
    public static bool forceUpgrade;
    public static bool widthUpgrade;
    public static bool paddleCollided;
    public static bool stickUpgrade;
    public static float forceSum;




    private void Awake()
    {
        Instance = this;
        particle = gameObject.transform.GetChild(0).gameObject;
        particle.SetActive(false);
        directionalArrow = gameObject.transform.GetChild(1).gameObject;
        directionalArrow.SetActive(false);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        paddle = GameObject.FindGameObjectWithTag("racket");
        initialPaddleSize = paddle.transform.localScale;
        upgradedPaddleSize = new Vector3(5, 0.15f, 0);
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
            float rotation = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
            //Debug.Log(directionalArrow.transform.rotation.z);
            Debug.Log(directionalArrow.transform.rotation.eulerAngles);
            //Debug.Log(directionalArrow.transform.rotation);
            Vector3 eulerAngles = directionalArrow.transform.rotation.eulerAngles;
            if (eulerAngles.z <=maxAngle || eulerAngles.z >=360 - maxAngle)
            {
                directionalArrow.transform.RotateAround(transform.position, Vector3.forward, rotation);
            }
            if (eulerAngles.z > maxAngle && eulerAngles.z < 100 && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                directionalArrow.transform.RotateAround(transform.position, Vector3.forward, rotation);
            }
            if (eulerAngles.z < 360-maxAngle && eulerAngles.z > 200 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                directionalArrow.transform.RotateAround(transform.position, Vector3.forward, rotation);
            }

            transform.SetParent(paddle.transform);
            if (Input.GetButtonDown("Fire1"))
            {
                stickUpgrade = false;
                paddleCollided = false;
                rb.isKinematic = false;
                transform.parent = null;
                directionalArrow.SetActive(false);
                rb.velocity = new Vector3(0, 0, 0);
                float eulerAngleZ = directionalArrow.transform.rotation.eulerAngles.z;
                if(eulerAngleZ>=0&& eulerAngleZ <= 90)
                {
                    float sideForcePercentage = eulerAngleZ / 90;
                    float sideForce = sideForcePercentage * forceSum;
                    rb.AddForce(-sideForce, forceSum - sideForce, 0);
                }
                if (eulerAngleZ >= 270)
                {
                    eulerAngleZ -= 270;
                    float yForcePercentage = eulerAngleZ / 90;
                    float yForce = yForcePercentage * forceSum;
                    rb.AddForce(forceSum - yForce,yForce, 0);
                }
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
        if(transform.parent != null)
        {
            transform.parent = null;
            paddle.transform.localScale = upgradedPaddleSize;
            transform.SetParent(paddle.transform);
        }  
        yield return new WaitForSeconds(10);
        paddle.transform.localScale = initialPaddleSize;
        paddle.GetComponent<Renderer>().material.color = Color.white;
    }

    public void stickUpgradeEnd()
    {
        stickUpgrade = false;
        directionalArrow.SetActive(false);
    }

    public void WidthUpgradeEnd()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            paddle.transform.localScale = initialPaddleSize;
            transform.SetParent(paddle.transform);
        }   
    }

    public void AllUpgradesEnd()
    {
        ForceUpgradeEnd();
        WidthUpgradeEnd();
        stickUpgradeEnd();
    }
}
