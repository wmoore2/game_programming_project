using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDriver : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _chassis;
    [SerializeField]
    private TMP_Text _optics;
    [SerializeField]
    private TMP_Text _jets;

    private PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            _chassis.text = $"Chassis: {player.getResource(PlayerStatus.rTypes.Chassis)}";
            _optics.text = $"Optics: {player.getResource(PlayerStatus.rTypes.Optics)}";
            _jets.text = $"Jets: {player.getResource(PlayerStatus.rTypes.Jets)}";
        }
    }

}
