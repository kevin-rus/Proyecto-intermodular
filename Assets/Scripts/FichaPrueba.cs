using UnityEngine;

public class FichaPrueba : MonoBehaviour
{
public GameObject fichaPrefab;

//en el centro del tablero
void Start()
{
Vector3 posicion = new Vector3(0, 0, 0);
Instantiate(fichaPrefab, posicion, Quaternion.identity);
}
}