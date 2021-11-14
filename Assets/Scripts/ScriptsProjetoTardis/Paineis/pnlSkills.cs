using UnityEngine;
using UnityEngine.UI;

public class pnlSkills : MonoBehaviour
{
    public Button btnSkillEscudo, btnSkillTeletransport;

    void Start()
    {
        btnSkillEscudo.onClick.AddListener(() =>
        {
            SkillsManager.instancia.Skill_Escudo();
        });
        btnSkillTeletransport.onClick.AddListener(() =>
        {
            SkillsManager.instancia.SkillTeleport();
        });

    }

}
