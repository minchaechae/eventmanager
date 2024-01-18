# Sample Code
## Subscribe to/Unsubscribe from events
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        EventManager.Subscribe(EventType.EffectSelected, OnEffectSelected);
        EventManager.Subscribe(EventType.NothingSelected, OnEffectUnselected);
    }

    private void OnEffectSelected(object ob)
    {
        if ((GameObject)ob == this.gameObject)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnEffectUnselected()
    {
        meshRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EventType.EffectSelected, OnEffectSelected);
        EventManager.Unsubscribe(EventType.NothingSelected, OnEffectUnselected);
    }

  ## Invoke events
    if (Physics.Raycast(lineOut, out hit))
    {
        endPosition = hit.point;

        var ob = hit.collider.gameObject;
        if (ob.layer == LayerMask.NameToLayer("Effect"))
            EventManager.TriggerEvent(EventType.EffectSelected, ob);
        else
            EventManager.TriggerEvent(EventType.NothingSelected);
    }
    else
    {
        EventManager.TriggerEvent(EventType.NothingSelected);
    }
