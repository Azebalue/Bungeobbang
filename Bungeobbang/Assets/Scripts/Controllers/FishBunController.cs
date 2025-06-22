using UnityEngine;
using UnityEngine.EventSystems;

public class FishBunController : MonoBehaviour,
    IDragHandler, IEndDragHandler, IPointerClickHandler
{
    #region ����
    static int numsOfFisBun = 0; //��ü plate���� �������� �ؾ ����

    GameObject parentMold; //���̾��Ű �� �θ� ������Ʈ
    GameObject filling; //�ؾ�� ������Ʈ ������ �ؾ �� ���ӿ�����Ʈ

    public FillingType fillingType; //�ؾ ��
    public Vector3 spawnPos; //�ʱ� ��ġ

    public CookingState state = CookingState.bottomBatter; //�ʱ� ����
    QualityStatus bakingStatus;
    /*    public QualityStatus batterStatus;
    public QualityStatus fillingStatus;
    public QualityStatus warmStatus;*/

    bool isDraggable 
    {
        get { return (state == CookingState.cooked); }
    }

    //���� ���� ���� ���� ����
    float startDelta;
    float endDelta;
    float bakingTime 
        { get { return endDelta - startDelta; } }

    static float requiredTime = 6; //perfect�ϰ� �������� �� �ɸ��� ��
    static float burntingTime = 15; //Ÿ������ �� �ɸ��� ��

    #endregion

    #region Ŭ�� ���� �������̽� ����
    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable == false)
            return;

        //�ؾ ���� ������Ʈ ��ġ �巡�� �ϴ� ������ �̵�
        Vector3 mouse = Camera.main.ScreenToWorldPoint(eventData.position);
        mouse.z = 0;
        gameObject.transform.position = mouse;


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable == false)
            return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(eventData.position);
        int dropLayerMask = LayerMask.GetMask("DropZone"); //��� ���̾�: DropZone���� ������Ʈ(������/��������)
        RaycastHit2D hit = Physics2D.Raycast(mouse, Vector2.zero, 0, dropLayerMask);

        if(hit.collider != null)
        {
            //�����뿡 ���� ���
            if (hit.collider.CompareTag("displayPlate"))
            {

                //spawnPos = DisplateController.SetPos(fillingType);
                DisplateController.Set(gameObject);
                transform.position = spawnPos;


            }
            else
            {
                if (hit.collider.CompareTag("customer"))
                {
                    Debug.Log($"{hit.collider.name}���� �ؾ ����");
                    DisplateController.Reset(fillingType);
                    ServeFishBun(hit.transform.gameObject);

                }

                Destroy(gameObject);

            }

            //���� ����
            Debug.Log($"{hit.collider.name}�� �ε���");
            parentMold.GetComponent<MoldController>().IsFilled = false;


        }
        else
        {
            //����ġ
            gameObject.transform.position = spawnPos;

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cooking();
    }

    #endregion

    //�ʱ�ȭ
    void Start()
    {
        //���� ������Ʈ : ����&�̸�
        gameObject.transform.SetParent(parentMold.transform);
        gameObject.name = $"{++numsOfFisBun}";
        //Debug.Log($"{gameObject.name} �ؾ ����");

        //��ġ ����
        transform.position = spawnPos;
        transform.localScale = parentMold.transform.localScale * 1.4f;

        //���� ������Ʈ ����
        filling = Util.FindObject(gameObject, Define.FillingString, true);
        filling.SetActive(false);

        //�̹���
        GetComponent<SpriteRenderer>().sprite =
            Managers.Resource.LoadSprite("FishBunState_proto", (int)CookingState.bottomBatter);

        //����
        state = CookingState.bottomBatter;
    }

    public void Set(Vector3 spawnPos, GameObject parentMold)
    {
        this.spawnPos = spawnPos;
        this.parentMold = parentMold;

    }

    void ServeFishBun(GameObject sprite)
    {
        //�θ� ������Ʈ���� ��ũ��Ʈ ����
        CustomerController controller = sprite.GetComponentInParent<CustomerController>();
        controller.Eat(fillingType, bakingStatus);

    }
    #region �丮 �Լ�
    void cooking()
    {
        switch (state)
        {

            case CookingState.bottomBatter:
                addFilling();
                break;

            case CookingState.filled:
                addBatter();
                break;

            case CookingState.topBatter:
                baking();
                break;

            case CookingState.cooking:
                cooked();
                break;
        }

        //PolygonCollider2D reset
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();

    }

    void addFilling()
    {
        if (ToolController.selectedTool.CompareTag("filling") == false)
            return;

        filling.SetActive(true);
        fillingType = ToolController.selectedTool.filling;
        filling.GetComponent<SpriteRenderer>().sprite
            = Managers.Resource.LoadSprite("fillingChunks", (int)fillingType);


        ++state;

        //��� ��� ���
        Managers.Game.IngredientCost += (int) (Define.FillingPrice[(int)fillingType] * Define.FillingCostRate);
        //Debug.Log($"{(int) (Define.FillingPrice[(int)fillingType] * Define.FillingCostRate)}���� ��");
    }

    void addBatter()
    {
        if (ToolController.selectedTool.CompareTag("kettle") == false)
            return;

        
        startDelta = Managers.Game.delta; //1�ܰ� ���� ���� ����

        GetComponent<SpriteRenderer>().sprite =
            Managers.Resource.LoadSprite("FishBunState_proto", (int)CookingState.topBatter-1);

        //layer �켱���� ó��
        //�̱���
        //�ؾ ���빰 ���� ó��
/*            SpriteRenderer sr = filling.GetComponent<SpriteRenderer>();
        Color color = sr.color; 
        color.a = 0.5f;
        sr.color = color;*/

        ++state;
        
    }

    void baking()
    {
        endDelta = Managers.Game.delta; //1�ܰ� ���� ���� ����

        if (nextState() == true)
            startDelta = endDelta; //2�ܰ� ���� ���� ����


    }

    void cooked()
    {
        endDelta = Managers.Game.delta; //2�ܰ� ���� ���� ����
        nextState();

    }

    //���� �ܰ�� �������� �� Ȯ���ϰ� �Ǹ� ���� ��ȯ
    bool nextState()
    {
        //requiredTime�� �̳����� �ȱ�����
        if (bakingTime < requiredTime)
            return false;

        int imgIndex;

        //burntingTime�� ������ Ž
        if (bakingTime > burntingTime)
        {
            state = CookingState.cooked;
            bakingStatus = QualityStatus.excessive;
            imgIndex = 7;

        }
        else
        {
            if (state == CookingState.topBatter)
            {
                imgIndex = (int)CookingState.cooking;
                state = CookingState.cooking;

            }
            else 
                //if (state == CookingState.cooking)
            {
                bakingStatus = QualityStatus.perfect;
                imgIndex = (int)CookingState.cooked;
                state = CookingState.cooked;

            }
        }

        GetComponent<SpriteRenderer>().sprite =
            Managers.Resource.LoadSprite("FishBunState_proto", imgIndex);
        
        return true;

    }


    #endregion
}
