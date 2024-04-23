using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    public Image fullAuto;
    public Image speedBoost;
    public Image homing;
    public Image bulletSpeed;
    public PlayerController playerController;
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {       
        fullAuto.enabled = false;
        speedBoost.enabled = false;
        homing.enabled = false;
        bulletSpeed.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.fullAuto) { fullAuto.enabled = true;  }
        else { fullAuto.enabled = false;  }

        if (playerController.moveSpeed == 10) { speedBoost.enabled = true;  }
        else { speedBoost.enabled = false;  }

        if (playerController.homing) { homing.enabled = true;  }
        else { homing.enabled = false;  }

        if (weapon.fireForce > 10) { bulletSpeed.enabled = true; }
        else { bulletSpeed.enabled = false;  }
    }
}
