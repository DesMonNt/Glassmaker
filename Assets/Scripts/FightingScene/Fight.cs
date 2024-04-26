using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Effects;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Fight : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> squads;
    private List<Unit> _charComponents;
    private Queue<Unit> _readyFighters;
    private List<Unit> _enemyComponents;
    private List<Unit> _allUnits;
    private List<Unit> _deletedUnits;
    private bool _isPrepare;
    public bool endOfEnemyTurn;
    public bool endOfPlayerTurn;
    
    private int numberOfChar;
    
    void Awake()
    {
        _readyFighters = new Queue<Unit>();
        _charComponents = new();
        _enemyComponents = new();
        _allUnits = new();
        _deletedUnits = new();
    }

    private void Start()
    {
        var firstPos = new Vector3(-62, 30);
        var firstPosToEnemy = new Vector3(5, 30);
        
        SpawnFighters(firstPos, _charComponents, _allUnits, squads);
        SpawnFighters(firstPosToEnemy, _enemyComponents, _allUnits, enemies);
        
        _charComponents = _charComponents.OrderByDescending(character => character.speed).ToList();
        _enemyComponents = _enemyComponents.OrderByDescending(enemy => enemy.speed).ToList();
        
        InitializeFighter(_charComponents);
        InitializeFighter(_enemyComponents); 
        
        StartCoroutine(Battle());
    }
    
    private IEnumerator Battle()
    {
        while (_charComponents.Count > 0 && _enemyComponents.Count > 0)
        {
            var nextUnit = GetNextFighter();

            if (nextUnit is not null)
            {
                if (_charComponents.Contains(nextUnit))
                {
                    _isPrepare = true;
                    Debug.Log($"Hero, {nextUnit.name}");
                    yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.Q) 
                                                     && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.E));

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        yield return StartOffensive(nextUnit, _enemyComponents[numberOfChar]);
                    }
                    if (Input.GetKeyDown(KeyCode.W))
                        yield return StartAbility(nextUnit, _enemyComponents[numberOfChar], KeyCode.W);
                    if (Input.GetKeyDown(KeyCode.R))
                        yield return StartAbility(nextUnit, _enemyComponents[numberOfChar], KeyCode.R);
                    
                    
                    
                    Debug.Log($"Success, {_enemyComponents[numberOfChar].name}");
                    _enemyComponents[numberOfChar].spirtRenderer.sprite = _enemyComponents[numberOfChar].spritePassive;
                    _isPrepare = false;
                    numberOfChar = 0;
                }
                else
                {
                    Debug.Log($"Enemy, {nextUnit.name}");
                    yield return StartOffensive(nextUnit, GetRandomFighter(_charComponents));
                }
            }

            yield return new WaitForSeconds(2f);
            
            IncreaseTurnMethod(_charComponents);
            IncreaseTurnMethod(_enemyComponents);
        }
        
        SceneManager.LoadScene("GameOver");
    }

    private Coroutine StartOffensive(Unit attacker, Unit victim) 
        => StartCoroutine(Offensive(attacker, victim));

    private Coroutine StartAbility(Unit attacker, Unit target, KeyCode code) =>
        code switch
        {
            KeyCode.W => StartCoroutine(Abilitier(target, attacker.skill)),
            KeyCode.R => StartCoroutine(Abilitier(target, attacker.ultimate))
        };

    private IEnumerator Abilitier(Unit target, Ability ability)
    {
        yield return new WaitForSeconds(2f);
        ability.Execute(target);
    }
    
    void Update()
    {
        if (!_isPrepare) 
            return;
        
        _enemyComponents[numberOfChar].spirtRenderer.sprite = _enemyComponents[numberOfChar].spriteActive;
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _enemyComponents[numberOfChar].spirtRenderer.sprite = _enemyComponents[numberOfChar].spritePassive;
            numberOfChar = (numberOfChar + 1) % enemies.Count;
            _enemyComponents[numberOfChar].spirtRenderer.sprite = _enemyComponents[numberOfChar].spriteActive;
        }

        if (!Input.GetKeyDown(KeyCode.LeftArrow)) 
            return;
        _enemyComponents[numberOfChar].spirtRenderer.sprite = _enemyComponents[numberOfChar].spritePassive;
        numberOfChar = Math.Abs((numberOfChar - 1) % enemies.Count);
        _enemyComponents[numberOfChar].spirtRenderer.sprite = _enemyComponents[numberOfChar].spriteActive;
    }
    
    private Unit GetRandomFighter(List<Unit> fighters) => fighters[Random.Range(0, fighters.Count)];

    private void IncreaseTurnMethod(List<Unit> fighters) => fighters.ForEach(fighter => fighter.IncreaseTurnMeter());
    
    private void InitializeFighter(IEnumerable<Unit> fighters)
    {
        foreach (var unit in fighters)
        {
            unit.TurnMeterFilled += OnTurnMeterFilled;
            unit.Died += OnDied;
        }
    }

    private void OnDied(Unit unit)
    {
        Debug.Log($"Death");
        unit.TurnMeterFilled -= OnTurnMeterFilled;
        unit.Died -= OnDied;
        DeleteUnit(unit);
    }

    private void DeleteUnit(Unit unit)
    {
        Debug.Log($"CharsDo: {_charComponents.Count}, Enemies: {_enemyComponents.Count}");
        _charComponents.Remove(unit);
        _enemyComponents.Remove(unit);
        _deletedUnits.Add(unit);
        unit.GameObject().SetActive(false);
        Debug.Log($"CharsAfter: {_charComponents.Count}, Enemies: {_enemyComponents.Count}");
    }

    private void OnTurnMeterFilled(Unit unit)
    {
        if(!_readyFighters.Contains(unit))
            _readyFighters.Enqueue(unit);
    }

    private Unit GetNextFighter()
    {
        if (_readyFighters.Count <= 0)
            return null;
        var next = _readyFighters.Dequeue();
        if (next is null)
            Debug.Log("Is null");
        return !_deletedUnits.Contains(next) ? next : null;
    }

    private void SpawnFighters<T>
        (Vector3 firstPositionality, List<T> fightersComps, List<T> allComps, List<GameObject> fighters)
    {
        foreach (var character in fighters)
        {
            firstPositionality += new Vector3(15, -15);
            var initObject = Instantiate(character, firstPositionality, Quaternion.identity);
            var comp = initObject.GetComponent<T>();
            fightersComps.Add(comp);
            allComps.Add(comp);
        }
    }
    //
    // private void GetFight()
    // {
    //     var currentChar = SelectChar();
    //     currentChar.transform.position = new Vector3(0, 0);
    //     
    //     if (Input.GetButtonDown("CKey"))
    //     {
    //         currentChar.transform.position = new Vector3(0, 0);
    //     }
    // }
    
    private static IEnumerator Offensive(Unit attacker, Unit target)
    {
        Debug.Log($"{target.currentHealthPoints}");
        attacker.UseAttack().Execute(target);
        Debug.Log($"{target.currentHealthPoints}");
        yield return new WaitForSeconds(2f);
    }
    //
    // private GameObject SelectChar()
    // {
    //     if (Input.GetKeyDown(KeyCode.RightArrow))
    //         numberOfChar = (numberOfChar + 1) % squads.Count;
    //     if (Input.GetKeyDown(KeyCode.LeftArrow))
    //         numberOfChar = (numberOfChar + 1) % squads.Count;
    //     return Input.GetKeyDown(KeyCode.KeypadEnter) 
    //         ? squads[numberOfChar] 
    //         : squads[0];
    // }
}
