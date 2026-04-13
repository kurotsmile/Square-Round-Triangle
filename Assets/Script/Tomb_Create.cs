using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb_Create : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Game").GetComponent<Game>().tray_create.set_tray_temp(collision.GetComponent<Tray>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.Find("Game").GetComponent<Game>().tray_create.set_none_tray_temp();
    }
}
