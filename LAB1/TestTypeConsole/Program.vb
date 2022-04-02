Imports System.Runtime.CompilerServices
Imports AwesomeAssebly
Imports AwesomeAssebly.Awesome.Abstractions
Imports AwesomeAssebly.Awesome.Implementation

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
        Dim s As IAwesomeLinkedList(Of Integer) = New AwesomeLinkedList(Of Integer)

        s.Add(2)
        s.Print()
        s.Add(3)
        s.Print
        s.Add(5)
        s.Print

        s.Remove(6)
        s.Print
        s.Remove(2)
        s.Print
        s.Remove(5)
        s.Print
        s.Remove(3)
        s.Print

        s.Add(6)
        s.Print
        s.Add(10)
        s.Print
        s.Remove(6)
        s.Print
        s.Add(11)
        s.Print

        Console.WriteLine("*****************************")

        For i = 0 To 20 Step 2
            s.Add(i)
        Next
        s.Print

        Console.WriteLine("Contains 1 => {0} ", s.Contains(1))
        Console.WriteLine("Contains 2 => {0} ", s.Contains(2))

        Console.WriteLine("Find 1 => {0} ", s.Find(1))
        Console.WriteLine("Find 2 => {0} ", s.Find(2))


        Console.WriteLine("Max => {0} ", s.Max())
        Console.WriteLine("Min => {0} ", s.Min())


    End Sub
End Module



