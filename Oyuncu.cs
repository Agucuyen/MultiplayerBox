using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncu : MonoBehaviour
{
    public GameObject top;
    public GameObject TopCikisnoktasi;
    public ParticleSystem TopAtisEfekt;
    public AudioSource TopAtmaSesi;
    float AtisYonu;
     

   [Header("GÜÇ BARI AYARLARI")]
    Image PowerBar;
    float powerSayi;    
    bool sonageldimi=false;
    Coroutine powerDongu;

    PhotonView pw;
    bool AtesAktifmi=false;
    void Start()
    {      

        pw = GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            PowerBar = GameObject.FindWithTag("PowerBar").GetComponent<Image>();
            if (PhotonNetwork.IsMasterClient)
            {
                gameObject.tag = "Oyuncu_1";
                transform.position = GameObject.FindWithTag("OlusacakNokta_1").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta_1").transform.rotation;
                AtisYonu = 2f;                
            }
            else
            {
                gameObject.tag = "Oyuncu_2";
                transform.position = GameObject.FindWithTag("OlusacakNokta_2").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta_2").transform.rotation;
                AtisYonu = -2f;
                
            }

        }
        InvokeRepeating("Oyunbasladimi", 0, .5f);

    }
    public void Oyunbasladimi()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (pw.IsMine)
            {
                powerDongu = StartCoroutine(PowerBarCalistir());
                CancelInvoke("Oyunbasladimi");

            }
           

        }else
        {
            StopAllCoroutines();
        }
    }
    IEnumerator PowerBarCalistir()
    {
        PowerBar.fillAmount = 0;
        sonageldimi = false;
        AtesAktifmi = true;

        while (true)
        {
            if (PowerBar.fillAmount < 1 && !sonageldimi)
            {
                powerSayi = 0.01f;
                PowerBar.fillAmount += powerSayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);

            }else
            {
                sonageldimi = true;
                powerSayi = 0.01f;
                PowerBar.fillAmount -= powerSayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);

                if (PowerBar.fillAmount==0)
                {
                    sonageldimi = false;

                }

            }


        }

    }

    
    
    void Update()
    {
              

        if (pw.IsMine)
        {
            if (Input.touchCount > 0 && AtesAktifmi) // dokunma eklenecek
            {
               
                PhotonNetwork.Instantiate("Patlama_efekt", TopCikisnoktasi.transform.position, TopCikisnoktasi.transform.rotation, 0, null);
                TopAtmaSesi.Play();
                GameObject topobjem = PhotonNetwork.Instantiate("Top", TopCikisnoktasi.transform.position, TopCikisnoktasi.transform.rotation, 0, null);


                topobjem.GetComponent<PhotonView>().RPC("TagAktar",RpcTarget.All, gameObject.tag);

                Rigidbody2D rg = topobjem.GetComponent<Rigidbody2D>();
                rg.AddForce(new Vector2(AtisYonu, 0f) * PowerBar.fillAmount * 12f, ForceMode2D.Impulse);
                AtesAktifmi = false;
                StopCoroutine(powerDongu);
               


            }

        }

       

        
    }


    public void PowerOynasin()
    {
        powerDongu = StartCoroutine(PowerBarCalistir());
    }
   
    public void sonuc(int deger)
    {
       
        if (pw.IsMine)
        {

            if (PhotonNetwork.IsMasterClient)
                {

                if (deger==1)
                {
                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
                    PlayerPrefs.SetInt("Toplam_puan", PlayerPrefs.GetInt("Toplam_puan") + 150);
                }
                else
                {
                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);

                }

            }
            else
            {


                if (deger == 2)
                {
                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
                    PlayerPrefs.SetInt("Toplam_puan", PlayerPrefs.GetInt("Toplam_puan") + 150);
                }
                else
                {
                    PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
                    PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);

                }


            }





        }

        Time.timeScale = 0;


    }
}
