using System.Collections.Generic;
using UnityEngine;

public class PawnBlack : BaseBlack
{
    private bool isFirstMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override bool calcularMovimientos(Casilla casillaIni, Casilla CasillaDese)
    {
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
            return true;
        }

        return false;
    }
}
