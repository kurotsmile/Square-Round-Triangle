using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray_Create : MonoBehaviour
{
    public Transform Tray_area_all;
    public Transform tr_obj_tray;
    public Transform tr_obj_tray_portain;
    public Transform tr_obj_tray_landspace;

    public Transform area_tray_create_tomb;
    public Transform area_create_tomb;
    public GameObject Tomb_prefab;
    public GameObject Tomb_data_prefab;

    private bool is_drag_tomb = false;
    private Tray tray_temp=null;
    private List<Tomb_data> list_type;
    private int index_sp_type;
    private int index_color_type;

    private List<GameObject> list_tomb_create_object;
    private List<int[]> list_create_data_tomb_type;
    public Sprite[] sp_circle;
    public Sprite[] sp_square;
    public Sprite[] sp_triangle;

    public Color32[] color_spring;
    public Color32[] color_summer;
    public Color32[] color_winter;
    public Color32[] color_fall;

    private List<Sprite[]> list_sp_skin;
    private List<Color32[]> list_color_skin;

    public void load_data()
    {
        this.list_sp_skin = new List<Sprite[]>();
        this.list_sp_skin.Add(sp_circle);
        this.list_sp_skin.Add(sp_square);
        this.list_sp_skin.Add(sp_triangle);

        this.index_sp_type = Random.Range(0, this.list_sp_skin.Count);

        this.list_color_skin = new List<Color32[]>();
        this.list_color_skin.Add(this.color_fall);
        this.list_color_skin.Add(this.color_summer);
        this.list_color_skin.Add(this.color_winter);
        this.list_color_skin.Add(this.color_spring);

        this.index_color_type=Random.Range(0,this.list_color_skin.Count);

        this.list_type = new List<Tomb_data>();

        this.add_type_data_tomb(true, false, false, false, 0);
        this.add_type_data_tomb(false, true, false, false, 1);
        this.add_type_data_tomb(false, false, true, false, 2);
        this.add_type_data_tomb(false, false, false, true, 3);

        this.list_create_data_tomb_type = new List<int[]>();

        this.create_data_tomb_type(0);
        this.create_data_tomb_type(1);
        this.create_data_tomb_type(2);
        this.create_data_tomb_type(3);

        if (this.GetComponent<Game>().get_game_mode() >= 1){ 
            this.create_data_tomb_type(0,1);
            this.create_data_tomb_type(0,2);
            this.create_data_tomb_type(0,3);

            this.create_data_tomb_type(1, 2);
            this.create_data_tomb_type(1, 3);
        }

        if (this.GetComponent<Game>().get_game_mode() >= 2)
        {
           this.create_data_tomb_type(0,1,2);
           this.create_data_tomb_type(0,1,3);
           this.create_data_tomb_type(1,0,3);
           this.create_data_tomb_type(1,2,3);
           this.create_data_tomb_type(2,0,1);
           this.create_data_tomb_type(2,0,3);
        }


        this.create_tomb();

        this.tr_obj_tray.gameObject.SetActive(false);
    }

    private void add_type_data_tomb(bool obj_visible1, bool obj_visible2, bool obj_visible3, bool obj_visible4, int type_tray)
    {
        GameObject obj_t_data = Instantiate(Tomb_data_prefab);
        obj_t_data.transform.SetParent(this.transform);
        Tomb_data t_data = obj_t_data.GetComponent<Tomb_data>();
        bool[] type_bol = { obj_visible1, obj_visible2, obj_visible3, obj_visible4 };
        t_data.visible_tray = type_bol;
        t_data.type = type_tray;
        this.list_type.Add(t_data);
    }

    private void create_data_tomb_type(int t_0, int t_1 = -1, int t_2 = -1, int t_3 = -1)
    {
        int[] create_type_arr = {t_0, t_1, t_2, t_3};
        this.list_create_data_tomb_type.Add(create_type_arr);
    }

    private void Update()
    {
        if (this.is_drag_tomb)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(2f);
            rayPoint.z = 0f;
            this.area_create_tomb.position = rayPoint;
        }
    }

    public void drag_tomb()
    {
        this.is_drag_tomb = true;
    }

    public void drop_tomb()
    {
        this.area_create_tomb.position = this.area_tray_create_tomb.position;
        this.is_drag_tomb = false;
        if (this.tray_temp == null)
        {
            GameObject.Find("Game").GetComponent<Game>().play_sound(3);
        }
        else
        {
            this.tray_temp.drop_tomb();
            this.tray_temp = null;
        }

    }

    public void set_tray_temp(Tray t)
    {
        this.tray_temp = t;
    }

    public void set_none_tray_temp()
    {
        this.tray_temp = null;
    }

    public void create_tomb()
    {
        this.list_tomb_create_object = new List<GameObject>();
        this.GetComponent<Game>().carrot.clear_contain(this.area_create_tomb);
        int index_r_type = Random.Range(0, this.list_create_data_tomb_type.Count);
        int[] c_type = this.list_create_data_tomb_type[index_r_type];
        for (int i = 0; i < c_type.Length; i++)
        {
            if (c_type[i] != -1)
            {
                GameObject Tomb_item = Instantiate(this.Tomb_prefab);
                Tomb_item.transform.SetParent(this.area_create_tomb);
                Tomb_item.transform.localScale = new Vector3(1f, 1f, 1f);
                Tomb_item.transform.localPosition = Vector3.zero;
                this.list_type[c_type[i]].index_color = Random.Range(0, this.get_list_color_tomb().Length);
                this.list_type[c_type[i]].color_tray = this.get_list_color_tomb()[this.list_type[c_type[i]].index_color];
                Tomb_item.GetComponent<Tomb>().load_data(this.list_type[c_type[i]]);
                Tomb_item.GetComponent<Tomb>().load_skin(this.list_sp_skin[this.index_sp_type]);
                list_tomb_create_object.Add(Tomb_item);
            }
        }
    }

    private Color32[] get_list_color_tomb()
    {
        return this.list_color_skin[this.index_color_type];
    }

    public List<GameObject> get_list_tomb_create()
    {
        return this.list_tomb_create_object;
    }

    public void set_rotate_scene(bool is_portain)
    {
        this.tr_obj_tray.gameObject.SetActive(true);
        if (is_portain)
        {
            this.Tray_area_all.localPosition = Vector3.zero;
            this.tr_obj_tray.position = this.tr_obj_tray_portain.position;
        }
        else
        {
            this.Tray_area_all.localPosition = new Vector3(0f, 20f, 0f);
            this.tr_obj_tray.position = this.tr_obj_tray_landspace.position;
        }
    }

    public void reset()
    {
        this.index_sp_type = Random.Range(0, this.list_sp_skin.Count);
        this.index_color_type = Random.Range(0, this.list_color_skin.Count);
        this.create_tomb();
        this.drop_tomb();
    }
}
