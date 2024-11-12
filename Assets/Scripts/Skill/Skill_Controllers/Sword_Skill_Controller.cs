﻿using System.Collections.Generic;
using EnemyCharacter;
using PlayerCharacter;
using UnityEngine;

namespace Skill
{
    public class Sword_Skill_Controller : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Player player;

        private bool canRotate = true;
        private bool isReturning;

        private float freezeTimeDuration;
        private float returnSpeed = 12;
        
        [Header("Pierce info")] 
        private float pierceAmount;
        
        [Header("Bounce info")]
        private float bounceSpeed = 10;
        private bool isBouncing;
        private int bounceAmount;
        private List<Transform> enemyTarget;
        private int targetIndex;

        [Header("Spin info")] 
        private float maxTravelDistance;
        private float spinDuration;
        private float spinTimer;
        private bool wasStoppped;
        private bool isSpinning;

        private float hitTimer;
        private float hitCooldown;
        
        private float spinDirection;
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }
        public void SetupSword(Vector2 finalDir,float gravity,Player _player,float _freezeTimeDuration,float _returnSpeed)
        {
            this.player = _player;
            this.freezeTimeDuration = _freezeTimeDuration;
            returnSpeed = _returnSpeed;
            this.rb.velocity = finalDir;
            this.rb.gravityScale = gravity;

            if (pierceAmount <= 0)
                anim.SetBool("Rotation" , true);

            spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);
            
            Invoke("DestroyMe" , 7);
        }

        public void SetupBounce(bool _isBouncing,int _amountOfBounces,float _bounceSpeed)
        {
            isBouncing = _isBouncing;
            bounceAmount = _amountOfBounces;
            bounceSpeed = _bounceSpeed;
            
            enemyTarget = new List<Transform>();
        }

        public void SetupPierce(int _pierceAmount)
        {
            pierceAmount = _pierceAmount;
        }

        public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration,float _hitCooldown)
        {
            isSpinning = _isSpinning;
            maxTravelDistance = _maxTravelDistance;
            spinDuration = _spinDuration;
            hitCooldown = _hitCooldown;
        }
        public void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.isKinematic = false;
            transform.parent = null;
            isReturning = true;
        }

        private void Update()
        {
            if (canRotate)
                transform.right = rb.velocity;

            if (isReturning)
            {
                anim.SetBool("Rotation" , true);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    returnSpeed * Time.deltaTime);
                
                if (Vector2.Distance(transform.position,player.transform.position) < 1)
                    player.CatchTheSword();
            }
            BounceLogic();
            SpinLogic();
        }

        private void SpinLogic()
        {
            if (isSpinning)
            {
                if (Vector2.Distance(player.transform.position,transform.position) > maxTravelDistance &&!wasStoppped)
                {
                    StopWhenSpinning();
                }

                if (wasStoppped)
                {
                    spinTimer -= Time.deltaTime;

                    transform.position = Vector2.MoveTowards(transform.position,
                        new Vector2(transform.position.x * spinDirection, transform.position.y), 1.5f * Time.deltaTime);
                    if (spinTimer < 0)
                    {
                        isReturning = true;
                        isSpinning = false;
                    }
                    
                    hitTimer -= Time.deltaTime;
                    if (hitTimer < 0)
                    {
                        hitTimer = hitCooldown;

                        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                        foreach (var hit in colliders)
                        {
                            if (hit.GetComponent<Enemy>() != null)
                            {
                                SwordSkillDamage(hit.GetComponent<Enemy>());
                            }
                        }
                    }
                }
            }
        }

        private void StopWhenSpinning()
        {
            wasStoppped = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = spinDuration;
        }

        private void BounceLogic()
        {
            if (isBouncing && enemyTarget.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position,
                    bounceSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position,enemyTarget[targetIndex].position) < .1f)
                {
                    SwordSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());
                    targetIndex++;
                    bounceAmount--;

                    if (bounceAmount <= 0)
                    {
                        isBouncing = false;
                        isReturning = true;
                    }
                    if (targetIndex >= enemyTarget.Count)
                        targetIndex = 0;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isReturning)
                return;

            if (other.GetComponent<Player>() != null)
            {
                Enemy enemy =other.GetComponent<Enemy>();
                SwordSkillDamage(enemy);
            }
            //搜索半径为10的圆形范围内的所有敌人，并添加到目标中
            SetupTargetForBounce();
            StuckInto(other);
        }

        private void SwordSkillDamage(Enemy enemy)
        {
            enemy.Damage();
            enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
        }

        private void SetupTargetForBounce()
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }
        }

        private void StuckInto(Collider2D collision)
        {
            if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
            {
                pierceAmount--;
                return;
            }

            if (isSpinning)
            {
                StopWhenSpinning();
                return;                
            }
            
            canRotate = false;
            cd.enabled = false;
            
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (isBouncing && enemyTarget.Count > 0)
                return;
            
            anim.SetBool("Rotation" , false);
            transform.parent = collision.transform;
        }
    }
}