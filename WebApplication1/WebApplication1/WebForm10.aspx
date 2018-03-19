<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm10.aspx.cs" Inherits="WebApplication1.WebForm10" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family :Arial" > 
        <asp:Button ID="btnGetDataFromDB" runat="server" Text="Get data from DB " OnClick="btnGetDataFromDB_Click" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Undo" />
        <br />
        <br />
        <asp:GridView ID="gdStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCancelingEdit="gdStudents_RowCancelingEdit" OnRowDeleting="gdStudents_RowDeleting" OnRowEditing="gdStudents_RowEditing" OnRowUpdating="gdStudents_RowUpdating">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                <asp:BoundField DataField="TotalMarks" HeaderText="TotalMarks" SortExpression="TotalMarks" />
            </Columns>
        </asp:GridView>
        
        <asp:Button ID="btlUpdateDB" runat="server" Text="Update Database Table" OnClick="btlUpdateDB_Click" />
<asp:Label ID="lblMessage" runat="server"></asp:Label>



    </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
