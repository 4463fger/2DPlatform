using System;
using UnityEngine;

namespace Skill
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager instance;

        public Dash_Skill dash { get; private set; }
        public Clone_Skill clone { get; private set; }
        public Sword_Skill sword { get; private set; }
        public BlackHole_Skill blackHole { get; private set; }

        private void Awake()
        {
            if (instance != null)
                Destroy(instance.gameObject);
            else
                instance = this;
        }

        private void Start()
        {
            dash = GetComponent<Dash_Skill>();
            clone = GetComponent<Clone_Skill>();
            sword = GetComponent<Sword_Skill>();
            blackHole = GetComponent<BlackHole_Skill>();
        }
    }
}