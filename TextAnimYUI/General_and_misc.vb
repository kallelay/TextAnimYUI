Module General_and_misc
    Sub Debugx(ByVal str$)
        Form1.TextBox4.Text &= "[" & Now.ToLongTimeString & "]" & str & vbNewLine
    End Sub
End Module
