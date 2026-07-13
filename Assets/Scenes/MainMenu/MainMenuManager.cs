using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pengaturan Audio")]
    public AudioSource bgmSource; // Wadah untuk memasukkan BGM_Manager dari Inspector

    // Fungsi untuk Tombol Mulai
    public void MulaiGame()
    {
        // Pastikan nama "Level1" sama persis dengan nama scene level pertama Anda
        SceneManager.LoadScene("Level1"); 
    }

    // Fungsi untuk Tombol Keluar
    public void KeluarAplikasi()
    {
        // Pesan ini hanya akan muncul di console Unity sebagai penanda saat diuji coba
        Debug.Log("Keluar dari Game!"); 
        
        // Perintah ini akan menutup aplikasi sepenuhnya (berlaku saat game sudah di-build)
        Application.Quit();
    }

    // Fungsi Dinamis untuk Slider Volume
    public void AturVolume(float volumeBaru)
    {
        // Memastikan ada Audio Source yang terpasang agar tidak error
        if (bgmSource != null) 
        {
            // Mengubah volume Audio Source sesuai dengan nilai pergeseran Slider (0 sampai 1)
            bgmSource.volume = volumeBaru; 
        }
    }
}