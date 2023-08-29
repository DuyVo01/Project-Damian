using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAI : MonoBehaviour
{
    [field: SerializeField] public Rigidbody2D rbAI { get; private set; }
    [field: SerializeField] public DataAI dataAI { get; private set; }

    private void Awake()
    {
        rbAI = GetComponent<Rigidbody2D>();
    }
}
