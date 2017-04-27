using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escada : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            outro.SendMessage("SetSubindo", true);
        }
    }

    void OnTriggerExit2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            outro.SendMessage("SetSubindo", false);
        }
    }
}
