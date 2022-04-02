Imports System.Runtime.CompilerServices
Imports AwesomeAssebly.Awesome.Abstractions

Public Module IAwesomeLinkedListExtension
    <Extension()>
    Public Sub Print(Of T)(ByVal list As IAwesomeLinkedList(Of T))
        For Each item In list
            Console.Write("{0} ", item)
        Next
        Console.WriteLine()
    End Sub
End Module