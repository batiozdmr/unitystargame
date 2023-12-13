using UnityEngine;

public class FareKontrolu : MonoBehaviour
{
    public float hassasiyet = 2.0f;
    public float minimumY = -90f;
    public float maximumY = 90f;
    public float zoomHassasiyet = 2.0f;
    public float minZoom = 1f;
    public float maxZoom = 10f;

    private float yatayHareket;
    private float dikeyHareket;
    private bool solTikBasili = false;

    void Update()
    {
        // Sol týk kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            solTikBasili = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            solTikBasili = false;
        }

        // Fare sürükleme hareketi kontrolü
        if (solTikBasili)
        {
            yatayHareket += hassasiyet * Input.GetAxis("Mouse X");
            dikeyHareket -= hassasiyet * Input.GetAxis("Mouse Y");
            dikeyHareket = Mathf.Clamp(dikeyHareket, minimumY, maximumY);

            transform.eulerAngles = new Vector3(dikeyHareket, yatayHareket, 0);
        }

        // Fare tekerleði kontrolü (Zoom)
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomHassasiyet;
        transform.Translate(Vector3.forward * zoom);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, minZoom, maxZoom));
    }
}
