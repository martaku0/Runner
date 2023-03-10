using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Transform _player;
    private GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        _gm = GameManager.instance;

        if (!_gm.magnetActive) return;

        if (Vector3.Distance(transform.position, _player.position) < _gm.magnetDistance)
        {
            var direction = (_player.position - transform.position).normalized;
            transform.position += direction * _gm.magnetSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
