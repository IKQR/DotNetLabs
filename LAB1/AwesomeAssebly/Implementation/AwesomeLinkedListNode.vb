Imports AwesomeAssebly.Awesome.Abstractions

Namespace Awesome.Implementation
    Public NotInheritable Class AwesomeLinkedListNode(Of T)
        Implements IAwesomeLinkedListNode(Of T)

        Friend list_ As IAwesomeLinkedList(Of T)
        Friend next_node_ As AwesomeLinkedListNode(Of T)
        Friend prev_node_ As AwesomeLinkedListNode(Of T)
        Friend data_ As T

        Public Overrides Function ToString() As String
            Return $"'Node with data {data_} typeof {GetType(T).Name}'"
        End Function

        Public Sub New(data As T)
            Me.data_ = data
        End Sub

        Public Sub New(list As IAwesomeLinkedList(Of T), data As T)
            Me.list_ = list
            Me.data_ = data
        End Sub

        Public ReadOnly Property List As IAwesomeLinkedList(Of T)
            Get
                Return List
            End Get
        End Property

        Public ReadOnly Property Next_Node As AwesomeLinkedListNode(Of T)
            Get
                Return next_node_
            End Get
        End Property

        Public ReadOnly Property Prev_Node As AwesomeLinkedListNode(Of T)
            Get
                Return prev_node_
            End Get
        End Property

        Friend Sub Invalidate()
            list_ = Nothing
            next_node_ = Nothing
            prev_node_ = Nothing
        End Sub
    End Class
End Namespace