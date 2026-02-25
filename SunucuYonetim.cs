using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SunucuYonetim : MonoBehaviourPunCallbacks
{

    GameObject serverbilgi;
    GameObject Ad_kaydet;
    GameObject Random_giris;
    GameObject Oda_kur_ve_gir;
   public bool butonlami;
    void Start()
    {
                
        serverbilgi = GameObject.FindWithTag("Server_bilgi");
        Ad_kaydet = GameObject.FindWithTag("Ad_kaydet_buton");
        Random_giris = GameObject.FindWithTag("Random_giris_yap");
        Oda_kur_ve_gir = GameObject.FindWithTag("Oda_kur_ve_gir");

       
        PhotonNetwork.ConnectUsingSettings();

        DontDestroyOnLoad(gameObject);
    }


    public override void OnConnectedToMaster()
    {
        serverbilgi.GetComponent<Text>().text = "Servere Bağlandı";
       
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        serverbilgi.GetComponent<Text>().text = "Lobiye Bağlandı";
        

        if (!PlayerPrefs.HasKey("Kullanıcıadi"))
        {
            Ad_kaydet.GetComponent<Button>().interactable = true;
        }else
        {
            Random_giris.GetComponent<Button>().interactable = true;
            Oda_kur_ve_gir.GetComponent<Button>().interactable = true;
        }
           
           
    }

    public void RandomGirisYap()
    {
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinRandomRoom();

    }
    public void OdaOlusturvegir()
    {
        PhotonNetwork.LoadLevel(1);
        string odaadi = Random.Range(0, 9964124).ToString();
        PhotonNetwork.JoinOrCreateRoom(odaadi, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {

        InvokeRepeating("BilgileriKontrolEt", 0, 1f);
        GameObject objem = PhotonNetwork.Instantiate("Oyuncu",Vector3.zero,Quaternion.identity,0,null);
        objem.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("Kullanıcıadi");

        if (PhotonNetwork.PlayerList.Length==2)
        {
            objem.gameObject.tag = "Oyuncu_2";
            GameObject.FindWithTag("GameKontrol").gameObject.GetComponent<PhotonView>().RPC("Basla", RpcTarget.All);
        }
       
    }

    public override void OnLeftRoom()

    {
        if (butonlami)
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();

        }else
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
            //  Debug.Log("Sen Çıktın");
              PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);
             PlayerPrefs.SetInt("Maglubiyet", PlayerPrefs.GetInt("Maglubiyet") + 1);
        }
    }

    public override void OnLeftLobby()

    {
        // lobiden

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // bir oyuncu girdiyse

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)

    {
        if (butonlami)
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();

        }
        else
        {
            Time.timeScale = 1;
          PhotonNetwork.ConnectUsingSettings();
          PlayerPrefs.SetInt("Toplam_mac", PlayerPrefs.GetInt("Toplam_mac") + 1);       
          PlayerPrefs.SetInt("Galibiyet", PlayerPrefs.GetInt("Galibiyet") + 1);
          PlayerPrefs.SetInt("Toplam_puan", PlayerPrefs.GetInt("Toplam_puan") + 150);
        }



        // Debug.Log("Rakip Çıktı");
        // bir oyuncu çıktıysa
        InvokeRepeating("BilgileriKontrolEt", 0, 1f);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
       {
        serverbilgi.GetComponent<Text>().text = "Odaya Girilemedi";

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    
    {
        serverbilgi.GetComponent<Text>().text = "Random bir odaya girilemedi";

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        serverbilgi.GetComponent<Text>().text = "Oda Oluşturulamadı";

    }


    void BilgileriKontrolEt()
    {

        if (PhotonNetwork.PlayerList.Length==2)
        {
            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(false);
            GameObject.FindWithTag("Oyuncu_1_isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Oyuncu_2_isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
            CancelInvoke("BilgileriKontrolEt");           
        }
        else
        {

            GameObject.FindWithTag("OyuncuBekleniyor").SetActive(true);
            GameObject.FindWithTag("Oyuncu_1_isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("Oyuncu_2_isim").GetComponent<TextMeshProUGUI>().text = ".......";
        }

       

    }


}
