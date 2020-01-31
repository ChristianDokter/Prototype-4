using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    //private float voor powerUpStrength
    private float powerUpStrength = 15.0f;

    //float voor speed in Unity
    public float speed = 5.0f;

    //bool voor hasPowerup in Unity
    public bool hasPowerup = false;

    // Dit maakt een gameobject voor powerIndicator
    public GameObject powerIndicator;

    
    void Start()
    {
        // Dit zorgt ervoor dat de computer weet wat playerRb is.
        playerRb = GetComponent<Rigidbody>();

        // Dit zorgt ervoor dat de computer weet wat focalPoint is.
        focalPoint = GameObject.Find("Focal Point");
    }

    
    void Update()
    {
        // Dit zorgt er voor dat de input wordt gezocht.
        float forwardInput = Input.GetAxis("Vertical");

        // Dit zorgt ervoor dat de spelere kan bewegen.
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        //dit zorgt ervoordat als de speler een powerup oppakt hij verdwijnt en opgepakt wordt
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // Dit zorgt ervoor dat de powerup 7 seconde duurt
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Dit zorgt ervoor dat als de speler de powerup heeft dat de bouncy sterker wordt
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * 10, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
