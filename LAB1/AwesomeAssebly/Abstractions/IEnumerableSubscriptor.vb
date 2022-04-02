Public Interface IEnumerableSubscriptor(Of T)

    Event OnAdd(data As T)
    Event OnRemove(data As T)

End Interface
