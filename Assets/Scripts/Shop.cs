using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private GameObject panel;
    private Text coinsText;
    private PlayerController player;
    private PlayerShooting shooter;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        shooter = FindObjectOfType<PlayerShooting>();
        CreateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Toggle();
    }

    void CreateUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) return;

        GameObject btnObj = new GameObject("ShopBtn");
        btnObj.transform.SetParent(canvas.transform, false);
        Button btn = btnObj.AddComponent<Button>();
        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        RectTransform btnRect = btnObj.GetComponent<RectTransform>();
        btnRect.anchorMin = new Vector2(0, 1);
        btnRect.anchorMax = new Vector2(0, 1);
        btnRect.pivot = new Vector2(0, 1);
        btnRect.anchoredPosition = new Vector2(10, -10);
        btnRect.sizeDelta = new Vector2(80, 40);

        GameObject btnTxt = new GameObject("Text");
        btnTxt.transform.SetParent(btnObj.transform, false);
        Text bt = btnTxt.AddComponent<Text>();
        bt.text = "Shop";
        bt.fontSize = 18;
        bt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        bt.color = Color.white;
        bt.alignment = TextAnchor.MiddleCenter;
        RectTransform btr = btnTxt.GetComponent<RectTransform>();
        btr.anchorMin = Vector2.zero;
        btr.anchorMax = Vector2.one;
        btr.sizeDelta = Vector2.zero;
        btn.onClick.AddListener(Toggle);

        panel = new GameObject("ShopPanel");
        panel.transform.SetParent(canvas.transform, false);

        Image bg = panel.AddComponent<Image>();
        bg.color = new Color(0, 0, 0, 0.85f);
        RectTransform pr = panel.GetComponent<RectTransform>();
        pr.anchorMin = new Vector2(0.5f, 0.5f);
        pr.anchorMax = new Vector2(0.5f, 0.5f);
        pr.pivot = new Vector2(0.5f, 0.5f);
        pr.anchoredPosition = Vector2.zero;
        pr.sizeDelta = new Vector2(360, 400);

        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(panel.transform, false);
        Text title = titleObj.AddComponent<Text>();
        title.text = "Shop";
        title.fontSize = 32;
        title.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        title.color = Color.white;
        title.alignment = TextAnchor.MiddleCenter;
        RectTransform tr = titleObj.GetComponent<RectTransform>();
        tr.anchorMin = new Vector2(0.5f, 1);
        tr.anchorMax = new Vector2(0.5f, 1);
        tr.pivot = new Vector2(0.5f, 1);
        tr.anchoredPosition = new Vector2(0, -10);
        tr.sizeDelta = new Vector2(300, 40);

        coinsText = MakePanelText(panel, "Coins", new Vector2(0, -50), 20);
        coinsText.alignment = TextAnchor.MiddleCenter;

        AddUpgrade(panel, "Move Speed +0.5", "$500", 0, () => BuySpeed(), 500);
        AddUpgrade(panel, "Fire Rate -0.01", "$300", 1, () => BuyFireRate(), 300);
        AddUpgrade(panel, "Bullet Speed +3", "$400", 2, () => BuyBulletSpeed(), 400);

        GameObject closeObj = new GameObject("CloseBtn");
        closeObj.transform.SetParent(panel.transform, false);
        Button closeBtn = closeObj.AddComponent<Button>();
        Image closeImg = closeObj.AddComponent<Image>();
        closeImg.color = new Color(0.6f, 0.1f, 0.1f);
        RectTransform cr = closeObj.GetComponent<RectTransform>();
        cr.anchorMin = new Vector2(0.5f, 0);
        cr.anchorMax = new Vector2(0.5f, 0);
        cr.pivot = new Vector2(0.5f, 0.5f);
        cr.anchoredPosition = new Vector2(0, 30);
        cr.sizeDelta = new Vector2(200, 40);
        GameObject ct = new GameObject("Text");
        ct.transform.SetParent(closeObj.transform, false);
        Text ct2 = ct.AddComponent<Text>();
        ct2.text = "Close";
        ct2.fontSize = 22;
        ct2.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        ct2.color = Color.white;
        ct2.alignment = TextAnchor.MiddleCenter;
        RectTransform crt = ct.GetComponent<RectTransform>();
        crt.anchorMin = Vector2.zero;
        crt.anchorMax = Vector2.one;
        crt.sizeDelta = Vector2.zero;
        closeBtn.onClick.AddListener(Close);

        panel.SetActive(false);
    }

    void AddUpgrade(GameObject parent, string label, string cost, int index, UnityEngine.Events.UnityAction action, int price)
    {
        float y = -90 - index * 70;

        GameObject row = new GameObject("Upgrade" + index);
        row.transform.SetParent(parent.transform, false);

        GameObject lbl = new GameObject("Label");
        lbl.transform.SetParent(row.transform, false);
        Text lt = lbl.AddComponent<Text>();
        lt.text = label;
        lt.fontSize = 18;
        lt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        lt.color = Color.white;
        lt.alignment = TextAnchor.MiddleLeft;
        RectTransform lr = lbl.GetComponent<RectTransform>();
        lr.anchorMin = new Vector2(0, 1);
        lr.anchorMax = new Vector2(0, 1);
        lr.pivot = new Vector2(0, 0.5f);
        lr.anchoredPosition = new Vector2(20, y + 5);
        lr.sizeDelta = new Vector2(200, 30);

        GameObject costObj = new GameObject("Cost");
        costObj.transform.SetParent(row.transform, false);
        Text ct = costObj.AddComponent<Text>();
        ct.text = cost;
        ct.fontSize = 18;
        ct.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        ct.color = new Color(1, 0.8f, 0.2f);
        ct.alignment = TextAnchor.MiddleRight;
        RectTransform cor = costObj.GetComponent<RectTransform>();
        cor.anchorMin = new Vector2(1, 1);
        cor.anchorMax = new Vector2(1, 1);
        cor.pivot = new Vector2(1, 0.5f);
        cor.anchoredPosition = new Vector2(-80, y + 5);
        cor.sizeDelta = new Vector2(60, 30);

        GameObject btnObj = new GameObject("Btn");
        btnObj.transform.SetParent(row.transform, false);
        Button btn = btnObj.AddComponent<Button>();
        Image btnImg = btnObj.AddComponent<Image>();
        btnImg.color = new Color(0.2f, 0.6f, 0.2f);
        RectTransform br = btnObj.GetComponent<RectTransform>();
        br.anchorMin = new Vector2(1, 1);
        br.anchorMax = new Vector2(1, 1);
        br.pivot = new Vector2(1, 0.5f);
        br.anchoredPosition = new Vector2(-10, y + 5);
        br.sizeDelta = new Vector2(60, 30);
        GameObject bt = new GameObject("Text");
        bt.transform.SetParent(btnObj.transform, false);
        Text bt2 = bt.AddComponent<Text>();
        bt2.text = "Buy";
        bt2.fontSize = 16;
        bt2.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        bt2.color = Color.white;
        bt2.alignment = TextAnchor.MiddleCenter;
        RectTransform btt = bt.GetComponent<RectTransform>();
        btt.anchorMin = Vector2.zero;
        btt.anchorMax = Vector2.one;
        btt.sizeDelta = Vector2.zero;
        btn.onClick.AddListener(action);
    }

    Text MakePanelText(GameObject parent, string name, Vector2 pos, int size)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent.transform, false);
        Text txt = obj.AddComponent<Text>();
        txt.fontSize = size;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.color = Color.white;
        txt.alignment = TextAnchor.MiddleCenter;
        RectTransform r = txt.GetComponent<RectTransform>();
        r.anchorMin = new Vector2(0.5f, 1);
        r.anchorMax = new Vector2(0.5f, 1);
        r.pivot = new Vector2(0.5f, 1);
        r.anchoredPosition = pos;
        r.sizeDelta = new Vector2(300, 30);
        return txt;
    }

    void Toggle()
    {
        if (panel.activeSelf) Close();
        else Open();
    }

    void Open()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
        UpdateCoins();
    }

    void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    void UpdateCoins()
    {
        if (GameManager.Instance != null)
            coinsText.text = "Coins: $" + GameManager.Instance.GetCoins().ToString("N0");
    }

    void BuySpeed()
    {
        if (player == null || !GameManager.Instance.SpendCoins(500)) return;
        player.moveSpeed += 0.5f;
        UpdateCoins();
    }

    void BuyFireRate()
    {
        if (shooter == null || !GameManager.Instance.SpendCoins(300)) return;
        shooter.fireRate = Mathf.Max(0.02f, shooter.fireRate - 0.01f);
        UpdateCoins();
    }

    void BuyBulletSpeed()
    {
        if (!GameManager.Instance.SpendCoins(400)) return;
        Bullet.globalSpeedBonus += 3f;
        UpdateCoins();
    }
}
