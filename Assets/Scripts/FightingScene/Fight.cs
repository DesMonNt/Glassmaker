using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.Linq;
using Effects;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> squads;
    private Queue<Unit> _readyFighters;
    private List<Unit> _charComponents;
    private List<Unit> _enemyComponents;
    private List<Unit> _charComponentsOrder;
    private List<Unit> _enemyComponentsOrder;
    private List<Unit> _allUnits;
    private List<Unit> _deletedUnits;
    private bool _isPrepare;
    [SerializeField] private GameObject qte;
    public static bool IsEndQte;
    [FormerlySerializedAs("QTEAccuracy")] [SerializeField] Text qteAccuracy;
    public static float CriticalChance;
    [SerializeField] public Text damageView;
    [SerializeField] public Image targetsPointer;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject buttonAttack;
    [SerializeField] private GameObject buttonSkill;
    [SerializeField] private GameObject buttonUltimate;
    [SerializeField] private Text skillName;
    [SerializeField] private Sprite ultSpritePassive;
    [SerializeField] private Sprite ultSpriteActive;
    [SerializeField] private List<GameObject> ultPoints;
    private List<SpriteRenderer> _ultPointsRenderers;
    
    private ViewDescription _mouseAttack;
    private ViewDescription _mouseSkill;
    private ViewDescription _mouseUltimate;
    
    private bool _activeUlt;
    private int _pointsToUlt;
    private int _currentPoints;
    
    [SerializeField] private List<GameObject> viewQueue;
    private List<SpriteRenderer> _viewQueueSprites;
    private Dictionary<string, SpriteRenderer> _spritesDictionary;
    
    private int _numberOfChar;
    
    private void Awake()
    {
        _charComponentsOrder = new();
        _enemyComponentsOrder = new();
        _ultPointsRenderers = new();
        _currentPoints = 0;
        _activeUlt = false;
        _pointsToUlt = 5;
        _spritesDictionary = new();
        _viewQueueSprites = viewQueue
            .Select(x => x.GetComponent<SpriteRenderer>())
            .ToList();
        _mouseAttack = buttonAttack.GetComponent<ViewDescription>();
        _mouseSkill = buttonSkill.GetComponent<ViewDescription>();
        _mouseUltimate = buttonUltimate.GetComponent<ViewDescription>();
        CriticalChance = -1;
        IsEndQte = false;
        _readyFighters = new Queue<Unit>();
        _charComponents = new();
        _enemyComponents = new();
        _allUnits = new();
        _deletedUnits = new();
        buttons.SetActive(false);
        qte.SetActive(false);
        skillName.GameObject().SetActive(false);
    }

    private void Start()
    {
        var firstPosition = new Vector3(-520, 420);
        var firstPositionToEnemy = new Vector3(400, 480);
        
        SpawnFighters(firstPosition, _charComponents, _allUnits, squads);
        SpawnFighters(firstPositionToEnemy, _enemyComponents, _allUnits, enemies);
        
        _charComponentsOrder = _charComponents.OrderByDescending(character => character.speed).ToList();
        _enemyComponentsOrder = _enemyComponents.OrderByDescending(enemy => enemy.speed).ToList();
        
        InitializeFighter(_charComponentsOrder);
        InitializeFighter(_enemyComponentsOrder); 
        
        damageView.GameObject().SetActive(false);
        
        var listImages = _charComponentsOrder.ToList();
        listImages.AddRange(_enemyComponentsOrder);
        foreach (var unit in listImages)
            _spritesDictionary.Add(unit.name, unit.GameObject().GetComponent<SpriteRenderer>());

        foreach (var point in ultPoints) 
            _ultPointsRenderers.Add(point.GetComponent<SpriteRenderer>());

        StartCoroutine(Battle());
    }
    
    private IEnumerator Battle()
    {
        while (_charComponentsOrder.Count > 0 && _enemyComponentsOrder.Count > 0)
        {
            var nextUnit = GetNextFighter();

            if (nextUnit is not null)
            {
                if (_charComponentsOrder.Contains(nextUnit))
                {
                    if (_currentPoints > 0 && _currentPoints <= _pointsToUlt)
                        _ultPointsRenderers[_currentPoints - 1].sprite = ultSpriteActive;
                    var (previousX, previousY) = (nextUnit.transform.position.x, nextUnit.transform.position.y);
                    GoToTarget(nextUnit, new Vector3(0, 0));
                    _isPrepare = true;
                    buttons.SetActive(true);
                    Debug.Log($"Hero, {nextUnit.name}");
                        
                    
                    yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.Q) 
                                                     && !Input.GetKeyDown(KeyCode.W) && !_activeUlt
                                                     && !_mouseAttack.IsAttack && !_mouseSkill.IsSkill);
                    
                    if (Input.GetKeyDown(KeyCode.Q) || _mouseAttack.IsAttack)
                    {
                        skillName.GameObject().SetActive(false);
                        buttons.SetActive(false);
                        PrepareCommonAttack();
                        yield return new WaitWhile(() => !IsEndQte);
                        IsEndQte = false;
                        yield return StartOffensive(nextUnit, _enemyComponentsOrder[_numberOfChar]);
                        _mouseAttack.IsAttack = false;
                    }

                    if (Input.GetKeyDown(KeyCode.W) || _mouseSkill.IsSkill)
                    {
                        skillName.GameObject().SetActive(false);
                        yield return StartAbility(nextUnit, _enemyComponentsOrder[_numberOfChar], KeyCode.W);
                        _mouseSkill.IsSkill = false;
                    }
                    
                    if (_activeUlt)
                    {
                        _activeUlt = false;
                        yield return StartAbility(nextUnit, _enemyComponentsOrder[_numberOfChar], KeyCode.R);
                        _mouseUltimate.IsUltimate = false;
                        _currentPoints = -1;
                        foreach (var render in _ultPointsRenderers) 
                            render.sprite = ultSpritePassive;
                    }
                        
                    GoToTarget(nextUnit, new Vector3(previousX, previousY));
                    buttons.SetActive(false);
                    _currentPoints++;
                    if (_enemyComponentsOrder.Count > 0)
                    {
                        Debug.Log($"Success, {_enemyComponentsOrder[_numberOfChar].name}");
                        _enemyComponentsOrder[_numberOfChar].spriteRenderer.sprite 
                            = _enemyComponentsOrder[_numberOfChar].spritePassive;
                    }
                    _isPrepare = false;
                    _numberOfChar = 0;
                }
                else
                {
                    var (previousX, previousY) = (nextUnit.transform.position.x, nextUnit.transform.position.y);
                    GoToTarget(nextUnit, new Vector3(0, 0));
                    targetsPointer.transform.position = new Vector3(1500, 1500);
                    Debug.Log($"Enemy, {nextUnit.name}");
                    yield return StartAIOffensive(nextUnit);
                    GoToTarget(nextUnit, new Vector3(previousX, previousY));
                }
            }
            
            yield return new WaitForSeconds(2f);
            
            IncreaseTurnMethod(_charComponentsOrder);
            IncreaseTurnMethod(_enemyComponentsOrder);
        }
        
        SceneManager.LoadScene("GameOver");
    }

    private void PrepareCommonAttack()
    {
        RandomSpawner.SpawnDelay = 86;
        AccuracyText.MaxSum = 0;
        AccuracyText.CurrentSum = 0;
        qteAccuracy.text = "100.00%";
        qteAccuracy.color = new Color(255, 255, 255);
        RandomSpawner.CanSpawn = true;
        qte.SetActive(true);
    }

    private Coroutine StartAIOffensive(Unit attacker) 
        => StartCoroutine(AIOffensive(attacker));
    
    private Coroutine StartOffensive(Unit attacker, Unit victim) 
        => StartCoroutine(Offensive(attacker, victim));

    private Coroutine StartAbility(Unit attacker, Unit target, KeyCode code) =>
        code switch
        {
            KeyCode.W => StartCoroutine(Abilitier(attacker, target)),
            KeyCode.R => StartCoroutine(Ultimater(attacker, target)),
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
        };

    private void Update()
    {
        var queueAsList = _readyFighters.ToList();
        for (var i = 0; i < queueAsList.Count; i++)
            _viewQueueSprites[i].sprite = _spritesDictionary[queueAsList[i].name].sprite;
        
        if (!_isPrepare) 
            return;

        if ((Input.GetKeyDown(KeyCode.R) || _mouseUltimate.IsUltimate) && _currentPoints >= _pointsToUlt)
            _activeUlt = true;

        targetsPointer.transform.position =
            _enemyComponentsOrder[_numberOfChar].transform.position +
              new Vector3(0, -_spritesDictionary[_enemyComponentsOrder[_numberOfChar].name].bounds.extents.y - 58, 0);
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _numberOfChar = (_numberOfChar + 1) %  _enemyComponentsOrder.Count;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _numberOfChar = (_numberOfChar - 1 + _enemyComponentsOrder.Count) % _enemyComponentsOrder.Count;
        }
    }
    
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
        Debug.Log($"CharsDo: {_charComponentsOrder.Count}, Enemies: {_enemyComponentsOrder.Count}");
        _isPrepare = false;
        _numberOfChar = 0;
        _charComponentsOrder.Remove(unit);
        _enemyComponentsOrder.Remove(unit);
        _deletedUnits.Add(unit);
        unit.GameObject().SetActive(false);
        Debug.Log($"CharsAfter: {_charComponentsOrder.Count}, Enemies: {_enemyComponentsOrder.Count}");
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
        for (var i = 0; i < fighters.Count; i++)
        {
            firstPositionality += new Vector3((float)(180 * Math.Pow(-1, i)), -240);
            var initObject = Instantiate(fighters[i], firstPositionality, Quaternion.identity);
            var comp = initObject.GetComponent<T>();
            fightersComps.Add(comp);
            allComps.Add(comp);
        }
    }
    
    private void GoToTarget(Unit attacker, Vector3 positionTo) => attacker.transform.position = positionTo;

    private IEnumerator Abilitier(Unit owner, Unit target)
    {
        var previousHp = target.currentHealthPoints;
        Debug.Log($"{owner}, HP: {owner.currentHealthPoints}, Damage: {owner.CurrentStats.Damage}");
        skillName.text = owner.skill;
        skillName.GameObject().SetActive(true);
        owner.UseAbility().Execute(owner, target);
        yield return StartCoroutine(GetDamageView(target, previousHp));
        Debug.Log($"{owner},HP: {owner.currentHealthPoints}, Damage: {owner.CurrentStats.Damage}");
    }
    
    private IEnumerator Ultimater(Unit owner, Unit target)
    {
        var previousHp = target.currentHealthPoints;
        Debug.Log($"{target}, HP: {target.currentHealthPoints}, Damage: {owner.CurrentStats.Damage}");
        skillName.text = owner.ultimate;
        skillName.GameObject().SetActive(true);
        owner.UseUltimate().Execute(owner, target);
        yield return StartCoroutine(GetDamageView(target, previousHp));
        Debug.Log($"{target},HP: {target.currentHealthPoints}, Damage: {owner.CurrentStats.Damage}");
    }
    
    
    private object GetUnitAttackWithDamageView(Unit attacker, Unit target, Attack attack)
    {
        var previousHp = target.currentHealthPoints;
        attack.Execute(attacker, target);
        return StartCoroutine(GetDamageView(target, previousHp));
    }
    
    private object GetOtherUnitAttack(Unit attacker, Unit target, Attack attack, float coefficient)
    {
        var previousHp = target.currentHealthPoints;
        attack.Execute(attacker, target, coefficient);
        return StartCoroutine(GetDamageView(target, previousHp));
    }
    
    private IEnumerator Offensive(Unit attacker, Unit target)
    {
        attacker.CurrentStats = new UnitStats(attacker.CurrentStats,
            criticalChance: attacker.CurrentStats.CriticalChance + CriticalChance);
        skillName.text = "Attack";
        skillName.GameObject().SetActive(true);
        var attack = attacker.UseAttack();
        Debug.Log($"Type: {attack.TypeAttack}");
        switch (attack.TypeAttack)
        {
            case TypeOfAttack.Aoe:
            {
                var first = _enemyComponents[0];
                var second = _enemyComponents[1 % _enemyComponents.Count];
                var third = _enemyComponents[2 % _enemyComponents.Count];

                yield return GetUnitAttackWithDamageView(attacker, first, attack);

                if (second != first)
                    yield return GetUnitAttackWithDamageView(attacker, second, attack);

                if (third != first && third != second)
                    yield return GetUnitAttackWithDamageView(attacker, third, attack);
                break;
            }
            
            case TypeOfAttack.Group:
            {
                var neededIndex = _enemyComponents.IndexOf(target);
                var previous = target;
                var next = target;
                if (neededIndex != 0)
                    previous = _enemyComponents[neededIndex - 1];
                if (neededIndex != _enemyComponents.Count - 1)
                    next = _enemyComponents[neededIndex + 1];
            
                if (neededIndex == 0)
                {
                    yield return GetUnitAttackWithDamageView(attacker, target, attack);
                    yield return GetOtherUnitAttack(attacker, next, attack, 0.5f);
                }
                else if (neededIndex == _enemyComponents.Count - 1)
                {
                    yield return GetUnitAttackWithDamageView(attacker, target, attack);
                    yield return GetOtherUnitAttack(attacker, previous, attack, 0.5f);
                }

                else
                {
                    yield return GetUnitAttackWithDamageView(attacker, target, attack);
                    yield return GetOtherUnitAttack(attacker, previous, attack, 0.5f);
                    yield return GetOtherUnitAttack(attacker, next, attack, 0.5f);
                };
                break;
            }
            
            case TypeOfAttack.Single:
            default:
                yield return GetUnitAttackWithDamageView(attacker, target, attack);
                break;
        }

        attacker.CurrentStats = new UnitStats(attacker.CurrentStats,
            criticalChance: attacker.CurrentStats.CriticalChance - CriticalChance);
    }

    private IEnumerator AIOffensive(Unit attacker)
    {
        var (action, target) = attacker.Brain.MakeDesicion(
            _charComponentsOrder.Select(x => x as Character).ToList(),
            _enemyComponentsOrder.Select(x => x as Enemy).ToList());
        var previousHp = target.currentHealthPoints;
        attacker.CurrentStats = new UnitStats(attacker.CurrentStats,
            criticalChance: attacker.CurrentStats.CriticalChance + CriticalChance);
        if (action is not Attack)
        {
            skillName.text = action.ToString();
            skillName.GameObject().SetActive(true);
        }

        if (action is Attack)
        {
            skillName.text = "Райт клик";
            skillName.GameObject().SetActive(true);
        }
        
        action.Execute(attacker, target);
        attacker.CurrentStats = new UnitStats(attacker.CurrentStats,
            criticalChance: attacker.CurrentStats.CriticalChance - CriticalChance);
        yield return StartCoroutine(GetDamageView(target, previousHp));
    }

    private IEnumerator GetDamageView(Unit target, float previousHp)
    {
        if (_deletedUnits.Contains(target))
            yield break;
        damageView.transform.position = target.transform.position +
                                        new Vector3(0, _spritesDictionary[target.name].bounds.extents.y, 0);
        damageView.text = (previousHp - target.currentHealthPoints).ToString(CultureInfo.InvariantCulture);
        damageView.GameObject().SetActive(true);
        yield return new WaitForSeconds(2f);
        skillName.GameObject().SetActive(false);
        damageView.GameObject().SetActive(false);
    }
}
