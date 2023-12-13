using UnityEngine;

public class GuvenlikSistemi : MonoBehaviour
{
    public GameObject guenesPrefab;
    public GameObject gezegenPrefab;
    public int katmanSayisi = 5;
    public int gezegenSayisi = 10;
    public float katmanArasiUzaklik = 10f;
    public Material[] gunesMateryalListesi; // Güneþ materyallerini Unity Editörü'nde ayarlayýn
    public Material[] hazirGezegenMalzemeler; // Gezegen materyallerini Unity Editörü'nde ayarlayýn

    public GameObject detayModalPrefab;

    void Start()
    {
        // Güneþ prefab'ýný kontrol et
        if (guenesPrefab == null)
        {
            Debug.LogError("Güneþ prefab'ý belirtilmemiþ!");
            return;
        }

        // Gezegen prefab'ýný kontrol et
        if (gezegenPrefab == null)
        {
            Debug.LogError("Gezegen prefab'ý belirtilmemiþ!");
            return;
        }

        // Güneþ materyal listesini kontrol et
        if (gunesMateryalListesi == null || gunesMateryalListesi.Length == 0)
        {
            Debug.LogError("Güneþ materyal listesi belirtilmemiþ veya boþ!");
            return;
        }

        // Gezegen materyal dizisini kontrol et
        if (hazirGezegenMalzemeler == null || hazirGezegenMalzemeler.Length == 0)
        {
            Debug.LogError("Gezegen materyal dizisi belirtilmemiþ veya boþ!");
            return;
        }

        // Rastgele bir güneþ materyali seç
        Material secilenGunesMateryali = gunesMateryalListesi[Random.Range(0, gunesMateryalListesi.Length)];

        // Ana güneþi oluþtur
        GameObject anaGunes = Instantiate(guenesPrefab, Vector3.zero, Quaternion.identity);

        // Ana güneþin materyalini deðiþtir
        Renderer anaGunesRenderer = anaGunes.GetComponent<Renderer>();
        if (anaGunesRenderer != null)
        {
            anaGunesRenderer.material = secilenGunesMateryali;
        }
        else
        {
            Debug.LogError("Ana Güneþ üzerinde Renderer bileþeni bulunamadý!");
            return;
        }

        // Katmanlar halinde gezegenleri oluþtur
        for (int katman = 0; katman < katmanSayisi; katman++)
        {
            // Birinci katmanda gezegen oluþturma
            if (katman > 0)
            {
                for (int gezegenIndex = 0; gezegenIndex < gezegenSayisi; gezegenIndex++)
                {
                    // Gezegeni rastgele bir konumda oluþtur
                    float aci = Random.Range(0f, 360f);
                    float radyanAci = Mathf.Deg2Rad * aci;
                    float x = Mathf.Cos(radyanAci) * (katmanArasiUzaklik * katman);
                    float z = Mathf.Sin(radyanAci) * (katmanArasiUzaklik * katman);

                    Vector3 gezegenKonumu = new Vector3(x, 0f, z);

                    // Rastgele bir gezegen materyali seç
                    Material rastgeleGezegenMateryal = hazirGezegenMalzemeler[Random.Range(0, hazirGezegenMalzemeler.Length)];

                    // Gezegeni oluþtur ve ana güneþin altýnda yerleþtir
                    GameObject yeniGezegen = Instantiate(gezegenPrefab, gezegenKonumu, Quaternion.identity);
                    yeniGezegen.transform.SetParent(anaGunes.transform);

                    // Gezegenin materyalini deðiþtir
                    Renderer yeniGezegenRenderer = yeniGezegen.GetComponent<Renderer>();
                    if (yeniGezegenRenderer != null)
                    {
                        yeniGezegenRenderer.material = rastgeleGezegenMateryal;
                    }
                    else
                    {
                        Debug.LogError("Gezegen üzerinde Renderer bileþeni bulunamadý!");
                    }

                    // Gezegen üzerine týklama algýlama
                    GezegenDetayScript gezegenDetayScript = yeniGezegen.AddComponent<GezegenDetayScript>();
                    gezegenDetayScript.detayModalPrefab = detayModalPrefab; // Detay modal prefab'ýný atayýn
                }


            }
        }
    }

    public class GezegenDetayScript : MonoBehaviour
    {
        public GameObject detayModalPrefab;

        void OnMouseDown()
        {
            // Gezegene týklandýðýnda detay modalý aç
            if (detayModalPrefab != null)
            {
                Instantiate(detayModalPrefab);
            }
            else
            {
                Debug.LogError("Detay modal prefab'ý belirtilmemiþ!");
            }
        }
    }
}
