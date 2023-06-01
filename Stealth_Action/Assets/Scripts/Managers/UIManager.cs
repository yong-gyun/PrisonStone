using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    Transform _root;
    public Transform Root
    {
        get
        {
            if (_root == null)
                _root = new GameObject { name = "@UI_Root" }.transform;

            return _root;
        }
    }

    public UI_Scene SceneUI { get; private set; }
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    Queue<UI_Production> _productionQueue = new Queue<UI_Production>();
    int _order = 10;

    public void SetCanvas(GameObject go, bool sort = false)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        go.transform.SetParent(_root);

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            canvas.worldCamera = Camera.main;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
            return null;

        T popup = go.GetOrAddComponent<T>();
        _popupStack.Push(popup);
        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        if (go == null)
            return null;

        T sceneUI = go.GetOrAddComponent<T>();
        SceneUI = sceneUI;
        return sceneUI;
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (go == null)
            return null;

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        T worldSpaceUI = go.GetOrAddComponent<T>();
        worldSpaceUI.transform.SetParent(_root);

        if (parent != null)
            worldSpaceUI.transform.SetParent(parent);

        return worldSpaceUI;
    }

    public T MakeProduction<T>(string name = null) where T : UI_Production
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Production/{name}");

        if (go == null)
            return null;

        T production = go.GetOrAddComponent<T>();
        _productionQueue.Enqueue(production);
        return production;
    }

    public void CloseProduction()
    {
        if (_productionQueue.Count == 0)
            return;

        UI_Production production = _productionQueue.Dequeue();
        Managers.Resource.Destroy(production.gameObject);
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() == popup)
            ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        Managers.Resource.Destroy(SceneUI.gameObject);
        SceneUI = null;
    }
}
