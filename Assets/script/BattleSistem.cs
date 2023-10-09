using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START,ORDER, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSistem : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySelectCanvas;
    [SerializeField]
    private GameObject optionCanvas;
    [SerializeField]
    private GameObject[] selectEnemyButtons;

    [SerializeField]
    private GameObject[] mainCharactersPrefab;
    [SerializeField]
    private GameObject[] enemyCharactersPrefab;

    [SerializeField]
    private Transform[] mainCharactersStation;
    [SerializeField]
    private Transform[] enemyCharactersStation;

    private BattleState state;

    private LinkedList<int> order = new LinkedList<int>();

    private Queue<Character> EnemyOrder = new Queue<Character>();
    private Queue<Character> MainChOrder = new Queue<Character>();

    private Dictionary<int, MainCharacter> MainCharacters = new Dictionary<int, MainCharacter>();
    private Dictionary<int, EnemyCharacter> EnemyCharacters = new Dictionary<int, EnemyCharacter>();

    [SerializeField]
    int tusTurnos, turnosEnemigo;

    public static BattleSistem Instance;

    private int i;
    private Character it;

    public void Start()
    {

        state = BattleState.START;

        StartCoroutine(SetupBattle());

        if (Instance != null && Instance != this)
        {

            Destroy(this);

        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(this);

        }

    }

    IEnumerator SetupBattle()
    {
        
        for (int i = 0; i < enemyCharactersPrefab.Length; i++)
        {

            GameObject clon = Instantiate(enemyCharactersPrefab[i], enemyCharactersStation[i].position, enemyCharactersStation[i].rotation);
            EnemyCharacter clonInf = clon.GetComponent<EnemyCharacter>();
            EnemyCharacters.Add(i,clonInf);

            

        }

        for (int i = 0; i < mainCharactersPrefab.Length; i++)
        {
            

            GameObject clon = Instantiate(mainCharactersPrefab[i], mainCharactersStation[i].position, mainCharactersStation[i].rotation);
            MainCharacter clonInf = clon.GetComponent<MainCharacter>();
            MainCharacters.Add(i,clonInf);

            

        }
                
        yield return new WaitForSeconds(2f);
        
        state = BattleState.ORDER;
        
        Order();

    }

    void Order()
    {
        
        if (EnemyOrder.Count == 0 && MainChOrder.Count == 0)
        {
            Dictionary<int, Character> HelpDiccionary = new Dictionary<int, Character>();

            foreach (KeyValuePair<int, EnemyCharacter> main in EnemyCharacters)
            {

                HelpDiccionary.Add(main.Key, main.Value);

            }

            SubOrder(HelpDiccionary, EnemyOrder);

            HelpDiccionary.Clear();

            foreach(KeyValuePair<int, MainCharacter> main in MainCharacters)
            {

                HelpDiccionary.Add(main.Key, main.Value);

            }

            SubOrder(HelpDiccionary, MainChOrder);
            
        }

        //print("main; " + MainChOrder.Count + "\n" + "Enemy: " + EnemyOrder.Count);

        if (EnemyOrder.Count == 0)
        {
            state = BattleState.PLAYERTURN;

            PlayerTurn();

        }
        else if (MainChOrder.Count == 0)
        {
            //print("hola");
            state = BattleState.ENEMYTURN;

            StartCoroutine(EnemyTurn(EnemyOrder.Peek()));

        }
        else
        {
            
            if (EnemyOrder.Peek().GetSpeed() > MainChOrder.Peek().GetSpeed())
            {
                
                state = BattleState.ENEMYTURN;
                
                StartCoroutine(EnemyTurn(EnemyOrder.Peek()));

            }
            else
            {
                state = BattleState.PLAYERTURN;
                
                PlayerTurn();

            }

        }

    }

    void SubOrder(Dictionary<int, Character> c, Queue<Character> q)
    {

        foreach (KeyValuePair<int, Character> mains in c)
        {

            order.AddFirst(mains.Value.GetSpeed());

        }

        int[] a = new int[order.Count];

        order.CopyTo(a, 0);

        Array.Sort(a);

        Array.Reverse(a);

        int reserSpeed = -1;

        foreach (int n in a)
        {

            if (reserSpeed != n)
            {

                foreach (KeyValuePair<int, Character> mains in c)
                {
                    if (mains.Value.GetSpeed() == n)
                    {
                        reserSpeed = n;

                        q.Enqueue(mains.Value);


                    }

                }

            }

        }

        order.Clear();

    }

    void PlayerTurn()
    {
        tusTurnos++;

        optionCanvas.SetActive(true);

        int i = 0;

        foreach (GameObject b in selectEnemyButtons)
        {

            if (EnemyCharacters.ContainsKey(i))
                b.SetActive(true);
            else
                b.SetActive(false);

            i++;
        }

    }

    IEnumerator EnemyTurn(Character me)
    {
        
        EnemyOrder.Dequeue();
        turnosEnemigo++;

        print("te ataca el enemigo");

        yield return new WaitForSeconds(1f);

        foreach(KeyValuePair<int, MainCharacter> main in MainCharacters)
        {

            main.Value.SetCanDodge(true);

        }
        
        int rng;
        do
        {

            rng = UnityEngine.Random.Range(0, mainCharactersPrefab.Length);
            //print("comrpueba el numero random es = " + rng);
            //print("el rng existe" + MainCharacters.ContainsKey(rng));
            //if (MainCharacters.ContainsKey(rng) == true)
            //    print("contaiss ");

            //if (MainCharacters != null)
            //    print("maincha" );

        } while (MainCharacters.ContainsKey(rng) == false && MainCharacters.Count != 0);

        //print("el numero random es = " + rng);

        MainCharacters.TryGetValue(rng, out MainCharacter it);

        me.Attack(it);

        this.it = it;
        i = rng;

        //CheckLive("Main");//active at the end of enemy animation

        //if (it.GetHP() <= 0)

    }

    public void OnAtackButton()
    {

        enemySelectCanvas.SetActive(true);
        optionCanvas.SetActive(false);

    }

    public void OnSelectEnemy(int i)
    {

        Character me = MainChOrder.Peek();
                 
        EnemyCharacters.TryGetValue(i, out EnemyCharacter it);

        this.it = it;
        this.i = i;

        MainChOrder.Dequeue();

        enemySelectCanvas.SetActive(false);

        me.Attack(it);
  
    }

    public void CheckLive(string rival)
    {
        Dictionary<int, Character> HelpDiccionary = new Dictionary<int, Character>();
        if(rival == "Main")
        {

            foreach (KeyValuePair<int, MainCharacter> main in MainCharacters)
            {

                main.Value.SetCanDodge(true);

            }

        }
        

        foreach (KeyValuePair<int, MainCharacter> main in MainCharacters)
        {

            main.Value.SetCanDodge(false);

        }

        if (it.GetHP() <= 0)
        {
            
            if (rival == "Main")
            {
                
                MainCharacters.Remove(i);
                
                
                foreach (KeyValuePair<int, MainCharacter> main in MainCharacters)
                {
                    
                    HelpDiccionary.Add(main.Key, main.Value);

                }
                MainChOrder.Clear();

                SubOrder(HelpDiccionary, MainChOrder);
                
            }
                
            else
                EnemyCharacters.Remove(i);
        }
        
        
        if (MainCharacters.Count == 0)
        {

            state = BattleState.LOST;

            EndBattle();

        }
        else if (EnemyCharacters.Count == 0)
        {

            state = BattleState.WON;

            EndBattle();

        }

        else
        {

            state = BattleState.ORDER;

            Order();

        }

    }

    void EndBattle()
    {

        if(state == BattleState.WON)
        {

            print("ganaste");

        }

        if (state == BattleState.LOST)
        {

            print("perdiste");

        }

    }

}
