using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerBlack : BaseBlack
{
    public override bool calcularMovimientos(Casilla casillaIni, Casilla CasillaDese)
    {
        Debug.Log("Calculando movimientos torre blanca");
        // Registra los posibles movimientos en una lista, luego comprueba que el movimiento
        // deseado se encuentra en la lista
        List<Casilla> posibMovimientos = new List<Casilla>();

        // Comprueba casilla en eje X positivo
        Debug.Log("Comprobando eje X positivo");
        for (int i = 1; i <= 7 ; i++)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Debug.Log("Casilla: " + posibMovX + " - " + posibMovY);
            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                Debug.Log("Casilla vacia");
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player == Player.White && posibCasilla == CasillaDese)
            {
                Debug.Log("Casilla ocupada por pieza blanca, se puede comer");
                Destroy(posibCasilla.OccupiedPiece.gameObject);

                return true;
            }
            else break;
        }

        // Comprueba casilla en eje X negativo
        Debug.Log("Comprobando eje X negativo");
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY < 0) break;

            Debug.Log("Casilla: " + posibMovX + " - " + posibMovY);
            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player == Player.White && posibCasilla == CasillaDese)
            {
                Debug.Log("Casilla ocupada por pieza blanca, se puede comer");
                Destroy(posibCasilla.OccupiedPiece.gameObject);

                return true;
            }
            else break;
        }

        // Comprueba casilla en eje Y positivo
        Debug.Log("Comprobando eje Y positivo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX > 7 || posibMovY > 7) break;

            Debug.Log("Casilla: " + posibMovX + " - " + posibMovY);
            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player == Player.White && posibCasilla == CasillaDese)
            {
                Debug.Log("Casilla ocupada por pieza blanca, se puede comer");
                Destroy(posibCasilla.OccupiedPiece.gameObject);

                return true;
            }
            else break;
        }

        // Comprueba casilla en eje Y negativo
        Debug.Log("Comprobando eje Y negativo");
        for (int i = -1; i >= -7; i--)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY();

            if (posibMovX < 0 || posibMovY < 0) break;

            Debug.Log("Casilla: " + posibMovX + " - " + posibMovY);
            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player == Player.White && posibCasilla == CasillaDese)
            {
                Debug.Log("Casilla ocupada por pieza blanca, se puede comer");
                Destroy(posibCasilla.OccupiedPiece.gameObject);

                return true;
            }
            else break;
        }

        if (posibMovimientos.Contains(CasillaDese))
        {
            return true;
        }

        return false;
    }
}
