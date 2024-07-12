using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoManager : MonoBehaviour
{
    public static TomatoManager Instance { get; private set; }

    public GameObject p_tomato;
    public Transform tomatoParent;

    public Transform tomatoPos_1;
    public Transform tomatoPos_2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnTomato(tomatoPos_1);
        SpawnTomato(tomatoPos_2);
    }
    public void SpawnTomato(Transform pos)
    {
        Instantiate(p_tomato, pos.position, Quaternion.identity, tomatoParent);
    }
}
