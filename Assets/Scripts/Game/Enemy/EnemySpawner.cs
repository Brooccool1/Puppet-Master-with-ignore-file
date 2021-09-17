using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _target;

    private int _level = 1;
    private List<GameObject> _aliveEnemies = new List<GameObject>();

    private int _enemiesToSpawn;
    private float _spawnDelay = 0;

    void Start()
    {
        _enemiesToSpawn = 1;
    }

    private void _aliveCheck()
    {
        for (int i = 0; i < _aliveEnemies.Count; i++)
        {
            if(_aliveEnemies[i] == null)
            {
                _aliveEnemies.RemoveAt(i);
            }
        }
        if (_aliveEnemies.Count == 0 && _enemiesToSpawn == 0)
        {
            _level++;
            if (_level % 5 == 0)
            {
                _enemiesToSpawn = _level * 2;
            }
            else
            {
                _enemiesToSpawn = _level;
            }
        }
    }

    private void _spawnEnemies()
    {
        if (_spawnDelay <= 0)
        {
            if (_enemiesToSpawn > 0)
            {
                if(_level % 10 == 0)
                {
                    _spawnBoss();
                }
                else if (_level % 5 == 0)
                {
                    _spawnHorde();
                }
                else
                {
                    _spawnEnemy();
                }
            }
        }
    }

    private void _spawnHorde()
    {
        GameObject scaledEnemy = new GameObject();
        scaledEnemy.transform.localScale -= new Vector3(0.3f, 0.3f, 0);
        scaledEnemy.transform.position = transform.position;
        scaledEnemy.transform.rotation = Quaternion.identity;

        _aliveEnemies.Add(_enemyPrefab);
        _aliveEnemies[_aliveEnemies.Count - 1] = Instantiate(_enemyPrefab, scaledEnemy.transform)/*?.GetComponent<EnemyController>()._target = _target*/;
        _aliveEnemies[_aliveEnemies.Count - 1].GetComponent<EnemyController>()._target = _target;
        _enemiesToSpawn--;
        _spawnDelay = 2;

        scaledEnemy.transform.DetachChildren();
        Destroy(scaledEnemy);
    }

    private void _spawnBoss()
    {
        GameObject scaledEnemy = new GameObject();
        scaledEnemy.transform.localScale += new Vector3(0.5f, 0.5f, 0);
        scaledEnemy.transform.position = transform.position + new Vector3(5, 4, 0);
        scaledEnemy.transform.rotation = Quaternion.identity;

        _aliveEnemies.Add(_enemyPrefab);
        _aliveEnemies[_aliveEnemies.Count - 1] = Instantiate(_enemyPrefab, scaledEnemy.transform)/*?.GetComponent<EnemyController>()._target = _target*/;
        _aliveEnemies[_aliveEnemies.Count - 1].GetComponent<EnemyController>()._target = _target;
        _enemiesToSpawn -= 10;
        _spawnDelay = 2;

        scaledEnemy.transform.DetachChildren();
        Destroy(scaledEnemy);
    }

    private void _spawnEnemy()
    {
        _aliveEnemies.Add(_enemyPrefab);
        _aliveEnemies[_aliveEnemies.Count - 1] = Instantiate(_enemyPrefab, transform.position, Quaternion.identity)/*?.GetComponent<EnemyController>()._target = _target*/;
        _aliveEnemies[_aliveEnemies.Count - 1].GetComponent<EnemyController>()._target = _target;
        _enemiesToSpawn--;
        _spawnDelay = 2;
    }

    private void _timer()
    {
        _spawnDelay -= Time.deltaTime;
    }

    void Update()
    {
        _timer();
        _spawnEnemies();
        _aliveCheck();

        Level.level = _level;
    }
}
