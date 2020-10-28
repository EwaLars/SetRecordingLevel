
Imports WaveLib.AudioMixer
'Imports System.Timers
Imports System.Deployment
Imports System.Threading

Module Module1

    Private Mix As New Mixer(MixerType.Recording)
    Private Level As Integer
    Public Sub Main(args As String())

        If Integer.TryParse(args(0), Level) Then
            Dim createdNew As Boolean
            Dim waitHandle = New EventWaitHandle(False, EventResetMode.AutoReset, "CF2D4313-33DE-489D-9721-6AFF69841DEA", createdNew)
            Dim signaled = False
            If Not createdNew Then
                Log("Inform other process to stop.")
                waitHandle.[Set]()
                Log("Informer exited.")
                Return
            End If
            Dim timer = New Timer(AddressOf OnTimerElapsed, Nothing, TimeSpan.Zero, TimeSpan.FromSeconds(5))
            Do
                signaled = waitHandle.WaitOne(TimeSpan.FromSeconds(5))
            Loop While Not signaled
            Log("Got signal to kill myself.")
        End If
    End Sub

    Private Sub Log(ByVal message As String)
        Console.WriteLine(DateTime.Now & ": " & message)
    End Sub

    Private Sub OnTimerElapsed(ByVal state As Object)
        For Each line As MixerLine In Mix.Lines
            If line.Volume <> Level Then
                line.Volume = Level
            End If
        Next
        Log("Checked")
    End Sub

End Module
