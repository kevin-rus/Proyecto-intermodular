using System.Collections.Generic;
using UnityEngine;

public class PawnBlack : BaseBlack
{
    private bool isFirstMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override bool calcularMovimientos(Casilla casillaIni, Casilla CasillaDese)
    {
        if (CasillaDese.OccupiedPiece != null && CasillaDese.OccupiedPiece.player == Player.White)
        {
            int posibMovY = casillaIni.getPosY() - 1;

            int leftCorner = casillaIni.getPosX() - 1;
            int rightCorner = casillaIni.getPosX() + 1;

            if (leftCorner >= 0)
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(leftCorner, posibMovY));
                if (posibCasilla == CasillaDese)
                {
                    Destroy(posibCasilla.OccupiedPiece.gameObject);
                    return true;
                }
            }
            if (rightCorner <= 7)
            {
                Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(rightCorner, posibMovY));
                if (posibCasilla == CasillaDese)
                {
                    Destroy(posibCasilla.OccupiedPiece.gameObject);
                    return true;
                }
            }
        }

        List<Casilla> posibMovimientos = new List<Casilla>();

        for (int i = -1; i >= (isFirstMove ? -2 : -1); i--)
        {
            int posibMovX = casillaIni.getPosX();
            int posibMovY = casillaIni.getPosY() + i;

            if (posibMovX > 7 || posibMovY > 7) break;

            Casilla posibCasilla = TableroManager.instance.GetCaillaFromPosition(new Vector2(posibMovX, posibMovY));
            if (posibCasilla.OccupiedPiece == null)
            {
                posibMovimientos.Add(posibCasilla);
            }
            else break;
        }

        if (posibMovimientos.Contains(CasillaDese))
        {
            isFirstMove = false;
            return true;
        }

        return false;
    }
}
