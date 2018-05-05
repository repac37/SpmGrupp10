using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    //public GameObject particle1, particle2;


    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            other.gameObject.SetActive(false);
           // particle1.SetActive(true);
           // particle2.SetActive(true);

            // particle1 och particle2 borde sättas aktiva här. YOLO

            yield return new WaitForSeconds(1f);

            // Här bör vi ladda en ny scen.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}