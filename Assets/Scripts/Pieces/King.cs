using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class King : BasePiece
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    Debug.Log("Casilla vacia");
                    if (!detectCheck(posibCasilla)) posibMovimientos.Add(posibCasilla);
                }
                else if (posibCasilla.OccupiedPiece.player != player && posibCasilla == CasillaDese)
                {
                    Debug.Log("Casilla ocupada por pieza negra, se puede comer");

                    if (!detectCheck(posibCasilla))
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    Debug.Log("Casilla vacia");
                    if(!detectCheck(posibCasilla)) posibMovimientos.Add(posibCasilla);
                }
                else if (posibCasilla.OccupiedPiece.player != player && posibCasilla == CasillaDese)
                {
                    Debug.Log("Casilla ocupada por pieza negra, se puede comer");

                    if(!detectCheck(posibCasilla))
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
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
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null)
                {
                    Debug.Log("Casilla vacia");
                    if (!detectCheck(posibCasilla)) posibMovimientos.Add(posibCasilla);
                }
                else if (posibCasilla.OccupiedPiece.player != player && posibCasilla == CasillaDese)
                {
                    Debug.Log("Casilla ocupada por pieza negra, se puede comer");

                    if (!detectCheck(posibCasilla))
                    {
                        Destroy(posibCasilla.OccupiedPiece.gameObject);
                        return true;
                    }
                }
            }
        }

        if (posibMovimientos.Contains(CasillaDese))
        {
            return true;
        }

        return false;
    }

    public void isInCheck()
    {
        inCheck = detectCheck(null);

        if (inCheck)
        {
            bool kingCanMove = false;
            bool enemyCanBeCaptured = false;
            bool pathCanBeBlocked = false;

            Debug.Log($"Rey {player} en jaque");
            kingCanMove = canMove();

            if(dangerPieces.Count == 1)
            {
                BasePiece enemyPiece = dangerPieces.First();

                if(enemyPiece.detectCheck(null))
                {
                    Debug.Log("La pieza puede ser capturada");
                    enemyCanBeCaptured = true;
                }
                else Debug.Log("La pieza no puede ser capturada");

                Debug.Log("Comprobando si el camino puede ser bloqueado");
                foreach (Casilla casilla in dangerPath)
                {
                    if (enemyPiece.detectCheck(casilla))
                    {
                        Debug.Log("El camino puede ser bloqueado");
                        pathCanBeBlocked = true;
                        break;
                    }
                }
            }
            else Debug.Log("Más de una pieza amenaza al rey");

            if ((!kingCanMove) && (!enemyCanBeCaptured) && (!pathCanBeBlocked))
            {
                Debug.Log($"Rey {player} en jaque mate");
            }
            else
            {
                Debug.Log($"Rey {player} en jaque, pero puede escapar o ser defendido");
            }
        }
    }

    public bool canMove()
    {
        Debug.Log("Se puede mover el rey?");
        bool inCheck = false;
        Casilla casillaIni = OccupiedCasilla;

        for (int i = -1; i < 2; i++)
        {
            int posibMovX = casillaIni.getPosX() + 1;
            int posibMovY = casillaIni.getPosY() + i;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null || posibCasilla.OccupiedPiece.player != player)
                {
                    inCheck = detectCheck(posibCasilla);

                    if(!inCheck)
                    {
                        Debug.Log("Salida viable en: " + posibMovX + " - " + posibMovY);
                        return true;
                    }
                }
            }
        }

        // Comprueba casilla laterales
        for (int i = 0; i < 2; i++)
        {
            int leteral = i == 0 ? 1 : -1;

            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + leteral;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null || posibCasilla.OccupiedPiece.player != player)
                {
                    inCheck = detectCheck(posibCasilla);

                    if (!inCheck)
                    {
                        Debug.Log("Salida viable en: " + posibMovX + " - " + posibMovY);
                        return true;
                    }
                }
            }
        }

        // Comprueba casilla en eje X negativo
        for (int i = -1; i < 2; i++)
        {
            int posibMovX = casillaIni.getPosX() - 1;
            int posibMovY = casillaIni.getPosY() + i;

            if (!(posibMovX > 7 || posibMovY > 7 || posibMovX < 0 || posibMovY < 0))
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
                if (posibCasilla.OccupiedPiece == null || posibCasilla.OccupiedPiece.player != player)
                {
                    inCheck = detectCheck(posibCasilla);

                    if (!inCheck)
                    {
                        Debug.Log("Salida viable en: " + posibMovX + " - " + posibMovY);
                        return true;
                    }
                }
            }
        }

        Debug.Log("No hay salidas viables para el rey");
        return false;
    }
}
