using PlayerCharacter;
using UnityEngine;

namespace EnemyCharacter.Skeleton
{
    public class Enemy_SkeletonAnimationTriggers : MonoBehaviour
    {
        private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

        private void AnimationTrigger()
        {
            enemy.AnimationFinishTrigger();
        }

        private void AttackTrigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

            foreach (var VARIABLE in colliders)
            {
                if (VARIABLE.GetComponent<Player>() != null) 
                    VARIABLE.GetComponent<Player>().Damage();
            }
        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    }
}