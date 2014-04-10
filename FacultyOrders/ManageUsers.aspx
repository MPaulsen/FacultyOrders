<%@ Page Language="C#" MasterPageFile="~/FacultyOrders.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="FacultyOrders.ManageUsers" EnableEventValidation="false" %>




<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="Body">
    <div>
        <br />
        <asp:Button Text ="Add User" runat="server" ID="btnAdd" OnClick="btnAdd_click" CssClass="centered"></asp:Button>
        <asp:GridView runat="server" ID="grdUsers" AllowSorting="true" AutoGenerateColumns="false" OnSorting="grdUsers_Sorting">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="ID" SortExpression="UserID" ItemStyle-CssClass="col" />
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" ItemStyle-CssClass="col" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" ItemStyle-CssClass="col" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" ItemStyle-CssClass="col" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-CssClass="col" />
                <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" ItemStyle-CssClass="col" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you wish to delete this user?')" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
