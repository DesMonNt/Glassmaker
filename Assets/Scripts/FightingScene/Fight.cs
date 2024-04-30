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
using UnityEngine.Serialization;
using UnityEngine.UI;
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
    [SerializeField] private GameObject qte;
    public static bool IsEndQte;
    [FormerlySerializedAs("QTEAccuracy")] [SerializeField] Text qteAccuracy;
    public static float CriticalChance;
    
    private int _numberOfChar;
    
    void Awake()
    {
        CriticalChance = -1;
        IsEndQte = false;
        qte.SetActive(false);
        _readyFighters = new Queue<Unit>();
        _charComponents = new();
        _enemyComponents = new();
        _allUnits = new();
        _deletedUnits = new();
    }

    private void Start()
    {
        var firstPos = new Vector3(-662, 360);
        var firstPosToEnemy = new Vector3(0, 360);
        
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
                                                     && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.R));

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        RandomSpawner.SpawnDelay = 86;
                        AccuracyText.MaxSum = 0;
                        AccuracyText.CurrentSum = 0;
                        qteAccuracy.text = "100.00%";
                        qteAccuracy.color = new Color(255, 255, 255);
                        RandomSpawner.CanSpawn = true;
                        qte.SetActive(true);
                        yield return new WaitWhile(() => !IsEndQte);
                        IsEndQte = false;
                        yield return StartOffensive(nextUnit, _enemyComponents[_numberOfChar]);
                    }
                        
                    if (Input.GetKeyDown(KeyCode.W))
                        yield return StartAbility(nextUnit, _enemyComponents[_numberOfChar], KeyCode.W);
                    
                    if (Input.GetKeyDown(KeyCode.R))
                        yield return StartAbility(nextUnit, _enemyComponents[_numberOfChar], KeyCode.R);

                    if (_enemyComponents.Count > 0)
                    {
                        Debug.Log($"Success, {_enemyComponents[_numberOfChar].name}");
                        _enemyComponents[_numberOfChar].spirtRenderer.sprite = _enemyComponents[_numberOfChar].spritePassive;
                    }
                    _isPrepare = false;
                    _numberOfChar = 0;
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
            KeyCode.W => StartCoroutine(Abilitier(attacker, target, attacker.skill)),
            KeyCode.R => StartCoroutine(Abilitier(attacker, target, attacker.ultimate)),
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
        };
    
    void Update()
    {
        if (!_isPrepare) 
            return;
        
        _enemyComponents[_numberOfChar].spirtRenderer.sprite = _enemyComponents[_numberOfChar].spriteActive;
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _enemyComponents[_numberOfChar].spirtRenderer.sprite = _enemyComponents[_numberOfChar].spritePassive;
            _numberOfChar = (_numberOfChar + 1) %  _enemyComponents.Count;
            _enemyComponents[_numberOfChar].spirtRenderer.sprite = _enemyComponents[_numberOfChar].spriteActive;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _enemyComponents[_numberOfChar].spirtRenderer.sprite = _enemyComponents[_numberOfChar].spritePassive;
            _numberOfChar = (_numberOfChar - 1 + _enemyComponents.Count) % _enemyComponents.Count;
            _enemyComponents[_numberOfChar].spirtRenderer.sprite = _enemyComponents[_numberOfChar].spriteActive;
        }
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
        Debug.Log("Death");
        unit.TurnMeterFilled -= OnTurnMeterFilled;
        unit.Died -= OnDied;
        DeleteUnit(unit);
    }

    private void DeleteUnit(Unit unit)
    {
        Debug.Log($"CharsDo: {_charComponents.Count}, Enemies: {_enemyComponents.Count}");
        _isPrepare = false;
        _numberOfChar = 0;
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
            firstPositionality += new Vector3(215, -215);
            var initObject = Instantiate(character, firstPositionality, Quaternion.identity);
            var comp = initObject.GetComponent<T>();
            fightersComps.Add(comp);
            allComps.Add(comp);
        }
    }

    private IEnumerator Abilitier(Unit owner, Unit target, string titleOfAbility)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log($"{titleOfAbility}");
        Debug.Log($"{target}, HP: {target.currentHealthPoints}, Damage: {owner.CurrentStats.Damage}");
        Abilities.DictOfAbilities[titleOfAbility].Execute(owner, target);
        Debug.Log($"{target},HP: {target.currentHealthPoints}, Damage: {owner.CurrentStats.Damage}");
    }
    
    private IEnumerator Offensive(Unit attacker, Unit target)
    {
        Debug.Log($"{target}, HP: {target.currentHealthPoints}");
        attacker.UseAttack().Execute(target, CriticalChance);
        Debug.Log($"{target},HP: {target.currentHealthPoints}");
        yield return new WaitForSeconds(2f);
    }
}
