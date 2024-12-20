﻿using System;
using System.Collections.Generic;
using EnemyCharacter;
using Mgr;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Skill
{
    public class BlackHole_Skill_Controller : MonoBehaviour
    {
        [SerializeField] private GameObject hotKeyPrefab;
        [SerializeField] private List<KeyCode> keyCodeList;
        
        private float maxSize;
        private float growSpeed;
        private float shrinkSpeed;
        private float balckHoleTimer;
        
        private bool canGrow = true;
        private bool canShrink;
        private bool canCreateHotKeys = true; 
        private bool cloneAttackReleased;
        private bool playerCanDisapear = true;
        
        private int amountOfAttacks = 4;
        private float cloneAttackCooldown = .3f;
        private float cloneAttackTimer;

        private List<Transform> targets = new List<Transform>();
        private List<GameObject> createdHotKey = new List<GameObject>();

        public bool playerCanExitState { get; private set; }

        public void SetupBlackHole(float _maxSize, float _growSpeed, float _shrinkSpeed,int _amountOfAttacks,float _cloneAttackCooldown,float _balckHoleTimer)
        {
            maxSize = _maxSize;
            growSpeed = _growSpeed;
            shrinkSpeed = _shrinkSpeed;
            amountOfAttacks = _amountOfAttacks;
            cloneAttackCooldown = _cloneAttackCooldown;
            balckHoleTimer = _balckHoleTimer;
        }
        private void Update()
        {
            cloneAttackTimer -= Time.deltaTime;
            balckHoleTimer -= Time.deltaTime;

            if (balckHoleTimer < 0)
            {
                balckHoleTimer = Mathf.Infinity;
                
                if (targets.Count > 0)
                    ReleaseCloneAttack();
                else
                    FinishBlackHoleAbility();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReleaseCloneAttack();
            }

            CloneAttackLogic();
            
            if (canGrow && !canShrink)
            {
                transform.localScale = Vector2.Lerp
                    (transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
            }

            if (canShrink)
            {
                transform.localScale =
                    Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
                
                if (transform.localScale.x < 0)
                    Destroy(gameObject);
            }
        }

        private void ReleaseCloneAttack()
        {
            if (targets.Count <= 0)
                return;
            
            DestroyHotKeys(); 
            cloneAttackReleased = true;
            canCreateHotKeys = false;

            if (playerCanDisapear)
            {
                playerCanDisapear = false;
                PlayerManager.instance.player.MakeTransparent(true);
            }
        }

        private void CloneAttackLogic()
        {
            if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfAttacks > 0)
            {
                cloneAttackTimer = cloneAttackCooldown;

                int randomIndex = Random.Range(0, targets.Count);
                float xOffset = Random.Range(0, 100) > 50 ? 2 : -2;
                SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset,0));
                amountOfAttacks--;
                if (amountOfAttacks <= 0)
                {
                    Invoke("FinishBlackHoleAbility", 1f);
                }
            }
        }

        private void FinishBlackHoleAbility()
        {
            DestroyHotKeys();
            playerCanExitState = true;
            canShrink = true;
            cloneAttackReleased = false;
        }

        private void DestroyHotKeys()
        {
            if (createdHotKey.Count < 0)
                return;
            
            foreach (var hotKey in createdHotKey)
                Destroy(hotKey);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Enemy>() != null)
            {
                other.GetComponent<Enemy>().FreezeTime(true);
                CreateHotKey(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<Enemy>() != null)
                other.GetComponent<Enemy>().FreezeTime(false);                
        }

        private void CreateHotKey(Collider2D other)
        {
            if (keyCodeList.Count == 0)
                return;
            if (!canCreateHotKeys)
                return;
            
            GameObject newHotKey = Instantiate(hotKeyPrefab, other.transform.position + new Vector3(0, 2), Quaternion.identity);
            createdHotKey.Add(newHotKey);
            
            KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
            keyCodeList.Remove(choosenKey);

            BlackHole_HotKey_Controller newHotKeyScript = newHotKey.GetComponent<BlackHole_HotKey_Controller>();
            newHotKeyScript.SetupHotKey(choosenKey,other.transform,this);
        }

        public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
    }
}