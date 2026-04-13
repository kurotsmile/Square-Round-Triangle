using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_pad : MonoBehaviour
{
    public GameObject obj_btn_setting_gamepad_home;
    public List<GameObject> list_button_gameover;
    private Carrot.Carrot_game carrot_game;
    private int gamepad_pointer_tray_x = 0;
    private int gamepad_pointer_tray_y = 0;

    private List<Tray[]> table_tray;

    public void load_gamepad()
    {
        this.carrot_game = this.GetComponent<Game>().carrot.GetComponent<Carrot.Carrot_game>();
        this.obj_btn_setting_gamepad_home.SetActive(false);


        this.carrot_game.set_act_handle_detection(this.act_change_status_gamepad);
        
        this.table_tray = new List<Tray[]>();
        this.table_tray.Add(this.GetComponent<Game>().tray_line[0].tray);
        this.table_tray.Add(this.GetComponent<Game>().tray_line[1].tray);
        this.table_tray.Add(this.GetComponent<Game>().tray_line[2].tray);
    }

    public void gamepad_keydown_left()
    {
        this.gamepad_pointer_tray_x--;
        if (this.gamepad_pointer_tray_x < 0) this.gamepad_pointer_tray_x = 2;
        this.check_pointer_tray();
    }

    public void gamepad_keydown_right()
    {
        this.gamepad_pointer_tray_x++;
        if (this.gamepad_pointer_tray_x >= 3) this.gamepad_pointer_tray_x = 0;
        this.check_pointer_tray();
    }

    public void gamepad_keydown_down()
    {
        this.gamepad_pointer_tray_y--;
        if (this.gamepad_pointer_tray_y <0) this.gamepad_pointer_tray_y = 2;
        this.check_pointer_tray();
    }

    public void gamepad_keydown_up()
    {
        this.gamepad_pointer_tray_y++;
        if (this.gamepad_pointer_tray_y >= 3) this.gamepad_pointer_tray_y = 0;
        this.check_pointer_tray();
    }

    public void gamepad_keydown_x()
    {
        this.table_tray[this.gamepad_pointer_tray_y][this.gamepad_pointer_tray_x].click();
    }

    public void gamepad_keydown_select()
    {
        this.table_tray[this.gamepad_pointer_tray_y][this.gamepad_pointer_tray_x].click();
    }

    public void gamepad_keydown_y()
    {
        this.GetComponent<Game>().btn_show_setting();
    }

    public void gamepad_keydown_b()
    {
        this.GetComponent<Game>().carrot.act_Escape();
    }

    public void gamepad_keydown_start()
    {
        this.GetComponent<Game>().btn_game_replay();
    }

    private void check_pointer_tray()
    {
        this.reset_check_pointer_tray();
        this.table_tray[this.gamepad_pointer_tray_y][this.gamepad_pointer_tray_x].gamepad_select();
    }

    private void reset_check_pointer_tray()
    {
        for(int i = 0; i < 3; i++) {
            for(int y=0;y<3;y++)
            this.table_tray[i][y].gamepad_unSelect();
        }
    }

    public void act_change_status_gamepad(bool is_active)
    {
        this.obj_btn_setting_gamepad_home.SetActive(is_active);
    }



}
