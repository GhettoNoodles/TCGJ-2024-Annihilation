using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Deleteeee());
    }
    public IEnumerator Deleteeee()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
