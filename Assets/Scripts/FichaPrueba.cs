using UnityEngine;

public class FichaPrueba : MonoBehaviour
{
public GameObject testWhite;
public GameObject testBlack;

    //en el centro del tablero
    void Start()
    {
        Vector3 posicionWhite = new Vector3(0, -3, 0);
        Vector3 posicionBlack = new Vector3(0, 3, 0);
        Instantiate(testWhite, posicionWhite, Quaternion.identity);
        Instantiate(testBlack, posicionBlack, Quaternion.identity);
    }
}