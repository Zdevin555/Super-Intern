
using UnityEngine;

  public static class InputManager
{
     public static bool Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

     public static bool Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }
        return false;
    }

     public static bool TakeShot()
    {
        if (Input.GetMouseButtonUp(0))
        {
            return true;
        }
        return false;
    }

     public static bool Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        return false;
    }

     public static bool Take()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        return false;
    }

     public static bool Elastic()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return true;
        }
        return false;
    }

     public static bool SuperRun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return true;
        }
        return false;
    }

     public static bool Throughwall()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return true;
        }
        return false;
    }

     public static bool ControlTime()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return true;
        }
        return false;
    }

    public static bool PutBox()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            return true;
        }
        return false;
    }


}
