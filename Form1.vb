Imports System.IO

Public Class Form1
	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Dim filePath = TextBox1.Text
		'filePathがファイルかフォルダかを判定
		If File.Exists(filePath) Then
			Label1.Text = "ファイルです"
			'filepathがファイルの場合、一つ上のフォルダ名を取得
			filePath = Path.GetDirectoryName(filePath)
		ElseIf Directory.Exists(filePath) Then
			Label1.Text = "フォルダです"
		Else
			Label1.Text = "存在しません"
		End If

		filePath = Path.Combine(filePath, "testfile.bin")

		Dim fileSize As Integer = 1024 * 1024 * 100 ' 100 MB
		Dim dataArray(fileSize - 1) As Byte


		Label1.Text = "" : Label2.Text = ""

		Dim stopwatch As New Stopwatch()

		' 書き込み速度を測定
		stopwatch.Start()
		Using fs As New FileStream(filePath, FileMode.Create, FileAccess.Write)
			fs.Write(dataArray, 0, dataArray.Length)
		End Using
		stopwatch.Stop()
		Label1.Text = "書き込み速度: " & Math.Round((fileSize / stopwatch.Elapsed.TotalSeconds) / (1024 * 1024), 2) & " MB/s"

		' 読み込み速度を測定
		stopwatch.Reset()
		stopwatch.Start()
		Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
			fs.Read(dataArray, 0, dataArray.Length)
		End Using
		stopwatch.Stop()
		Label2.Text = "読み込み速度: " & Math.Round((fileSize / stopwatch.Elapsed.TotalSeconds) / (1024 * 1024), 2) & " MB/s"

		' テストファイルを削除
		File.Delete(filePath)
	End Sub

	'textbox1のドロップを受け入れる
	Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			e.Effect = DragDropEffects.Copy
		Else
			e.Effect = DragDropEffects.None
		End If
	End Sub

	'textbox1にドロップされたファイルのパスを表示
	Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
		Dim filePath As String = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())(0)
		TextBox1.Text = filePath
	End Sub

End Class
