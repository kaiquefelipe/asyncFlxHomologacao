<%@ Page Async="True" Language="C#" AutoEventWireup="true" CodeBehind="Async.aspx.cs" Inherits="Assincrono.Async" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        function cwRating(idPost, target, like, user) {
            $.ajax({
                type: "POST",
                url: "Async.aspx/returnData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger;
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Label ID="lblInsert" runat="server" Text="Gerar select INTO"></asp:Label>
        </div>

        <br />
        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert Async" />
        <asp:Button ID="btnInsertSync" runat="server" OnClick="btnInsertSync_Click" Text="Insert Sync" Width="105px" />
        <asp:Button ID="BtnShow" runat="server" Text="Show Result" OnClientClick="cwRating(); return false;"/>
        <br />

        <div style="height: 1000px; overflow-y:auto">
            <asp:Label ID="resultsSpan" runat="server" Text="Gerar select INTO"></asp:Label>
            <textarea id="resultsTextBox" runat="server" cols="20" rows="2"></textarea>
        </div>

    </form>
</body>
</html>
