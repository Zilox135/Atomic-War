using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] float loadLevelDelay = 2f;
    [SerializeField] ParticleSystem collisionParticle;

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()  
    {
        collisionParticle.Play(true);
        
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        Invoke(nameof(ReloadLevel), loadLevelDelay);  
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
