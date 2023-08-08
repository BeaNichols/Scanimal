using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    private void Start()
    {
        Instantiate(Player, this.GetComponentInParent<Transform>());
    }
}
