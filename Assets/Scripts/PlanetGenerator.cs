using UnityEngine;

public class GuvenlikSistemi : MonoBehaviour
{
    public GameObject guenesPrefab;
    public GameObject gezegenPrefab;
    public int katmanSayisi = 5;
    public int gezegenSayisi = 10;
    public float katmanArasiUzaklik = 10f;
    public Material[] gunesMateryalListesi; // G�ne� materyallerini Unity Edit�r�'nde ayarlay�n
    public Material[] hazirGezegenMalzemeler; // Gezegen materyallerini Unity Edit�r�'nde ayarlay�n

    public GameObject detayModalPrefab;

    void Start()
    {
        // G�ne� prefab'�n� kontrol et
        if (guenesPrefab == null)
        {
            Debug.LogError("G�ne� prefab'� belirtilmemi�!");
            return;
        }

        // Gezegen prefab'�n� kontrol et
        if (gezegenPrefab == null)
        {
            Debug.LogError("Gezegen prefab'� belirtilmemi�!");
            return;
        }

        // G�ne� materyal listesini kontrol et
        if (gunesMateryalListesi == null || gunesMateryalListesi.Length == 0)
        {
            Debug.LogError("G�ne� materyal listesi belirtilmemi� veya bo�!");
            return;
        }

        // Gezegen materyal dizisini kontrol et
        if (hazirGezegenMalzemeler == null || hazirGezegenMalzemeler.Length == 0)
        {
            Debug.LogError("Gezegen materyal dizisi belirtilmemi� veya bo�!");
            return;
        }

        // Rastgele bir g�ne� materyali se�
        Material secilenGunesMateryali = gunesMateryalListesi[Random.Range(0, gunesMateryalListesi.Length)];

        // Ana g�ne�i olu�tur
        GameObject anaGunes = Instantiate(guenesPrefab, Vector3.zero, Quaternion.identity);

        // Ana g�ne�in materyalini de�i�tir
        Renderer anaGunesRenderer = anaGunes.GetComponent<Renderer>();
        if (anaGunesRenderer != null)
        {
            anaGunesRenderer.material = secilenGunesMateryali;
        }
        else
        {
            Debug.LogError("Ana G�ne� �zerinde Renderer bile�eni bulunamad�!");
            return;
        }

        // Katmanlar halinde gezegenleri olu�tur
        for (int katman = 0; katman < katmanSayisi; katman++)
        {
            // Birinci katmanda gezegen olu�turma
            if (katman > 0)
            {
                for (int gezegenIndex = 0; gezegenIndex < gezegenSayisi; gezegenIndex++)
                {
                    // Gezegeni rastgele bir konumda olu�tur
                    float aci = Random.Range(0f, 360f);
                    float radyanAci = Mathf.Deg2Rad * aci;
                    float x = Mathf.Cos(radyanAci) * (katmanArasiUzaklik * katman);
                    float z = Mathf.Sin(radyanAci) * (katmanArasiUzaklik * katman);

                    Vector3 gezegenKonumu = new Vector3(x, 0f, z);

                    // Rastgele bir gezegen materyali se�
                    Material rastgeleGezegenMateryal = hazirGezegenMalzemeler[Random.Range(0, hazirGezegenMalzemeler.Length)];

                    // Gezegeni olu�tur ve ana g�ne�in alt�nda yerle�tir
                    GameObject yeniGezegen = Instantiate(gezegenPrefab, gezegenKonumu, Quaternion.identity);
                    yeniGezegen.transform.SetParent(anaGunes.transform);

                    // Gezegenin materyalini de�i�tir
                    Renderer yeniGezegenRenderer = yeniGezegen.GetComponent<Renderer>();
                    if (yeniGezegenRenderer != null)
                    {
                        yeniGezegenRenderer.material = rastgeleGezegenMateryal;
                    }
                    else
                    {
                        Debug.LogError("Gezegen �zerinde Renderer bile�eni bulunamad�!");
                    }

                    // Gezegen �zerine t�klama alg�lama
                    GezegenDetayScript gezegenDetayScript = yeniGezegen.AddComponent<GezegenDetayScript>();
                    gezegenDetayScript.detayModalPrefab = detayModalPrefab; // Detay modal prefab'�n� atay�n
                }


            }
        }
    }

    public class GezegenDetayScript : MonoBehaviour
    {
        public GameObject detayModalPrefab;

        void OnMouseDown()
        {
            // Gezegene t�kland���nda detay modal� a�
            if (detayModalPrefab != null)
            {
                Instantiate(detayModalPrefab);
            }
            else
            {
                Debug.LogError("Detay modal prefab'� belirtilmemi�!");
            }
        }
    }
}
