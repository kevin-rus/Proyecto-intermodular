using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bishop : BasePiece
{
    public override bool calcularMovimientos(Casilla casillaIni, Casilla CasillaDese)
    {
        // Obtiene el rey del jugador para comprobar los movimientos cuando está en jaque
        King myKing = player == Player.White ? PieceManager.instance.GetWhiteKing() : PieceManager.instance.GetBlackKing();

        Debug.Log("Calculando movimientos alfil");
        // Registra los posibles movimientos en una lista, luego comprueba que el movimiento
        // deseado se encuentra en la lista
        List<Casilla> posibMovimientos = new List<Casilla>();

        // Comprueba casilla en eje X positivo - eje Y positivo
        Debug.Log("Comprobando eje X positivo - eje Y positivo");
        for (int i = 1; i <= 7 ; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                Debug.Log("Casilla vacia");
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player && posibCasilla == CasillaDese)
            {
                if (!myKing.inCheck)
                {
                    Destroy(posibCasilla.OccupiedPiece.gameObject);
                    return true;
                }
                else
                {
                    // Si el rey está en jaque, el peón solo puede comer la pieza que lo amenaza
                    if (myKing.dangerPieces.Count == 1 && myKing.dangerPieces.Contains(posibCasilla.OccupiedPiece))
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
                }
            }
            else break;
        }

        // Comprueba casilla en eje X positivo - eje Y negativo
        Debug.Log("Comprobando eje X positvo - eje Y negativo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + i;
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX > 7 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player && posibCasilla == CasillaDese)
            {
                if (!myKing.inCheck)
                {
                    Destroy(posibCasilla.OccupiedPiece.gameObject);
                    return true;
                }
                else
                {
                    // Si el rey está en jaque, el peón solo puede comer la pieza que lo amenaza
                    if (myKing.dangerPieces.Count == 1 && myKing.dangerPieces.Contains(posibCasilla.OccupiedPiece))
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
                }
            }
            else break;
        }

        // Comprueba casilla en eje X negativo - eje Y negativo
        Debug.Log("Comprobando eje X negativo - eje Y negativo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + (i * -1);

            if (posibMovX < 0 || posibMovY < 0) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player && posibCasilla == CasillaDese)
            {
                if (!myKing.inCheck)
                {
                    Destroy(posibCasilla.OccupiedPiece.gameObject);
                    return true;
                }
                else
                {
                    // Si el rey está en jaque, el peón solo puede comer la pieza que lo amenaza
                    if (myKing.dangerPieces.Count == 1 && myKing.dangerPieces.Contains(posibCasilla.OccupiedPiece))
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
                }
            }
            else break;
        }

        // Comprueba casilla en eje X negativo - eje Y positivo
        Debug.Log("Comprobando eje X negativo - eje Y positivo");
        for (int i = 1; i <= 7; i++)
        {
            int posibMovX = casillaIni.getPosX() + (i * -1);
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX < 0 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else if (posibCasilla.OccupiedPiece.player != player && posibCasilla == CasillaDese)
            {
                if (!myKing.inCheck)
                {
                    Destroy(posibCasilla.OccupiedPiece.gameObject);
                    return true;
                }
                else
                {
                    // Si el rey está en jaque, el peón solo puede comer la pieza que lo amenaza
                    if (myKing.dangerPieces.Count == 1 && myKing.dangerPieces.Contains(posibCasilla.OccupiedPiece))
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
                }
            }
            else break;
        }

        if (posibMovimientos.Contains(CasillaDese))
        {
            if (myKing.inCheck)
            {
                List<Casilla> dangerPath = myKing.dangerPath;
                // Si el rey está en jaque, el peón solo puede moverse a una casilla que bloquee el jaque
                if (myKing.dangerPieces.Count == 1 && dangerPath.Contains(CasillaDese))
                {
                    return true;
                }
            }
            else if (myKing.protectingPieces.Contains(this))
            {
                if(dangerPath.Contains(CasillaDese))
                {
                    Debug.Log("La pieza está protegiendo al rey y el movimiento está en el camino de peligro.");
                    return true;
                }
                else return false;
            }
            else return true;
        }

        return false;
    }
}
