using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tomb : MonoBehaviour
{
    public Image[] t_type;

    public int index_type;
    public int index_color;

    private Tomb_data tomb_data;
    private bool is_out = false;
    
    public void load_data(Tomb_data t_data)
    {
        this.is_out = false;
        this.tomb_data = t_data;
        for (int i = 0; i < t_data.visible_tray.Length; i++){
            this.t_type[i].color = t_data.color_tray;

            if (t_data.visible_tray[i])
                this.t_type[i].gameObject.SetActive(true);
            else
                this.t_type[i].gameObject.SetActive(false);
            
        }
        this.index_type = t_data.type;
        this.index_color = t_data.index_color;
    }

    public void load_skin(Sprite[] sp)
    {
        for (int i = 0; i < this.t_type.Length; i++) this.t_type[i].sprite = sp[i];
    }

    public int get_type()
    {
        return this.index_type;
    }

    public string get_type_and_color()
    {
        return this.index_type + "," + this.index_color;
    }

    public Tomb_data get_tomb_data()
    {
        return this.tomb_data;
    }

    public void stop_anim()
    {
        this.GetComponent<Animator>().enabled = false;
    }

    public void set_run_anim()
    {
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<Animator>().Play("Tomb_run");
    }

    public void set_in_anim()
    {
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<Animator>().Play("Tomb_in");
    }

    public void set_out_anim()
    {
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<Animator>().Play("Tomb_out");
        this.is_out = true;
        Destroy(this.gameObject,0.5f);
    }

    public bool get_status_out()
    {
        return this.is_out;
    }
}
