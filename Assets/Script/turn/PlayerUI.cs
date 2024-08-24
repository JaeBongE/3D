using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject objKnight;
    [SerializeField] private Button attackButton;

    void Start()
    {
        Knight scKnight = objKnight.GetComponent<Knight>();

        attackButton.onClick.AddListener(() =>
        {
            scKnight.Attack(true);
        });
    }

    void Update()
    {
        
    }
}
