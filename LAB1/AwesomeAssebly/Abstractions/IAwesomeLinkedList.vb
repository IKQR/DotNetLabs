Imports System.Runtime.Serialization

Namespace Awesome.Abstractions

    Public Interface IAwesomeLinkedList(Of T)
        Inherits ICollection(Of T),
                IEnumerable(Of T),
                IReadOnlyCollection(Of T),
                IEnumerableSubscriptor(Of T),
                ICollection,
                IDeserializationCallback,
                ISerializable

        Function Find(item As T) As IAwesomeLinkedListNode(Of T)

    End Interface

End Namespace