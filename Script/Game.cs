using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Game Object")]
    public Carrot.Carrot carrot;
    public Game_scores game_scores;
    public Game_pad game_pad_play;

    [Header("Ui Object")]
    public GameObject panel_game_over;
    public GameObject panel_menu_bar_protain;
    public Transform area_tray_main;
    public Animator ani;

    

    public Tray[] tray;
    public Tray_Line[] tray_line;
    public Tray_Create tray_create;
    public Image img_skill;
    public Sprite[] sp_skill;

    private int game_mode = 1;
    private float count_timer_show_tip = 0;

    private bool is_protain = true;

    public AudioSource[] sound;

    private Carrot.Carrot_Box box_mode_game;
    private Carrot.Carrot_Gamepad gamepad1;

    void Start()
    {
        this.carrot.Load_Carrot(this.check_exit_app);
        this.carrot.act_after_close_all_box = this.reset_game_pad_status;
        this.carrot.game.set_act_handle_detection(this.game_pad_play.act_change_status_gamepad);
        this.game_pad_play.load_gamepad();

        this.gamepad1=this.carrot.game.create_gamepad("Player1");

        gamepad1.set_gamepad_keydown_down(gamepad_keydown_down);
        gamepad1.set_gamepad_keydown_up(gamepad_keydown_up);
        gamepad1.set_gamepad_keydown_left(gamepad_keydown_left);
        gamepad1.set_gamepad_keydown_right(gamepad_keydown_right);
        gamepad1.set_gamepad_keydown_x(gamepad_keydown_x);
        gamepad1.set_gamepad_keydown_y(gamepad_keydown_y);
        gamepad1.set_gamepad_keydown_b(gamepad_keydown_b);
        gamepad1.set_gamepad_keydown_a(gamepad_keydown_a);
        gamepad1.set_gamepad_keydown_select(gamepad_keydown_select);
        gamepad1.set_gamepad_keydown_start(gamepad_keydown_start);

        this.game_mode = PlayerPrefs.GetInt("game_mode",1);

        this.panel_game_over.SetActive(false);

        this.tray_create.load_data();
        this.game_scores.load_data();

        if (this.carrot.get_status_sound()) this.carrot.game.load_bk_music(this.sound[10]);

        for(int i = 0; i < this.tray.Length; i++) this.tray[i].set_nomal();

        this.carrot.delay_function(1f, this.check_rotate_scene);
        this.carrot.game.set_enable_gamepad_console(false);
    }

    private void gamepad_keydown_start()
    {
        if (this.carrot.game.get_status_gamepad_console()){
            this.carrot.game.gamepad_keydown_enter_console();
        }else
            this.game_pad_play.gamepad_keydown_start();
    }

    private void gamepad_keydown_select()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_enter_console();
        else
            this.game_pad_play.gamepad_keydown_select();
    }

    private void gamepad_keydown_a()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_down_console();
        else
            this.game_pad_play.gamepad_keydown_start();
    }

    private void gamepad_keydown_b()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_up_console();
        else
            this.game_pad_play.gamepad_keydown_b();
    }

    private void gamepad_keydown_y()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_up_console();
        else
            this.game_pad_play.gamepad_keydown_y();
    }

    private void gamepad_keydown_x()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_down_console();
        else
            this.game_pad_play.gamepad_keydown_x();
    }

    private void gamepad_keydown_right()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_up_console();
        else
            this.game_pad_play.gamepad_keydown_right();
    }

    private void gamepad_keydown_left()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_up_console();
        else
            this.game_pad_play.gamepad_keydown_left();
    }

    private void gamepad_keydown_up()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_up_console();
        else
            this.game_pad_play.gamepad_keydown_up();
    }

    private void gamepad_keydown_down()
    {
        if (this.carrot.game.get_status_gamepad_console())
            this.carrot.game.gamepad_keydown_down_console();
        else
            this.game_pad_play.gamepad_keydown_down();
    }

    private void check_exit_app()
    {
        if (this.panel_game_over.activeInHierarchy)
        {
            this.btn_game_replay();
            this.carrot.set_no_check_exit_app();
        }
    }

    private void Update()
    {
        this.count_timer_show_tip+=Time.deltaTime*1f;
        if (this.count_timer_show_tip > 8f)
        {
            if(this.get_status_protain())
                this.ani.Play("GameTipPortain");
            else
                this.ani.Play("GameTipLandspace");
            this.count_timer_show_tip = 0;
        }

    }

    private void set_hide_hand_tip()
    {
        this.ani.Play("GameMain");
        this.count_timer_show_tip = 0;
    }

    public void add_tomb_to_tray(Tray t,bool is_effect_add=false)
    {
        this.set_hide_hand_tip();
        t.add_tomb(this.tray_create.get_list_tomb_create(), is_effect_add);
        bool is_true_tomb=t.check_true_tomb(this.tray_create.get_list_tomb_create());
        this.play_sound(1);

        this.tray_create.create_tomb();
        this.check_tray_ins();
        if(is_true_tomb==false) this.check_game_over();
    }

    public void check_tray_ins()
    {
        for(int i = 0; i < this.tray.Length; i++) this.tray[i].check_tray_block(this.tray_create.get_list_tomb_create());

        for (int i = 0; i < this.tray.Length; i++)
        {
            if (this.tray[i].get_block_status() == true)
                this.tray[i].set_block();
            else
                this.tray[i].set_nomal();
        }
    }

    private void check_game_over()
    {
        int count_tray_false = 0;
        for(int i = 0; i < this.tray.Length; i++) if (this.tray[i].get_block_status() == true) count_tray_false++;

        if (count_tray_false >8){
            this.carrot.delay_function(0.8f, show_game_over);
        }
    }

    private void show_game_over()
    {
        this.carrot.ads.show_ads_Interstitial();
        this.panel_game_over.SetActive(true);
        this.game_scores.check_and_update_hight_scores();
        this.tray_create.drop_tomb();
        this.carrot.play_vibrate();
        this.play_sound(9);
        this.ani.Play("Gameover");
        this.carrot.delay_function(2f, this.stop_anim_gameover);
        this.carrot.game.set_list_button_gamepad_console(this.GetComponent<Game_pad>().list_button_gameover);
    }

    public void btn_game_replay()
    {
        this.play_sound();
        for (int i = 0; i < this.tray.Length; i++) this.tray[i].clear();
        this.panel_game_over.SetActive(false);
        this.carrot.ads.show_ads_Interstitial();
        this.game_scores.reset();
        this.tray_create.reset();
        this.ani.enabled = true;
        this.ani.Play("GameReplay");
        this.carrot.game.set_enable_gamepad_console(false);
        this.carrot.delay_function(2f, fix_game_replay);
    }

    private void fix_game_replay()
    {
        this.area_tray_main.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void stop_anim_gameover()
    {
        this.ani.enabled = false;
    }

    public void btn_show_setting()
    {
        this.carrot.ads.show_ads_Interstitial();
        Carrot.Carrot_Box box_setting=this.carrot.Create_Setting();

        Carrot.Carrot_Box_Item setting_game_mode= box_setting.create_item_of_top("mode_game");
        setting_game_mode.set_title("Choose game mode");
        setting_game_mode.set_tip("Change game mode");
        setting_game_mode.set_icon(this.game_scores.sp_mode_game[this.get_game_mode()]);
        setting_game_mode.set_act(this.btn_show_game_mode);

        box_setting.set_act_before_closing(this.act_after_close_settting);
        box_setting.update_color_table_row();
        box_setting.update_gamepad_cosonle_control();
    }

    private void act_after_close_settting(List<string> list_change)
    {
        foreach(string s in list_change)
        {
            if (s == "list_bk_music") this.carrot.game.load_bk_music(this.sound[10]);
        }

        if (this.carrot.get_status_sound())
            this.sound[10].Play();
        else
            this.sound[10].Stop();
        this.carrot.game.set_enable_gamepad_console(false);
        this.carrot.ads.show_ads_Interstitial();
    }

    public void play_sound(int index_sound = 0)
    {
        if(this.carrot.get_status_sound()) this.sound[index_sound].Play();
    }

    public void btn_show_app_other()
    {
        this.carrot.ads.show_ads_Interstitial();
        this.play_sound();
        this.carrot.show_list_carrot_app();
    }

    public void btn_show_rate()
    {
        this.play_sound();
        this.carrot.show_rate();
    }

    public void btn_show_share()
    {
        this.play_sound();
        this.carrot.show_share();
    }

    public void btn_remove_ads()
    {
        this.play_sound();
        this.carrot.buy_product(this.carrot.index_inapp_remove_ads);
    }

    public void btn_user()
    {
        this.carrot.user.show_login();
    }

    public void btn_rank()
    {
        this.carrot.ads.show_ads_Interstitial();
        this.carrot.game.Show_List_Top_player();
    }

    public void act_change_rotate_scene()
    {
        this.carrot.delay_function(0.2f, this.check_rotate_scene);
    }

    private void check_rotate_scene()
    {
        this.is_protain = this.panel_menu_bar_protain.activeInHierarchy;
        this.tray_create.set_rotate_scene(this.panel_menu_bar_protain.activeInHierarchy);
        this.game_scores.act_play_anin();
    }

    public void check_reward()
    {
        int count_line_true = 0;
        for (int i = 0; i < this.tray_line.Length; i++){
            if(this.tray_line[i].obj_effect_line_true.activeInHierarchy) count_line_true++;
        }
        this.carrot.log("Line true:" + count_line_true);
        if (count_line_true >= 2) {
            if(count_line_true==2){this.img_skill.sprite = this.sp_skill[0];this.play_sound(4);}
            if(count_line_true==3){this.img_skill.sprite = this.sp_skill[1];this.play_sound(5);}
            if(count_line_true==4){this.img_skill.sprite = this.sp_skill[2];this.play_sound(6);}
            if(count_line_true==5){this.img_skill.sprite = this.sp_skill[3];this.play_sound(7);}
            if(count_line_true==6){this.img_skill.sprite = this.sp_skill[4];this.play_sound(8);}

            this.ani.Play("GameKill");
            
        }
        GameObject.Find("Game").GetComponent<Game>().game_scores.add_scores(count_line_true);
    }

    public bool get_status_protain()
    {
        return this.is_protain;
    }

    public void btn_show_game_mode()
    {
        this.box_mode_game= this.carrot.Create_Box("game_mode");
        this.box_mode_game.set_icon(this.game_scores.sp_mode_game[this.get_game_mode()]);
        this.box_mode_game.set_title("Choose game mode");

        Carrot.Carrot_Box_Item model_item_easy= this.box_mode_game.create_item("model_easy");
        model_item_easy.set_icon(this.game_scores.sp_mode_game[0]);
        model_item_easy.set_title("Easy");
        model_item_easy.set_tip("High score:" + PlayerPrefs.GetInt("hight_scores_" + 0));
        model_item_easy.set_act(() => this.btn_select_game_mode(0));

        Carrot.Carrot_Box_Item model_item_medium = this.box_mode_game.create_item("model_medium");
        model_item_medium.set_icon(this.game_scores.sp_mode_game[1]);
        model_item_medium.set_title("Medium");
        model_item_medium.set_tip("High score:" + PlayerPrefs.GetInt("hight_scores_" + 1));
        model_item_medium.set_act(() => this.btn_select_game_mode(1));

        Carrot.Carrot_Box_Item model_item_difficult = this.box_mode_game.create_item("model_difficult");
        model_item_difficult.set_icon(this.game_scores.sp_mode_game[2]);
        model_item_difficult.set_title("Difficult");
        model_item_difficult.set_tip("High score:" + PlayerPrefs.GetInt("hight_scores_" + 2));
        model_item_difficult.set_act(() => this.btn_select_game_mode(2));

        this.play_sound();
    }

    public void btn_select_game_mode(int index_mode)
    {
        if (index_mode != this.game_mode){
            this.game_mode = index_mode;
            for (int i = 0; i < this.tray.Length; i++) this.tray[i].clear();
            this.tray_create.load_data();
            this.tray_create.tr_obj_tray.gameObject.SetActive(true);
            this.game_scores.select_mode_game(index_mode);
        }

        if (this.box_mode_game != null) this.box_mode_game.close();
    }

    public int get_game_mode()
    {
        return this.game_mode;
    }

    public void reset_game_pad_status()
    {
        this.carrot.game.set_enable_gamepad_console(false);
    }

    public void btn_show_setting_gamepad()
    {
        this.GetComponent<Game>().play_sound();
        this.gamepad1.show_setting_gamepad();
    }


}
