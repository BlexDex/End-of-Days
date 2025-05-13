using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DenisAlipov.GameTime
{
    public class Coop : MonoBehaviour
    {

        private int lastActionHour;
        private int harvestHour = 8;
        private int stopHarvestHour = 9;

        public bool hasRun = false;
        // Start is called before the first frame update
        void Start()
        {
            GameTime.AddOnTimeChanged(AddFood);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void AddFood(GameDayTime newTime)
        {
            int newTimeHours = newTime.Hours;
            if (lastActionHour == newTimeHours)
            {
                hasRun = false;
                return;
            }

            if (newTimeHours == harvestHour)
            {
                if(!hasRun)
                {
                    ResourceManager.Instance.IncreaseResource(ResourceManager.ResourceType.Food, 25);
                }
                hasRun = true;
                return;
            }
            if (newTimeHours == stopHarvestHour)
            {
                lastActionHour = newTimeHours;
                hasRun = false;
                return;
            }
        }
    }
}
