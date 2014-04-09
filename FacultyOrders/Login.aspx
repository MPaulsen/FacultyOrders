<%@ Page Title="" Language="C#" MasterPageFile="~/FacultyOrders.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FacultyOrders.Login" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">

    <div class="row">
        <div class="twelve columns notabs">
            <div class="titles">
                Login
            </div>
            <div class="innercontent">
                <table>
                    <tr>
                        <td class="auto-style3">Username</td>
                        <td class="auto-style4">
                            <asp:TextBox ID="txtUser" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="TextBoxUser" runat="server"
                                ControlToValidate="txtUser" ErrorMessage="Please enter a valid Username"
                                ForeColor="#CC0000"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Password</td>
                        <td class="auto-style4">
                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="TextBoxPass" runat="server"
                                ControlToValidate="txtPass" ErrorMessage="Please enter a valid Password"
                                ForeColor="#CC0000"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style4">
                            <asp:Button ID="bt_Login" runat="server" OnClick="bt_Login_Click"
                                Text="Login" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>


</asp:Content>
