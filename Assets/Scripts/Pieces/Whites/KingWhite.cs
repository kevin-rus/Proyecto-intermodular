using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KingWhite : BaseWhite
{
    public override bool calcularMovimientos(Casilla casillaIni, Casilla CasillaDese)
    {
        Debug.Log("Calculando movimientos rey blanco");
        // Registra los posibles movimientos en una lista, luego comprueba que el movimiento
        // deseado se encuentra en la lista
        List<Casilla> posibMovimientos = new List<Casilla>();

        // Comprueba casilla en eje X positivo
        Debug.Log("Comprobando eje X positivo");
        for (int i = -1; i < 2; i++)
        {
            int posibMovX = casillaIni.getPosX() + 1;
            int posibMovY = casillaIni.getPosY() + i;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Debug.Log("Casilla: " + posibMovX + " - " + posibMovY);
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    Debug.Log("Casilla vacia");
                    posibMovimientos.Add(posibCasilla);
                }
            }
        }

        // Comprueba casilla laterales
        Debug.Log("Comprobando eje X negativo");
        for (int i = 0; i < 2; i++)
        {
            int leteral = i == 0 ? 1 : -1;

            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + leteral;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Debug.Log("Casilla: " + posibMovX + " - " + posibMovY);
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    Debug.Log("Casilla vacia");
                    posibMovimientos.Add(posibCasilla);
                }
            }
        }

        // Comprueba casilla en eje X negativo
        Debug.Log("Comprobando eje X negativo");
        for (int i = -1; i < 2; i++)
        {
            int posibMovX = casillaIni.getPosX() - 1;
            int posibMovY = casillaIni.getPosY() + i;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Debug.Log("Casilla: " + posibMovX + " - " + posibMovY);
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    Debug.Log("Casilla vacia");
                    posibMovimientos.Add(posibCasilla);
                }
            }
        }

        if (posibMovimientos.Contains(CasillaDese))
        {
            return true;
        }

        return false;
    }
}
