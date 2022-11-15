using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class goblin_cads: MonoBehaviour
{
    public Slider slid;
    public AudioSource Pig, Stil_sound, twenty_one, great_steal, take_pm, take_desert, kozel, take_card, background;
    public AudioSource[] Mems;
    public Button morebutt, enoghbutt,  Bet_butt, still_money, buy_memes;
    public TMP_Text coinscreen, slider_value;
    public Transform StartPos;
    public Transform card_pos_scene, coin_pos_scene;
    public GameObject[] Gun;
    public GameObject[] GunPos;
    public GameObject[] GobEndPoint;
    public GameObject[] PlayEndPoint;
    public GameObject[] face;
    public GameObject[] coins;
    public GameObject[] CoinEndpoint;
    private GameObject[] coinmove = new GameObject[100];
    private GameObject[] gmove = new GameObject[36];
    private GameObject[] pmove = new GameObject[36];
    public float gstep, pstep ,gscore, pscore;
    public int coin = 100, bet; 
    public int gob, play ,k,c, ppos, gpos,mem_numb = 0;
    public float[] ingame = new float[36];
    private int[] score = {8, 0, 10, 2, 3, 4, 11, 6, 7, 11, 4, 11, 6, 7, 8, 10, 2, 3, 0, 8, 7, 6, 6, 7, 8, 0, 10, 2, 3, 4, 0, 11, 4, 3, 2, 10};
    public bool fight, fight2,fight3, p = false,g = false;



    void Start()
    {
        fight = false;
        still_money.interactable = true;
        slid.interactable = true;
        slid.value = 0;
        Bet_butt.interactable = true;
        morebutt.interactable = true;
        enoghbutt.interactable = true;
        gscore = 0;
        pscore = 0;
        ppos = -1;
        gpos = -1;
        k = -1;
        c = -1;
        coinscreen.text = "Coin: " + coin;
        slid.maxValue = coin;

        goblinspawn();
        playspawn();
    }
      
    IEnumerator wait_pm()
    {
        yield return new WaitForSeconds(5);
        Gun[1].transform.position = GunPos[1].transform.position;
        if (Random.Range(0,2) == 0)
        {
            background.Pause();
            take_desert.Play();
            yield return new WaitForSeconds(30);
            Gun[2].transform.position = GunPos[2].transform.position;
            yield return new WaitForSeconds(27);
            coin = 0;
        }
        else coin +=200;  
        still_money.interactable = false;
        for (int i = 0; i < 3; ++i) Gun[i].transform.position = GunPos[3].transform.position;
        change_info();
        background.Play();
    }
    private void change_info()
    {
        if(morebutt.interactable == false) coinscreen.text = "Coin: " + coin + "\nPlayer: "+ pscore+"\nGoblin: "+gscore;
        else coinscreen.text = "Coin: " + coin + "\nPlayer: "+ pscore;
        slid.maxValue = coin;

    }
    public void buy_mem()
    {
        if (!(Mems[mem_numb].isPlaying) && (coin >=30) && !(great_steal.isPlaying) && !(Stil_sound.isPlaying))
        {
            mem_numb = Random.Range(0,22);
            coin -= 30;
            slid.maxValue = coin;
            change_info();
            Mems[mem_numb].Play();
        }
    }
    public void Still()
    {
        if (fight)
        {
            take_pm.Play();
            Gun[1].transform.position = GunPos[1].transform.position;
            StartCoroutine(wait_pm());
            still_money.interactable = false;
        }
        else if (!(Mems[mem_numb].isPlaying) && !(great_steal.isPlaying) && !(Stil_sound.isPlaying))
        { 
            still_money.interactable = false;
            if (Random.Range(0,2) == 0)
            {
                Stil_sound.Play();
                coin /=2;
            }else
            {
                if (Random.Range(0,2) == 0) 
                { 
                    fight = true;
                    Gun[0].transform.position = GunPos[0].transform.position;
                    still_money.interactable = true;
                    kozel.Play();
                } else
                {
                    great_steal.Play();
                    coin += 100;
                }
            }
        }
        change_info();
    }

    public void make_bet()
    {
        Pig.Play();
        if (slid.interactable != false)
        {
            slid.interactable = false;
            bet = int.Parse (slid.value.ToString("0"));
            coin -= bet;
            change_info();
            for (int i = 0; i < bet/100;++i) coinmove[++c] = Instantiate(coins[0], CoinEndpoint[0].transform.position+ new Vector3(0,i*0.3f,0),coins[0].transform.rotation, coin_pos_scene);
            for (int i = 0; i < bet/10-(bet/100)*10;++i) coinmove[++c] = Instantiate(coins[1], CoinEndpoint[1].transform.position + new Vector3(0,i*0.3f,0), coins[1].transform.rotation, coin_pos_scene);
            for (int i = 0; i < bet%10;++i) coinmove[++c] = Instantiate(coins[2], CoinEndpoint[2].transform.position+ new Vector3(0,i*0.3f,0), coins[2].transform.rotation, coin_pos_scene);
        }

    }
    private void goblinspawn()
    {
        ++gpos;
        while(true)
        {
            if (check(gob = Random.Range(0,36))) break;
        }
        take_card.Play();
        ingame[++k] = gob;
        gscore += score[gob];
        gmove[gpos] = Instantiate(face[gob],StartPos.transform.position,StartPos.transform.rotation,card_pos_scene);
        gstep = 0.01f;
        g = true;
    }
    public void playspawn()
    {
        if (ppos == 0) slid.interactable = false;
        morebutt.interactable = false;
        if (enoghbutt.interactable == false) CleanScene();
        else
        {
            if (pscore <= 21)
            {
                ++ppos;
                while(true)
                {
                    if (check(play = Random.Range(0,36))) break;
                }
                take_card.Play();
                ingame[++k] = play;
                pscore += score[play];
                pmove[ppos] = Instantiate(face[play],StartPos.transform.position,StartPos.transform.rotation,card_pos_scene);
                pstep = 0.01f; 
                p = true;

            }else Enough();
        }
    }
    public void Enough()
    {
        slid.interactable = false;
        morebutt.interactable = false;
        enoghbutt.interactable = false;
        g = true;   
    }
    public float x= 180 ,y=0 ,z = 45 , w = 0;
    void FixedUpdate()
    {
        if (g)
        {
            gmove[gpos].transform.position = Vector3.MoveTowards(gmove[gpos].transform.position,GobEndPoint[gpos].transform.position, 0.8f);
            gstep +=0.01f;
            if (gstep > 0.6)
            {
                g = false;
                if (enoghbutt.interactable == false)
                { 
                    change_info();
                    gmove[gpos].transform.localScale = new Vector3(1.1f,1.1f,1.1f);
                    gmove[gpos].transform.Rotate(180,0, 45);
                    gmove[gpos].transform.Rotate(-10,0, 0);
                    if (gscore == 21) twenty_one.Play();
                    if ((gscore > pscore) && (gscore <= 21) )  morebutt.interactable = true;
                    else if (gscore <=16) goblinspawn();
                    else
                    {
                        if ((gscore < pscore) || (gscore > 21)) coin += (2*bet);  
                        if (gscore == pscore) coin += bet;  
                        morebutt.interactable = true;
                        change_info();
                    }
                }      
            }
        } 
        if (p)
        {
            pmove[ppos].transform.position = Vector3.MoveTowards(pmove[ppos].transform.position,PlayEndPoint[ppos].transform.position, 0.8f);
            pstep +=0.01f;
            if (pstep > 0.6)
            {   
                morebutt.interactable = true;
                change_info();
                pmove[ppos].transform.Rotate(180,0, 45);
                pmove[ppos].transform.Rotate(-10,0, 0);
                pstep -=0.08f;
                p = false;
                if (pscore == 21)
                {
                    coin += (2*bet);  
                    change_info();
                    enoghbutt.interactable = false;

                }else if (pscore > 21)
                {
                    enoghbutt.interactable = false;
                }
            }
        } 
    }
    public void CleanScene()
    {
        for (int i = 0; i <= gpos; ++i) Destroy(gmove[i]);
        for (int i = 0; i <= ppos; ++i) Destroy(pmove[i]);
        for (int i = 0; i <= c; ++i) Destroy(coinmove[i]);
        for (int i = 0; i < 3; ++i) Gun[i].transform.position = GunPos[3].transform.position;
        Start();
    }
    public void change_slider()
    {
        slider_value.text = slid.value.ToString();
    }
    private bool check(int a)
    {
        bool t = true;
        for(int i = 0; i < 36;++i )
        {
            if (ingame[i] == a) 
            {
                t = false;
                break;
            }
        }
        return(t);

    }
   
}
    // private void takegobcard()
    // {
    //     gstep = 0.01f;
    //     while(true)
    //     {
    //         if (check(gob = Random.Range(0,35))) break;
    //     }
    //     ingame[++k] = gob;
    //     gscore += score[gob];
    //     face[gob].transform.position = StartPos.transform.position;
    //     face[gob].transform.rotation = StartPos.transform.rotation;
    //     g = true;
    // }
    
    // public void takeplaycard()
    // {
    //     while(true)
    //     {
    //         if (check(play = Random.Range(0,35))) break;
    //     }
    //     ingame[++k] = play;
    //     pscore += score[play];
    //     face[play].transform.position = StartPos.transform.position;
    //     face[play].transform.rotation = StartPos.transform.rotation;
    //     pstep = 0.01f; 
    //     p = true;
    // }