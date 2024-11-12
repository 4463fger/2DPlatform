using UnityEngine;

namespace Skill
{
    public class Clone_Skill : Skill
    {
        [Header("Clone info")]
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;
        [Space] 
        [SerializeField] private bool canAttack;
        
        public void CreateClone(Transform _closePosition,Vector3 _offset)
        {
            GameObject newClone = Instantiate(clonePrefab);
            
            newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_closePosition,cloneDuration,canAttack,_offset);
        }
    }
}
