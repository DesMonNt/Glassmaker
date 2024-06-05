using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Effects;
using FightingScene.Units;
using ObjectSaves;
using ScriptsToQTE;
using UI_Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;
using Unit = FightingScene.Units.Unit;

namespace FightingScene
{
    public class Fight : MonoBehaviour
    {
        [FormerlySerializedAs("FightKey")] public int fightKey;
        public List<GameObject> enemies = new();
        public List<GameObject> squads = new();
        private Queue<Unit> _readyFighters;
        private List<Unit> _charComponents;
        private List<Unit> _enemyComponents;
        private List<Unit> _charComponentsOrder;
        private List<Unit> _enemyComponentsOrder;
        private List<Unit> _selectedComponentsOrder;
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
        [SerializeField] private List<GameObject> queueCircles;
        private List<SpriteRenderer> _queueCirclesRenderers;

        public GameObject winBoss;
        public GameObject winEnemy;
        public GameObject lose;

        private VideoPlayer _videoPlayer;
        public GameObject titres;

        private bool _isBossFight;
    
        private ViewDescription _mouseAttack;
        private ViewDescription _mouseSkill;
        private ViewDescription _mouseUltimate;

        private bool _activeSkill;
        private bool _activeUlt;
        private int _pointsToUseAbility;
        private int _currentPoints;
    
        [SerializeField] private List<GameObject> viewQueue;
        private List<SpriteRenderer> _viewQueueSprites;
        private Dictionary<Unit, (SpriteRenderer renderer, string typeOfFighter)> _spritesDictionary;
        private Dictionary<Unit, AudioClip> _soundsDictionary;
    
        private int _numberOfChar;
        private Unit _selectedChar;
        private float _timer;

        public AudioClip basicFight;
        public AudioClip bossFight;
        private AudioSource _audio;
    
        #region InitializeMembers
        private void Awake()
        {
            _soundsDictionary = new Dictionary<Unit, AudioClip>();
            _videoPlayer = titres.GetComponent<VideoPlayer>();
            winBoss.GameObject().SetActive(false);
            winEnemy.GameObject().SetActive(false);
            lose.GameObject().SetActive(false);
            _selectedComponentsOrder = new List<Unit>();
            _queueCirclesRenderers = new List<SpriteRenderer>();
            _charComponentsOrder = new List<Unit>();
            _enemyComponentsOrder = new List<Unit>();
            _ultPointsRenderers = new List<SpriteRenderer>();
            _currentPoints = 2;
            _activeSkill = false;
            _activeUlt = false;
            _pointsToUseAbility = 5;
            _spritesDictionary = new Dictionary<Unit, (SpriteRenderer renderer, string typeOfFighter)>();
            _viewQueueSprites = viewQueue
                .Select(x => x.GetComponent<SpriteRenderer>())
                .ToList();
            _mouseAttack = buttonAttack.GetComponent<ViewDescription>();
            _mouseSkill = buttonSkill.GetComponent<ViewDescription>();
            _mouseUltimate = buttonUltimate.GetComponent<ViewDescription>();
            CriticalChance = -1;
            IsEndQte = false;
            _readyFighters = new Queue<Unit>();
            _charComponents = new List<Unit>();
            _enemyComponents = new List<Unit>();
            _allUnits = new List<Unit>();
            _deletedUnits = new List<Unit>();
            buttons.SetActive(false);
            qte.SetActive(false);
            skillName.GameObject().SetActive(false);
            _audio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            (squads, enemies) = SetUnitsFromPreviousScene.SetCharactersAndEnemies();
            var firstPosition = new Vector3(-430, 180);
            var firstPositionToEnemy = new Vector3(420, 140);
        
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
            {
                _spritesDictionary.Add(unit,
                    _charComponents.Contains(unit)
                        ? (unit.GameObject().GetComponent<SpriteRenderer>(), "Char")
                        : (unit.GameObject().GetComponent<SpriteRenderer>(), "Enemy"));
            }

            foreach (var unit in listImages) 
                _soundsDictionary.Add(unit, unit.attackSound);

            foreach (var point in ultPoints) 
                _ultPointsRenderers.Add(point.GetComponent<SpriteRenderer>());
        
            foreach (var circle in queueCircles) 
                _queueCirclesRenderers.Add(circle.GetComponent<SpriteRenderer>());
        
            for (var i = 0; i < _currentPoints; i++) 
                _ultPointsRenderers[i].sprite = ultSpriteActive;

            _selectedComponentsOrder = _enemyComponentsOrder;

            if (_enemyComponentsOrder.Any(component => component.GetComponent<Glassmaker>() is not null)) 
                _isBossFight = true;
        
            foreach (var myUnit in _charComponentsOrder)
            {
                foreach (var shard in SetUnitsFromPreviousScene.SavedShards) 
                    myUnit.AddBuff(shard);
            }
        
            _audio.clip = _isBossFight 
                ? bossFight 
                : basicFight;
        
            _audio.Play();
        
            StartCoroutine(Battle());
        }
    

        #endregion


        #region FightProcess
        private IEnumerator Battle()
        {
            yield return new WaitForSeconds(3f);
        
            while (_charComponentsOrder.Count > 0 && _enemyComponentsOrder.Count > 0)
            {
                var nextUnit = GetNextFighter();

                if (nextUnit is not null)
                {
                    var previousHp = nextUnit.currentHealthPoints;
                    nextUnit.ApplyBuffs();
                    StartCoroutine(GetDamageView(nextUnit, previousHp));
                
                    if (_charComponentsOrder.Contains(nextUnit))
                    {
                        _selectedChar = nextUnit;
                        _mouseAttack.description.sprite = nextUnit.attackSprite;
                        _mouseSkill.description.sprite = nextUnit.skillSprite;
                        _mouseUltimate.description.sprite = nextUnit.ultimateSprite;
                    
                        var targetsList = _enemyComponentsOrder;
                        _pointsToUseAbility = nextUnit.CurrentStats.EnergyToUlt;
                        var position = nextUnit.transform.position;
                        var (previousX, previousY) = (position.x, position.y);
                        GoToTarget(nextUnit, new Vector3(0, 0));
                        buttons.SetActive(true);
                    
                        yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.Q) 
                                                         && !_activeSkill && !_activeUlt
                                                         && !_mouseAttack.isAttack);
                    
                        if (Input.GetKeyDown(KeyCode.Q) || _mouseAttack.isAttack)
                        {
                            _isPrepare = true;
                            yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.E));
                            skillName.GameObject().SetActive(false);
                            buttons.SetActive(false);
                            _currentPoints = Math.Clamp(_currentPoints + 1, 0, 5);
                            _ultPointsRenderers[_currentPoints - 1].sprite = ultSpriteActive;
                            PrepareCommonAttack();
                            yield return new WaitWhile(() => !IsEndQte);
                            IsEndQte = false;
                            yield return StartOffense(nextUnit, _enemyComponentsOrder[_numberOfChar]);
                            _mouseAttack.isAttack = false;
                        }

                        else if (_activeSkill)
                        {
                            if (nextUnit.Skill.Target == Targets.Character)
                            {
                                targetsList = _charComponentsOrder;
                                _selectedComponentsOrder = _charComponentsOrder;
                            }
                            _isPrepare = true;
                            yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.E));
                            _ultPointsRenderers[_currentPoints - 1].sprite = ultSpritePassive;
                            _currentPoints--;
                            skillName.GameObject().SetActive(false);
                            yield return StartAbility(nextUnit, targetsList[_numberOfChar % targetsList.Count], KeyCode.W);
                            _mouseSkill.isSkill = false;
                            _activeSkill = false;
                        }
                    
                        else if (_activeUlt)
                        {
                            if (nextUnit.Ultimate.Target == Targets.Character)
                            {
                                targetsList = _charComponentsOrder;
                                _selectedComponentsOrder = _charComponentsOrder;
                            }
                            _isPrepare = true;
                            yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.E));
                            yield return StartAbility(nextUnit, targetsList[_numberOfChar % targetsList.Count], KeyCode.R);
                            _mouseUltimate.isUltimate = false;
                            _currentPoints -=  nextUnit.CurrentStats.EnergyToUlt;
                            for (var i = 0; i < 5; i++)
                            {
                                if (i + 1 > _currentPoints)
                                    _ultPointsRenderers[i].sprite = ultSpritePassive;
                            }
                            _activeUlt = false;
                        }
                        
                        GoToTarget(nextUnit, new Vector3(previousX, previousY));
                        buttons.SetActive(false);
                        _isPrepare = false;
                        _numberOfChar = 0;
                        _activeSkill = false;
                        _selectedComponentsOrder = _enemyComponentsOrder;
                    }
                    else
                    {
                        var (previousX, previousY) = (nextUnit.transform.position.x, 
                            nextUnit.transform.position.y);
                        GoToTarget(nextUnit, new Vector3(0, 0));
                        targetsPointer.transform.position = new Vector3(1500, 1500);
                        yield return StartAIOffensive(nextUnit);
                        GoToTarget(nextUnit, new Vector3(previousX, previousY));
                    }
                }
            
                yield return new WaitForSeconds(1f);
            
                IncreaseTurnMethod(_charComponentsOrder);
                IncreaseTurnMethod(_enemyComponentsOrder);
            }

            yield return StartCoroutine(EndingScene());

            Saves.Fights.Remove(fightKey);
            SceneManager.LoadScene(_charComponentsOrder.Count > 0 
                ? "Tower exploration" 
                : "MainMenu");
        }
    

        #endregion

        private IEnumerator EndingScene()
        {
            if (_charComponentsOrder.Count > 0)
            {
                if (!_isBossFight)
                {
                    winEnemy.GameObject().SetActive(true);
                
                    yield return new WaitForSeconds(2);
                }

                else
                {
                    winBoss.GameObject().SetActive(true);
                    _audio.Stop();
                    yield return new WaitForSeconds(3f);
                    titres.SetActive(true);
                    yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.H) || _videoPlayer.isPlaying);
                }
            }
        
            else if (_enemyComponentsOrder.Count > 0) 
                lose.GameObject().SetActive(true);

            yield return new WaitForSeconds(2);
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
    
        private Coroutine StartOffense(Unit attacker, Unit victim) 
            => StartCoroutine(Offensive(attacker, victim, attacker.UseAttack(), "Attack"));

        private Coroutine StartAbility(Unit attacker, Unit target, KeyCode code) =>
            code switch
            {
                KeyCode.W => StartCoroutine(AbilityUsage(attacker, target)),
                KeyCode.R => StartCoroutine(UltimateUsage(attacker, target)),
                _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
            };

        private void Update()
        {
            var queueAsList = _readyFighters.ToList();
            for (var i = 0; i < queueAsList.Count; i++)
            {
                _viewQueueSprites[i].sprite = _spritesDictionary[queueAsList[i]].renderer.sprite;
                _queueCirclesRenderers[i].color = _spritesDictionary[queueAsList[i]].typeOfFighter == "Enemy" 
                    ? Color.red 
                    : Color.green;
            }

            if ((Input.GetKeyDown(KeyCode.R) || _mouseUltimate.isUltimate) && _currentPoints >= _pointsToUseAbility)
                _activeUlt = true;
        
            if ((Input.GetKeyDown(KeyCode.W) || _mouseSkill.isSkill) && _currentPoints >= 1)
                _activeSkill = true;

            targetsPointer.transform.position =
                _selectedComponentsOrder[_numberOfChar].transform.position +
                new Vector3(0, 
                    -_spritesDictionary[_selectedComponentsOrder[_numberOfChar]].renderer.bounds.extents.y - 58, 0);

            if (!_isPrepare)
            {
                targetsPointer.gameObject.SetActive(false);
                return;
            }
            targetsPointer.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                _numberOfChar =
                    _selectedComponentsOrder[(_numberOfChar + 1) % _selectedComponentsOrder.Count] == _selectedChar
                        ? (_numberOfChar + 2) % _selectedComponentsOrder.Count
                        : (_numberOfChar + 1) % _selectedComponentsOrder.Count;
            

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                _numberOfChar =
                    _selectedComponentsOrder[
                        (_numberOfChar - 1 + _selectedComponentsOrder.Count) % _selectedComponentsOrder.Count] ==
                    _selectedChar
                        ? (_numberOfChar - 2 + _selectedComponentsOrder.Count) % _selectedComponentsOrder.Count
                        : (_numberOfChar - 1 + _selectedComponentsOrder.Count) % _selectedComponentsOrder.Count;
        }

        #region HpBar

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
            unit.TurnMeterFilled -= OnTurnMeterFilled;
            unit.Died -= OnDied;
            DeleteUnit(unit);
        }

        private void DeleteUnit(Unit unit)
        {
            _isPrepare = false;
            _numberOfChar = 0;
            _charComponentsOrder.Remove(unit);
            _enemyComponentsOrder.Remove(unit);
            _deletedUnits.Add(unit);
            unit.GameObject().SetActive(false);
        }

        private void OnTurnMeterFilled(Unit unit)
        {
            if(!_readyFighters.Contains(unit))
                _readyFighters.Enqueue(unit);
        }

        #endregion

        private Unit GetNextFighter()
        {
            if (_readyFighters.Count <= 0)
                return null;
            var next = _readyFighters.Dequeue();
            return !_deletedUnits.Contains(next) 
                ? next 
                : null;
        }

        private static void SpawnFighters<T>
            (Vector3 firstPosition, List<T> fightersComps, List<T> allComps, List<GameObject> fighters)
        {
            var coefficient = 1;
            if (firstPosition == new Vector3(420, 180))
                coefficient = -1;
            for (var i = 0; i < fighters.Count; i++)
            {
                GameObject initObject;
                if (i == 0)
                    initObject = Instantiate(fighters[i], firstPosition, new Quaternion());
                else
                {
                    firstPosition += new Vector3((float)(180 * coefficient * Math.Pow(-1, i)), -200);
                    initObject = Instantiate(fighters[i], firstPosition, Quaternion.identity);
                }
                
                var comp = initObject.GetComponent<T>();
                fightersComps.Add(comp);
                allComps.Add(comp);
            }
        }
    
        private static async Task GoToTarget(Component attacker, Vector3 positionTo)
        {
            var timer = 0f;
            var startPosition = attacker.transform.position;
            const float time = .5f;
            while (timer < time)
            {
                attacker.transform.position = Vector3.Lerp(startPosition, positionTo, timer / time);
                await Task.Yield();
                timer += Time.deltaTime;
            }
        }

        #region Enumerators

        private IEnumerator AbilityUsage(Unit owner, Unit target)
        {
            _audio.PlayOneShot(_soundsDictionary[owner]); 
            skillName.text = owner.Skill.Name;
            skillName.GameObject().SetActive(true);
            owner.UseAbility().Execute(owner, target);
            if (owner.Skill.Attack is not null)
                yield return Offensive(owner, target, owner.Skill.Attack, owner.Skill.Name);
        }
    
        private IEnumerator UltimateUsage(Unit owner, Unit target)
        {
            _audio.PlayOneShot(_soundsDictionary[owner]); 
            owner.UseUltimate().Execute(owner, target);
            if (owner.Ultimate.Attack is not null)
                yield return Offensive(owner, target, owner.Ultimate.Attack, owner.Ultimate.Name);
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
    
        private IEnumerator Offensive(Unit attacker, Unit target, Attack attack, string nameOfSkill)
        {
            _audio.PlayOneShot(_soundsDictionary[attacker]); 
            attacker.CurrentStats = new UnitStats(attacker.CurrentStats,
                criticalChance: attacker.CurrentStats.CriticalChance + CriticalChance);
            skillName.text = nameOfSkill;
            skillName.GameObject().SetActive(true);
            yield return HandleAttack(attacker, attack, target, _selectedComponentsOrder);

            attacker.CurrentStats = new UnitStats(attacker.CurrentStats,
                criticalChance: attacker.CurrentStats.CriticalChance - CriticalChance);
        }

        private IEnumerator AIOffensive(Unit attacker)
        {
            var (action, target) = attacker.Brain.MakeDecision(
                _charComponentsOrder.Select(x => x).ToList(),
                _enemyComponentsOrder.Select(x => x).ToList());
            var previousHp = target.currentHealthPoints;
            _audio.PlayOneShot(_soundsDictionary[attacker]); 
            attacker.CurrentStats = new UnitStats(attacker.CurrentStats,
                criticalChance: attacker.CurrentStats.CriticalChance + CriticalChance);
            switch (action)
            {
                case Ability skill:
                    if (skill.Attack is not null)
                        yield return HandleAttack(attacker, skill.Attack, target, _charComponentsOrder);
                    skillName.text = skill.Name;
                    skillName.GameObject().SetActive(true);
                    action.Execute(attacker, target);
                
                    yield return StartCoroutine(GetDamageView(target, previousHp));
                    break;
                case Attack attack:
                    skillName.text = "Обычная атака";
                    skillName.GameObject().SetActive(true);
                    yield return HandleAttack(attacker, attack, target, _charComponentsOrder);

                    break;
            }
        }

        private IEnumerator HandleAttack(Unit attacker, Attack attack, Unit target, IList<Unit> targets)
        {
            switch (attack.TypeAttack)
            {
                case TypeOfAttack.Aoe:
                {
                    var first = targets[0];
                    var second = targets[1 % targets.Count];
                    var third = targets[2 % targets.Count];

                    yield return GetUnitAttackWithDamageView(attacker, first, attack);

                    if (second != first)
                        yield return GetUnitAttackWithDamageView(attacker, second, attack);

                    if (third != first && third != second)
                        yield return GetUnitAttackWithDamageView(attacker, third, attack);
                    break;
                }
            
                case TypeOfAttack.Group:
                {
                    var neededIndex = targets.IndexOf(target);
                    var previous = target;
                    var next = target;
                    if (neededIndex != 0)
                        previous = targets[neededIndex - 1];
                    if (neededIndex != targets.Count - 1)
                        next = targets[neededIndex + 1];
            
                    if (neededIndex == 0)
                    {
                        yield return GetUnitAttackWithDamageView(attacker, target, attack);
                        yield return GetOtherUnitAttack(attacker, next, attack, 0.5f);
                    }
                    else if (neededIndex == targets.Count - 1)
                    {
                        yield return GetUnitAttackWithDamageView(attacker, target, attack);
                        yield return GetOtherUnitAttack(attacker, previous, attack, 0.5f);
                    }

                    else
                    {
                        yield return GetUnitAttackWithDamageView(attacker, target, attack);
                        yield return GetOtherUnitAttack(attacker, previous, attack, 0.5f);
                        yield return GetOtherUnitAttack(attacker, next, attack, 0.5f);
                    }
                    break;
                }
            
                case TypeOfAttack.Single:
                default:
                    yield return GetUnitAttackWithDamageView(attacker, target, attack);
                    break;
            }
        }

        private IEnumerator GetDamageView(Unit target, float previousHp)
        {
            if (_deletedUnits.Contains(target))
                yield break;
            damageView.transform.position = target.transform.position +
                                            new Vector3(0, _spritesDictionary[target].renderer.bounds.extents.y, 0);
        
            damageView.text = Math.Abs(previousHp - target.currentHealthPoints).ToString(CultureInfo.InvariantCulture);
            damageView.GameObject().SetActive(true);
        
            switch (previousHp - target.currentHealthPoints)
            {
                case 0:
                    damageView.GameObject().SetActive(false);
                    break;
                case < 0:
                    damageView.color = Color.green;
                    break;
                default:
                    damageView.color = Color.red;
                    break;
            }
        
            yield return new WaitForSeconds(1f);
            skillName.GameObject().SetActive(false);
            damageView.GameObject().SetActive(false);
        }
        #endregion
    }
}
