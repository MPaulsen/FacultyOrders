<%@ Page Title="" Language="C#" MasterPageFile="~/FacultyOrders.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FacultyOrders.Register" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <h4 style="text-align:center">
        <strong>Registration Page</strong></h4>
        <table class="twelve">
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
                <td class="auto-style3">First Name</td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtFirst" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtFirst" ErrorMessage="Please enter a First Name" 
                        ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Last Name</td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtLast" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtLast" ErrorMessage="Please enter a Last Name" 
                        ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Email</td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegExEmailValid"
                        ControlToValidate="txtEmail"
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Invalid Email Format"
                        runat="server"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Role</td>
                <td class="auto-style4">
                    <asp:RadioButtonList ID="rdoRole" runat="server" RepeatDirection="Horizontal" RepeatLayout="table">
                        <asp:ListItem Selected="True" Text="Computer Purchaser"></asp:ListItem>
                        <asp:ListItem  Text="Non-Computer Purchaser"></asp:ListItem>
                        <asp:ListItem  Text="Accountant"></asp:ListItem>
                        <asp:ListItem  Text="Administrator"></asp:ListItem>

                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" 
                        Text="Register" />
                </td>
                <td><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
            </tr>
        </table>
</asp:Content>