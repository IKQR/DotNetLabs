Imports System.Runtime.Serialization
Imports AwesomeAssebly.Awesome.Abstractions

Namespace Awesome.Implementation

    Public Class AwesomeLinkedList(Of T)
        Implements IAwesomeLinkedList(Of T)

        Dim size_ As UInteger
        Dim head_ As AwesomeLinkedListNode(Of T)
        Dim last_ As AwesomeLinkedListNode(Of T)

        Public Event OnAdd As IEnumerableSubscriptor(Of T).OnAddEventHandler Implements IEnumerableSubscriptor(Of T).OnAdd
        Public Event OnRemove As IEnumerableSubscriptor(Of T).OnRemoveEventHandler Implements IEnumerableSubscriptor(Of T).OnRemove

        Public ReadOnly Property Count As Integer Implements ICollection(Of T).Count
            Get
                Return size_
            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of T).IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                Return Me
            End Get
        End Property

        Private ReadOnly Property ICollection_Count As Integer Implements ICollection.Count, IReadOnlyCollection(Of T).Count
            Get
                Return size_
            End Get
        End Property

        Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
            If (info Is Nothing) Then
                Throw New ArgumentNullException("info")
            End If

            info.AddValue("Count", size_)
            If size_ <> 0 Then
                Dim array(size_ - 1) As T
                CopyTo(array, 0)
                info.AddValue("Data", array)
            End If
        End Sub

        Public Sub Add(item As T) Implements ICollection(Of T).Add
            Dim node = New AwesomeLinkedListNode(Of T)(Me, item)

            If (head_ Is Nothing) Then
                InternalAddIfListEmpty(node)
            Else
                InternalAdd(node)
            End If

            size_ += 1
            RaiseEvent OnAdd(item)

        End Sub

        Private Sub InternalAddIfListEmpty(node As AwesomeLinkedListNode(Of T))
            node.prev_node_ = Nothing
            node.next_node_ = Nothing
            head_ = node
            last_ = node
        End Sub

        Private Sub InternalAdd(node As AwesomeLinkedListNode(Of T))
            node.prev_node_ = last_
            last_.next_node_ = node
            last_ = node
            node.next_node_ = Nothing
        End Sub

        'Private Sub ValidateNewNode(node As AwesomeLinkedListNode(Of T))
        '    If node Is Nothing Then
        '        Throw New ArgumentNullException(NameOf(node))
        '    End If

        '    If node.List IsNot Nothing Then
        '        Throw New InvalidOperationException("Node is attached to another list")
        '    End If
        'End Sub

        Public Sub Clear() Implements ICollection(Of T).Clear
            Dim w_node = head_

            While (w_node IsNot Nothing)
                Dim t_node = w_node
                w_node = w_node.next_node_

                t_node.Invalidate()
            End While

            head_ = Nothing
            size_ = 0
        End Sub

        Public Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
            If (array Is Nothing) Then
                Throw New ArgumentNullException(NameOf(array))
            End If

            If (arrayIndex > array.Length OrElse arrayIndex < 0) Then
                Throw New ArgumentOutOfRangeException(NameOf(arrayIndex))
            End If

            If (arrayIndex > array.Length - size_) Then
                Throw New ArgumentException("Not enought space for copy")
            End If

            Dim w_node = head_
            If (w_node IsNot Nothing) Then
                Do
                    array(arrayIndex) = w_node.data_
                    arrayIndex += 1

                    w_node = w_node.next_node_

                Loop While (w_node IsNot Nothing)
            End If
        End Sub

        Public Sub CopyTo(array As Array, arrayIndex As Integer) Implements ICollection.CopyTo

            If (array Is Nothing) Then
                Throw New ArgumentNullException(NameOf(array))
            End If

            If (array.Rank <> 1) Then
                Throw New ArgumentException("Array has rank more than 1")
            End If

            Dim arr = CType(array, T())
            If arr IsNot Nothing Then
                CopyTo(arr, arrayIndex)
            End If
        End Sub

        Public Sub OnDeserialization(sender As Object) Implements IDeserializationCallback.OnDeserialization
            Throw New NotImplementedException()
        End Sub

        Public Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
            Return If(Find(item) Is Nothing, False, True)
        End Function

        Public Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
            If (head_ Is Nothing) Then
                Return False
            End If

            Dim w_node = head_

            Dim ec = EqualityComparer(Of T).Default
            Do
                Dim t_node = w_node
                w_node = w_node.next_node_

                If (ec.Equals(t_node.data_, item)) Then
                    InternalRemoveNode(t_node)
                    RaiseEvent OnRemove(item)
                    Return True
                End If

            Loop While (w_node IsNot Nothing)

            Return False
        End Function

        Private Sub InternalRemoveNode(node As AwesomeLinkedListNode(Of T))
            If (node Is Nothing) Then
                Throw New ArgumentNullException(NameOf(node))
            End If

            Dim nx = node.next_node_
            Dim pr = node.prev_node_

            If nx IsNot Nothing Then
                nx.prev_node_ = pr
            Else
                last_ = pr
            End If

            If pr IsNot Nothing Then
                pr.next_node_ = nx
            Else
                head_ = nx
            End If

            node.Invalidate()

            size_ -= 1

        End Sub

        Public Function GetEnumerator() As Enumerator
            Return New Enumerator(Me)
        End Function

        Public Function IEnumerator_T_GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            Return Me.GetEnumerator()
        End Function

        Private Function IEnumerator_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Me.GetEnumerator()
        End Function

        Public Function Find(item As T) As IAwesomeLinkedListNode(Of T) Implements IAwesomeLinkedList(Of T).Find
            Dim w_node = head_
            Dim ec = EqualityComparer(Of T).Default

            If (w_node IsNot Nothing) Then
                If (item Is Nothing) Then
                    Do
                        If (w_node.data_ Is Nothing) Then
                            Return w_node
                        End If
                        w_node = w_node.next_node_
                    Loop While (w_node IsNot Nothing)
                Else
                    Do
                        If (ec.Equals(w_node.data_, item)) Then
                            Return w_node
                        End If
                        w_node = w_node.next_node_
                    Loop While (w_node IsNot Nothing)
                End If
            End If

            Return Nothing
        End Function

        Public Structure Enumerator
            Implements IEnumerator(Of T),
                        IEnumerator,
                        ISerializable,
                        IDeserializationCallback

            Dim list_ As AwesomeLinkedList(Of T)
            Dim node_ As AwesomeLinkedListNode(Of T)
            Dim current_ As T
            Dim index_ As Integer

            Public Sub New(list As AwesomeLinkedList(Of T))
                Me.New()
                Me.list_ = list
                node_ = list_.head_
                current_ = CType(Nothing, T)
                index_ = 0


            End Sub

            Public ReadOnly Property Current As T Implements IEnumerator(Of T).Current
                Get
                    Return current_
                End Get
            End Property

            Private ReadOnly Property IEnumerator_Current As Object Implements IEnumerator.Current
                Get
                    If (index_ = 0 OrElse (index_ - list_.Count = 1)) Then
                        Throw New InvalidOperationException("It's not possible when index of enumerator is out of range")
                    End If

                    Return current_
                End Get
            End Property

            Public Sub Reset() Implements IEnumerator.Reset
                current_ = CType(Nothing, T)
                node_ = list_.head_
                index_ = 0
            End Sub

            Public Sub Dispose() Implements IDisposable.Dispose
                'Object is not disposable
            End Sub

            Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
                If (info Is Nothing) Then
                    Throw New ArgumentNullException("info")
                End If

                info.AddValue("AwesomeLinkedList", list_)
                info.AddValue("Current", current_)
                info.AddValue("Index", index_)
            End Sub

            Public Sub OnDeserialization(sender As Object) Implements IDeserializationCallback.OnDeserialization
                Throw New NotImplementedException()
            End Sub

            Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
                If (node_ Is Nothing) Then
                    index_ = list_.Count + 1
                    Return False
                End If

                index_ += 1
                current_ = node_.data_
                node_ = node_.next_node_

                Return True
            End Function
        End Structure
    End Class
End Namespace