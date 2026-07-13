using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct DataDialog
{
    public string NamaKarakter;
    [TextArea(2, 5)] public string Kalimat;
    public Sprite PotretKarakter;
}

public class Level1Manager : MonoBehaviour
{
    [Header("Pengaturan Efek Teks")]
    public float KecepatanNgetik = 0.03f;
    private Coroutine efekNgetik;
    private bool sedangNgetik = false;

    [Header("Cutscene Awal")]
    public GameObject PanelCutsceneAwal;
    public Image PotretPembicara;
    public TMP_Text TeksNama;
    public TMP_Text TeksCeritaAwal;
    public DataDialog[] PercakapanAwal;
    private int indexDialogAwal = 0;

    [Header("Pilih Karakter")]
    public GameObject PanelPilihKarakter;
    public GameObject[] KarakterUI; // Masukkan Karakter1, Karakter2, Karakter3
    public TMP_Text TeksNamaKarakter;
    private int indexKarakterTerpilih = 0;
    private string[] namaKarakterList = { "Ninja Hijau", "Pendekar Merah", "Ninja Hitam" };

    [Header("Cutscene Akhir")]
    public GameObject PanelCutsceneAkhir;
    public Image PotretPembicaraAkhir;
    public TMP_Text TeksNamaAkhir;
    public TMP_Text TeksCeritaAkhir;
    public DataDialog[] PercakapanAkhir;
    private int indexDialogAkhir = 0;

    [Header("Gameplay")]
    public GameObject[] VisualKarakterPlayer; // Masukkan VisualNinja1, 2, 3
    public GameObject TombolTanganUI;

    void Start()
    {
        PanelPilihKarakter.SetActive(false);
        PanelCutsceneAkhir.SetActive(false);
        TombolTanganUI.SetActive(false);
        foreach (GameObject v in VisualKarakterPlayer) v.SetActive(false);

        PanelCutsceneAwal.SetActive(true);
        TampilkanDialogAwal();
    }

    // --- LOGIKA CUTSCENE AWAL ---
    void TampilkanDialogAwal()
    {
        if (indexDialogAwal < PercakapanAwal.Length)
        {
            DataDialog d = PercakapanAwal[indexDialogAwal];
            TeksNama.text = d.NamaKarakter;
            PotretPembicara.sprite = d.PotretKarakter;
            if (efekNgetik != null) StopCoroutine(efekNgetik);
            efekNgetik = StartCoroutine(KetikTeks(d.Kalimat, TeksCeritaAwal));
        }
        else
        {
            PanelCutsceneAwal.SetActive(false);
            PanelPilihKarakter.SetActive(true);
            UpdateUIPilihKarakter();
        }
    }

    public void LanjutDialogAwal()
    {
        if (sedangNgetik) { StopCoroutine(efekNgetik); TeksCeritaAwal.text = PercakapanAwal[indexDialogAwal].Kalimat; sedangNgetik = false; }
        else { indexDialogAwal++; TampilkanDialogAwal(); }
    }

    // --- LOGIKA PILIH KARAKTER ---
    public void GeserKanan() { indexKarakterTerpilih = (indexKarakterTerpilih + 1) % KarakterUI.Length; UpdateUIPilihKarakter(); }
    public void GeserKiri() { indexKarakterTerpilih = (indexKarakterTerpilih - 1 + KarakterUI.Length) % KarakterUI.Length; UpdateUIPilihKarakter(); }

    void UpdateUIPilihKarakter()
    {
        for (int i = 0; i < KarakterUI.Length; i++) KarakterUI[i].SetActive(i == indexKarakterTerpilih);
        TeksNamaKarakter.text = namaKarakterList[indexKarakterTerpilih];
    }

    public void KonfirmasiKarakter()
    {
        PanelPilihKarakter.SetActive(false);
        PanelCutsceneAkhir.SetActive(true);
        TampilkanDialogAkhir();
    }

    // --- LOGIKA CUTSCENE AKHIR ---
    void TampilkanDialogAkhir()
    {
        if (indexDialogAkhir < PercakapanAkhir.Length)
        {
            DataDialog d = PercakapanAkhir[indexDialogAkhir];
            TeksNamaAkhir.text = d.NamaKarakter;
            PotretPembicaraAkhir.sprite = d.PotretKarakter;
            if (efekNgetik != null) StopCoroutine(efekNgetik);
            efekNgetik = StartCoroutine(KetikTeks(d.Kalimat, TeksCeritaAkhir));
        }
        else
        {
            PanelCutsceneAkhir.SetActive(false);
            VisualKarakterPlayer[indexKarakterTerpilih].SetActive(true);
            TombolTanganUI.SetActive(true);
        }
    }

    public void LanjutDialogAkhir()
    {
        if (sedangNgetik) { StopCoroutine(efekNgetik); TeksCeritaAkhir.text = PercakapanAkhir[indexDialogAkhir].Kalimat; sedangNgetik = false; }
        else { indexDialogAkhir++; TampilkanDialogAkhir(); }
    }

    IEnumerator KetikTeks(string kalimat, TMP_Text target)
    {
        sedangNgetik = true; target.text = "";
        foreach (char c in kalimat.ToCharArray()) { target.text += c; yield return new WaitForSeconds(KecepatanNgetik); }
        sedangNgetik = false;
    }
}