

public class Grenade : Weapon
{
    public void changeFiremode()
    {
        if (fireModes >= 9)
        {
            currentFireMode++;

            if (currentFireMode >= fireModes)
                currentFireMode = 0;
            

            if (currentFireMode == 0)
            {
                holdToAttack = true;
                rof = 0.5f;
                scattershot = false;
                arc = true;
            }
            else if (currentFireMode == 3)
            {
                holdToAttack = true;
                rof = 0.5f;
                scattershot = false;
                arc = false;
            }
            else if (currentFireMode == 6)
            {
                scattershot = true;
                arc = false;
            }
        }


    }

}
