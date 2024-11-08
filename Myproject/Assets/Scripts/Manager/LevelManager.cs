using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        //do Something on Awake E.g. make an object appearence false
    }


    //Ini adalah sebuah coroutine yang memuat scene baru secara asinkron (di latar belakang)
    //sehingga tidak menyebabkan permainan berhenti sejenak
    IEnumerator LoadSceneAsync(string sceneName)
    {
        //Memicu animasi transisi dengan mengaktifkan trigger bernama "animation_triger"
        animator.SetTrigger("animation_triger");

        //Menghentikan sementara eksekusi selama 1 detik untuk memungkinkan animasi transisi berjalan.
        yield return new WaitForSeconds(1);

        //Memuat scene baru secara asinkron berdasarkan sceneName
        SceneManager.LoadSceneAsync(sceneName);


        //Menjalankan animasi akhir dari transisi setelah scene baru dimuat
        Player.Instance.transform.position = new(0, -4.5f);

        //Menjalankan animasi akhir dari transisi setelah scene baru dimuat.
        animator.SetTrigger("end_triger");

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}