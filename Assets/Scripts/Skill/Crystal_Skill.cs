/*
* ┌──────────────────────────────────┐
* │  描    述:                       
* │  类    名: Crystal_Skill.cs       
* │  创 建 人:                       
* └──────────────────────────────────┘
*/

using UnityEngine;

namespace Skill
{
    public class Crystal_Skill : Skill
    {
        [SerializeField] private float crystalDuration;
        [SerializeField] private GameObject crystalPrefab;
        private GameObject currentCrystal;

        public override void UseSkill()
        {
            base.UseSkill();

            if (currentCrystal == null)
            {
                currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
                Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();
                
                currentCrystalScript.SetUpCrystal(crystalDuration);
            }
            else
            {
                player.transform.position = currentCrystal.transform.position;
                Destroy(currentCrystal);
            }
        }
    }
}