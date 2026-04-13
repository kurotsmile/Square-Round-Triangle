using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tray_Line : MonoBehaviour
{
    public Tray[] tray;
    public GameObject obj_effect_line_true;

    private Tomb tomb_temp;
    public bool check_tomb_in_line(Tomb t)
    {
        bool is_tomb_true = false;
        if (this.tray[0].check_tomb_in_tray(t)==true&& this.tray[1].check_tomb_in_tray(t)==true&& this.tray[2].check_tomb_in_tray(t))
        {
            this.tomb_temp = t;
            this.obj_effect_line_true.GetComponent<Image>().color = t.get_tomb_data().color_tray;

            StartCoroutine(this.show_effect_true());
            StartCoroutine(this.hide_effect_true());
            is_tomb_true = true;
        }
        return is_tomb_true;
    }

    private IEnumerator show_effect_true()
    {
        yield return new WaitForSeconds(0.6f);
        GameObject.Find("Game").GetComponent<Game>().play_sound(2);
        this.obj_effect_line_true.SetActive(true);
        GameObject.Find("Game").GetComponent<Game>().check_reward();

    }

    private IEnumerator hide_effect_true()
    {
        yield return new WaitForSeconds(1.3f);
        this.tray[0].remove_tomb_in_tray(this.tomb_temp);
        this.tray[1].remove_tomb_in_tray(this.tomb_temp);
        this.tray[2].remove_tomb_in_tray(this.tomb_temp);
        this.obj_effect_line_true.SetActive(false);
        GameObject.Find("Game").GetComponent<Game>().check_tray_ins();

    }
}
