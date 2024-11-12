using EnemyCharacter;
using Skill;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player Player => GetComponentInParent<Player>();
        private void AnimationTrigger()
        {
            Player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Player.attackCheck.position, Player.attackCheckRadius);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                    hit.GetComponent<Enemy>().Damage();
            }
        }

        private void ThrowSword()
        {
            SkillManager.instance.sword.CreateSword();
        }
    }
}