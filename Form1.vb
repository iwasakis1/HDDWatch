Imports System.IO

Public Class Form1
	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Dim filePath As String = "C:\temp\testfile.bin"
		Dim fileSize As Integer = 1024 * 1024 * 100 ' 100 MB
		Dim dataArray(fileSize - 1) As Byte

		Dim stopwatch As New Stopwatch()

		' 書き込み速度を測定
		stopwatch.Start()
		Using fs As New FileStream(filePath, FileMode.Create, FileAccess.Write)
			fs.Write(dataArray, 0, dataArray.Length)
		End Using
		stopwatch.Stop()
		Console.WriteLine("書き込み速度: " & Math.Round((fileSize / stopwatch.Elapsed.TotalSeconds) / (1024 * 1024), 2) & " MB/s")

		' 読み込み速度を測定
		stopwatch.Reset()
		stopwatch.Start()
		Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
			fs.Read(dataArray, 0, dataArray.Length)
		End Using
		stopwatch.Stop()
		Console.WriteLine("読み込み速度: " & Math.Round((fileSize / stopwatch.Elapsed.TotalSeconds) / (1024 * 1024), 2) & " MB/s")

		' テストファイルを削除
		File.Delete(filePath)
	End Sub

End Class
