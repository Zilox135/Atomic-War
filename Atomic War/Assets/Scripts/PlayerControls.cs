using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    
    [HeaderAttribute("General Setup Settings")]
    [Tooltip("How fast the player can move the ship up and down")]
    [SerializeField] float controlSpeed = 10f;
    [Tooltip("The maximum range that the player can go on the X Axis")]
    [SerializeField] float xRange = 10f;
    [Tooltip("The maximum range that the player can go on the Y Axis")]
    [SerializeField] float yRange = 7f;
    [Header("Screen based position")]
    [SerializeField] float positionPitchFactor = -2f;
    
    [SerializeField] float positionYawFactor = 2f;
    [Tooltip("The amount of pitch force")]
    [Header("Camera based position")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -5f;

    [Header("Particles Setup")]
    [Tooltip("Control for particles")]
    [SerializeField] GameObject[] lasers;

    
    [SerializeField] GameObject laserLeft;
    [SerializeField] GameObject laserRight;
    [SerializeField] Transform laserJointLeft;
    [SerializeField] Transform laserJointRight;

    GameObject parentGameObject;

    float xThrow, yThrow;
    
    void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }


}
