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
    [SerializeField]
    private TMP_Text _heartHealth;
    [SerializeField]
    private TMP_Text _gameOver;

    private PlayerStatus player;
    private HeartStatus heartStatus;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        heartStatus = GameObject.FindGameObjectWithTag("Heart").GetComponent<HeartStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            _chassis.text = $"Chassis: {player.getResource(PlayerStatus.rTypes.Chassis)}";
            _optics.text = $"Optics: {player.getResource(PlayerStatus.rTypes.Optics)}";
            _jets.text = $"Jets: {player.getResource(PlayerStatus.rTypes.Jets)}";
            _heartHealth.text = $"Heart: {heartStatus.CurrentHealth: 0000}";
            _gameOver.enabled = heartStatus.CurrentHealth <= 0 || player.CurrentHealth <= 0;
        }
    }

}
