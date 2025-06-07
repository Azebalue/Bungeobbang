using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Object/Customer Data", order = int.MaxValue)]
public class CustomerData : ScriptableObject
{

    [SerializeField]
    private CustomerType _customer; //�մ� ����
    public CustomerType _Customer { get { return _customer; } }

/*    [SerializeField]
    private Sprite image; //�⺻ ���� ��������Ʈ
    public Sprite Image { 
        get {  } }*/
    
    public Sprite GetImage(int index = 0)
    {
        return Managers.Resource.LoadSprite($"Customers/{_Customer}", index);
    }
/*    [SerializeField]
    private Sprite image; //���� ��������Ʈ
    public Sprite Image { get { return image; } }

    [SerializeField]
    private Sprite disappoint; //�Ǹ��� ��������Ʈ
    public Sprite Image { get { return disappoint; } }*/

    [SerializeField]
    private FillingType flavor; //��ȣ�ϴ� �ؾ ����
    public FillingType Flavor  { get { return flavor; } }

    [SerializeField]
    private string[] greetingText; //�λ� ����
    public string[] GreetingText { get { return greetingText; } }

    /*    [SerializeField]
        private string[] greetingText; //�λ� ����
        public string[] GreetingText { get { return greetingText; } }*/

    [SerializeField]
    private string[] disappointingText; // �ֹ��� ���� �Ǹ����� ��, �� �� ����
    public string[] DisappointingText { get { return disappointingText; } }
}

