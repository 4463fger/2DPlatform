using UnityEngine;

namespace Skill
{
    public class BlackHole_Skill : Skill
    {
        [SerializeField] private int amountOfAttacks;
        [SerializeField] private float cloneCooldown;
        [SerializeField] private float blackHoleDuration;
        [Space]
        [SerializeField] private GameObject blackHolePrefab;
        [SerializeField] private float maxSize;
        [SerializeField] private float growSpeed;
        [SerializeField] private float shrinkSpeed;

        private BlackHole_Skill_Controller currentBlackHole;
        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void UseSkill()
        {
            base.UseSkill();
            
            GameObject newBlackHole = Instantiate(blackHolePrefab,player.transform.position,Quaternion.identity);
            currentBlackHole = newBlackHole.GetComponent<BlackHole_Skill_Controller>();
            currentBlackHole.SetupBlackHole(maxSize,growSpeed,shrinkSpeed,amountOfAttacks,cloneCooldown,blackHoleDuration);
        }
        
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        public bool SkillCompleted()
        {
            if (!currentBlackHole)
                return false;
            if (currentBlackHole.playerCanExitState)
            {
                currentBlackHole = null;
                return true;
            }
            return false;
        }
    }
}