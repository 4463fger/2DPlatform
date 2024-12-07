/*
* ┌──────────────────────────────────┐
* │  描    述:                       
* │  类    名: Crystal_Skill_Controller.cs       
* │  创 建 人:                       
* └──────────────────────────────────┘
*/

using System;
using UnityEngine;

namespace Skill
{
    public class Crystal_Skill_Controller : MonoBehaviour
    {
        private float crystalExistTimer;

        public void SetUpCrystal(float _crystalDuration)
        {
            crystalExistTimer = _crystalDuration;
        }

        private void Update()
        {
            crystalExistTimer -= Time.deltaTime;
            if (crystalExistTimer < 0)
            {
                SelfDestroy();
            }
        }
        
        public void SelfDestroy() => Destroy(gameObject);
    }
}