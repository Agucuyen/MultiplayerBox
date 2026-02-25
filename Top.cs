using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    float darbegucu;
    int benkimim;


    GameObject gameKontrol;
    GameObject Oyuncu;
    PhotonView pw;
    AudioSource YokOlmaSesi;


    void Start()
    {
        darbegucu = 20;
        gameKontrol = GameObject.FindWithTag("GameKontrol");
        pw = GetComponent<PhotonView>();
        YokOlmaSesi = GetComponent<AudioSource>();
    }
    [PunRPC]
    public void TagAktar(string gelentag)
    {
        Oyuncu = GameObject.FindWithTag(gelentag);

        if (gelentag == "Oyuncu_1")
            benkimim = 1;
        else
            benkimim = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Ortadaki_kutular"))
        {
            collision.gameObject.GetComponent<PhotonView>().RPC("darbeal", RpcTarget.All, darbegucu);
            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();


            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);


        }
        if (collision.gameObject.CompareTag("Oyuncu_2_Kule") || collision.gameObject.CompareTag("Oyuncu_2"))
        {
            if (benkimim != 2)
            {
                gameKontrol.GetComponent<PhotonView>().RPC("Darbe_vur", RpcTarget.All, 2, darbegucu);

            }

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();


            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Oyuncu_1_Kule") || collision.gameObject.CompareTag("Oyuncu_1"))
        {
            if (benkimim != 1)
            {
                gameKontrol.GetComponent<PhotonView>().RPC("Darbe_vur", RpcTarget.All, 1, darbegucu);

            }
            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("Zemin"))
        {

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("Tahta"))
        {

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("Odul"))
        {
            gameKontrol.GetComponent<PhotonView>().RPC("SaglikDoldur", RpcTarget.All, benkimim);
            PhotonNetwork.Destroy(collision.transform.gameObject);
            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();
            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("Top"))
        {

            Oyuncu.GetComponent<Oyuncu>().PowerOynasin();

            PhotonNetwork.Instantiate("Duman_puf_Carpma_efekti", transform.position, transform.rotation, 0, null);
            YokOlmaSesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }



    }



}
