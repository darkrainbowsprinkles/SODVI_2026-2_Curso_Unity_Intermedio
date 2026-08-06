using FPS.Combat;
using TMPro;
using UnityEngine;

namespace FPS.UI
{
    public class AggroGroupUI : MonoBehaviour
    {
        [SerializeField] TMP_Text enemyCountText;
        AggroGroup aggroGroup;

        void Awake()
        {
            aggroGroup = FindAnyObjectByType<AggroGroup>();
        }

        void Start()
        {
            RefreshUI();
        }

        void OnEnable()
        {
            aggroGroup.OnChange += RefreshUI;
        }

        void OnDisable()
        {
            aggroGroup.OnChange -= RefreshUI;
        }

        void RefreshUI()
        {
            int totalCount = aggroGroup.GetTotalCount();
            int aliveCount = aggroGroup.GetAliveCount();
            enemyCountText.text = $"{aliveCount}/{totalCount}";
        }
    }
}