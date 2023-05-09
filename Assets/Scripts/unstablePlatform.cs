using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unstablePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("collideS");
            removePlatform();
        }
        
    }

    // removes the platform after 5 seconds
    private IEnumerator removePlatform()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }

    // enables the platform after 2 seconds
    private IEnumerator restorePlatform()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(true);
    }
    
    // after the platform is disabled restore platform is called
    private void OnDisable()
    {
        restorePlatform();
    }
}
