using System.Collections;
using UnityEngine;

public class CrocodileMouthController : MonoBehaviour
{
    public float openDuration = 1.0f; 
    public float closedDuration = 1.0f;

    [SerializeField] private GameObject open_mouth, closed_mouth;
    private void Start()
    {
        StartCoroutine(OpenCloseMouth());
        open_mouth.SetActive(true);
        closed_mouth.SetActive(false);
    }

    private IEnumerator OpenCloseMouth()
    {
        while (true)
        {
            yield return new WaitForSeconds(openDuration);
            CloseMouth();
            yield return new WaitForSeconds(closedDuration);
            OpenMouth();
        }
    }

    private void OpenMouth()
    {
        open_mouth.SetActive(true);
        closed_mouth.SetActive(false);
    }
    
    private void CloseMouth()
    {
        open_mouth.SetActive(false);
        closed_mouth.SetActive(true);
    }
    
}