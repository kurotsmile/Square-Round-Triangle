using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tray : MonoBehaviour
{

    public Tray_Line[] tray_line_check;
    public List<Tomb> list_tomb = new List<Tomb>();
    public Transform area_all_tomb;
    public Animator ani;
    public Image img_gamepad_select;
    private bool is_block=false;

    public void add_tomb(List<GameObject> list_tomb_create_object,bool is_effect_add=false)
    {
        for (int i = 0; i <list_tomb_create_object.Count; i++)
        {
            GameObject Tomb_item = Instantiate(list_tomb_create_object[i]);
            Tomb_item.transform.SetParent(this.area_all_tomb);
            Tomb_item.transform.localScale = new Vector3(1f, 1f, 1f);
            Tomb_item.transform.localPosition = Vector3.zero;
            
            if (is_effect_add) Tomb_item.GetComponent<Tomb>().set_in_anim();
            else Tomb_item.GetComponent<Tomb>().set_run_anim();

            list_tomb.Add(Tomb_item.GetComponent<Tomb>());
        }
    }

    public void click()
    {
        if (this.is_block == false)
            GameObject.Find("Game").GetComponent<Game>().add_tomb_to_tray(this,true);
        else
            GameObject.Find("Game").GetComponent<Game>().play_sound(3);
    }

    public void drop_tomb()
    {
        GameObject.Find("Game").GetComponent<Game>().add_tomb_to_tray(this,false);
    }

    public void check_tray_block(List<GameObject> list_tomb_create_object)
    {
        this.is_block = false;
        for (int y = 0; y < list_tomb_create_object.Count; y++) { 
            for(int i = 0; i < this.list_tomb.Count; i++)
            {
                if (this.list_tomb[i].get_type() == list_tomb_create_object[y].GetComponent<Tomb>().get_type()) this.is_block = true;
            }
        }
    }

    public void set_block()
    {
        this.ani.Play("Tray_block");
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void set_nomal()
    {
        this.ani.Play("Tray_nomal");
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public bool check_true_tomb(List<GameObject> list_tomb_create_object)
    {
        bool is_tomb_true = false;
        for (int y = 0; y < list_tomb_create_object.Count; y++)
        {
            Tomb t_chech = list_tomb_create_object[y].GetComponent<Tomb>();
            for (int i = 0; i < this.tray_line_check.Length; i++) if(this.tray_line_check[i].check_tomb_in_line(t_chech)) is_tomb_true=true;
        }
        return is_tomb_true;
    }

    public bool check_tomb_in_tray(Tomb t_check)
    {
        for(int i = 0; i < this.list_tomb.Count; i++)
        {
            if(this.list_tomb[i].get_type_and_color()==t_check.get_type_and_color()) return true;
        }
        return false;
    }

    public void remove_tomb_in_tray(Tomb t_check)
    {
        for (int i = 0; i < list_tomb.Count; i++) if (list_tomb[i].get_type() == t_check.get_type()){
                list_tomb[i].set_out_anim();
        }

        List<Tomb> new_list = new List<Tomb>();
        for (int i = 0; i < list_tomb.Count; i++) if (list_tomb[i].get_status_out()==false) new_list.Add(this.list_tomb[i]);

        this.list_tomb = new_list;
    }

    public bool get_block_status()
    {
        return this.is_block;
    }

    public void clear()
    {
        this.list_tomb = new List<Tomb>();
        GameObject.Find("Game").GetComponent<Game>().carrot.clear_contain(this.area_all_tomb);
        this.set_nomal();
        this.is_block = false;
    }

    public void gamepad_select()
    {
        this.img_gamepad_select.gameObject.SetActive(true);
    }

    public void gamepad_unSelect()
    {
        this.img_gamepad_select.gameObject.SetActive(false);
    }

}
