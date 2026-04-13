using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_scores : MonoBehaviour
{
    public Text txt_scores;
    public Text txt_scores_effect;
    public Text txt_hight_scores;
    public Text txt_hight_scores_gameover;
    public Text txt_best_scores_gameover;

    public Animator anim_scores_portrait;
    public Animator anim_scores_landspace;

    public Text txt_scores_landspace;
    public Text txt_scores_effect_landspace;
    public Text txt_hight_scores_landspace;
    public GameObject panel_hight_scores;

    [Header("Mode Game")]
    public Image img_home_mode_game_portrait;
    public Image img_home_mode_game_landspace;
    public Sprite[] sp_mode_game;
    private int mode_game;

    private int scores = 0;
    private int hight_scores = 0;

    public void load_data()
    {
        this.mode_game = this.GetComponent<Game>().get_game_mode();
        this.check_info_mode_game();

        this.txt_scores.text = this.scores.ToString();
        this.txt_scores_landspace.text= this.scores.ToString();
  
        this.txt_scores_effect.gameObject.SetActive(false);
        this.txt_scores_effect_landspace.gameObject.SetActive(false);
    }

    public void act_play_anin()
    {
        if (this.GetComponent<Game>().get_status_protain())
            this.anim_scores_portrait.Play("Scores_nomal");
        else
            this.anim_scores_landspace.Play("Scores_nomal");
    }

    public void add_scores(int socres_add)
    {
        if(this.GetComponent<Game>().get_status_protain())
            this.anim_scores_portrait.Play("Scores");
        else
            this.anim_scores_landspace.Play("Scores");

        this.scores += socres_add;

        this.txt_scores_effect.text = "+"+socres_add.ToString();
        this.txt_scores_effect.gameObject.SetActive(true);

        this.txt_scores_effect_landspace.text = "+" + socres_add.ToString();
        this.txt_scores_effect_landspace.gameObject.SetActive(true);

        this.GetComponent<Game>().carrot.delay_function(0.5f, this.stop_anim);
    }

    public void check_and_update_hight_scores()
    {
        if (this.scores > this.hight_scores)
        {
            this.hight_scores = this.scores;
            PlayerPrefs.SetInt("hight_scores_" + this.mode_game, this.scores);
            this.txt_hight_scores_gameover.text = "New top score :" + this.hight_scores;
            this.panel_hight_scores.SetActive(true);
            this.txt_hight_scores.text = this.hight_scores.ToString();
            this.txt_hight_scores_landspace.text = this.hight_scores.ToString();
        }
        else
            this.panel_hight_scores.SetActive(false);

        this.GetComponent<Game>().carrot.game.update_scores_player(this.hight_scores);

        this.txt_best_scores_gameover.text = "Highest score : "+this.hight_scores;
    }

    public void stop_anim()
    {
        this.txt_scores.text = this.scores.ToString();
        this.txt_scores_effect.gameObject.SetActive(false);

        this.txt_scores_landspace.text = this.scores.ToString();
        this.txt_scores_effect_landspace.gameObject.SetActive(false);

        if (this.GetComponent<Game>().get_status_protain())
            this.anim_scores_portrait.Play("Scores_nomal");
        else
            this.anim_scores_landspace.Play("Scores_nomal");
    }

    public void reset()
    {
        this.scores = 0;
        this.txt_scores.text = "0";
    }


    public void select_mode_game(int index_mode)
    {
        this.mode_game = index_mode;
        this.check_info_mode_game();
    }

    private void check_info_mode_game()
    {
        this.img_home_mode_game_landspace.sprite = this.sp_mode_game[this.mode_game];
        this.img_home_mode_game_portrait.sprite = this.sp_mode_game[this.mode_game];
        this.hight_scores = PlayerPrefs.GetInt("hight_scores_" + this.mode_game, 0);
        this.txt_hight_scores.text = this.hight_scores.ToString();
        this.txt_hight_scores_landspace.text = this.hight_scores.ToString();
    }
}
