using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Brough, Heath]
// [5/9/2023]
// disables and enables the platform after it is touched


public class PlatformParentController : MonoBehaviour
{
    // how much time the platform is inactive
    public float timeInactive;
    private void OnTriggerEnter(Collider other)
    {
        // if the player touches the platform, call disableAndEnable
        if (other.CompareTag("Player"))
        {
            StartCoroutine(disableAndEnable());
        }
    }

    // enables and disables the platform after it is touched
    private IEnumerator disableAndEnable()
    {
        // set the platform inactive after timeInactive seconds 
        yield return new WaitForSeconds(timeInactive);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        // set the platform active after timeInactive/2 seconds
        yield return new WaitForSeconds(timeInactive/2);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
