using UnityEngine;

public partial class PlayerController
{
    [System.Serializable]
    public class Settings : JSon
    {
        [Range(0, 1)]
        public float
            slide_default = .25f, slide_ice = .3f, slide_air = .1f, 
            slope_range_min = .5f, slope_range_max = .7f;

        [Min(0)]
        public float
            move_speed = 10, side_speed = 10, 
            jump1_force = 20, jump2_force = 10, 
            fly_speed = 1, fly_force = .1f, grav_force = 55, 
            shoot_force = 15, shoot_lifetime_min = 10, shoot_lifetime_max = 15;
    }
}