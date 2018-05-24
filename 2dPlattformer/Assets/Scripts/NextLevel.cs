using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("lvl1" + GameManager.lvl1Done);
            Debug.Log("lvl2" + GameManager.lvl1Done);
            other.gameObject.SetActive(false);
           // particle1.SetActive(true);
           // particle2.SetActive(true);

            // particle1 och particle2 borde sättas aktiva här. YOLO
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                GameManager.lvl1Done = true;
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GameManager.lvl2Done = true;
            }

            yield return new WaitForSeconds(1f);

            // Här bör vi ladda en ny scen.
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); this is for Leo
            SceneManager.LoadScene("HUB");
        }
    }
}