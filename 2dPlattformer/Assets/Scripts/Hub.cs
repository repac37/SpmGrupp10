using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Hub : MonoBehaviour
{
    public int sceneNr;
    //public GameObject particle1, particle2;


    IEnumerator OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("level2done"+GameManager.lvl1Done);
        Debug.Log("level2done"+GameManager.lvl2Done);
        if (other.CompareTag("Player"))
        {

            if (sceneNr == 1) {

                other.gameObject.SetActive(false);
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("Level1");

            }

            if (GameManager.lvl1Done && sceneNr == 2)
            {
                other.gameObject.SetActive(false);
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("Level2");

            }

            if (GameManager.lvl2Done && sceneNr == 4)
            {
                other.gameObject.SetActive(false);
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("Level4");

            }
        }
    }
}