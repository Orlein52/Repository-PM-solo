

public class Grenade : Weapon
{
    public void changeFiremode()
    {
        if (fireModes >= 3)
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
            if (currentFireMode == 1)
            {
                holdToAttack = true;
                rof = 0.5f;
                scattershot = false;
                arc = false;
            }
            if (currentFireMode == 2)
            {
                scattershot = true;
                arc = false;
            }
        }


    }

}
