using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Foster
{
    public class Zone : SlipstreamJumper.Zone
    {
        new public static ZoneInfo info = new ZoneInfo()
        {

            zoneName = "",
            creator = "Emily Foster",
            sceneFile = "ZoneFoster"

        };

        //singleton;
        public static Zone main;

        public AABB player;


        private List<AABB> platforms = new List<AABB>();

        public List<AABB> powerups = new List<AABB>();


        private void Awake()
        {
            if (main != null)
            {
                Destroy(gameObject);

            }
            else
            {
                main = this;
            }
        }

        private void OnDestroy()
        {
            if (main == this) main = null;
        }


        void LateUpdate()
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();

            foreach (AABB box in platforms)
            {
                if (player.OverlapCheck(box))
                {
                    pm.ApplyFix(player.FindFix(box));
                }
            }
            foreach (AABB power in powerups)
            {
                if(player.OverlapCheck(power))
                {

                    SpringBlock sb = power.GetComponent<SpringBlock>();
                    if(sb)
                    {
                        sb.PlayerHit(pm);
                    }

                }
            }
        }

        public void AddPlateform(AABB platform)
        {
            platforms.Add(platform);


        }

        public void RemovingPlatform(AABB platform)
        {
            platforms.Remove(platform);
        }
    }
}