<%@ Page Language="C#" MasterPageFile="~/FacultyOrders.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="FacultyOrders.EditUser" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="Body">

    <div class="row">
        <div class="twelve columns notabs">
            <div class="titles">
                <h3>Edit User</h3>
            </div>
            <div class="innercontent">
                <div>
                    <asp:Label ID="lblStatus" ForeColor="Red" runat="server" />
                </div>
                <div>
                    User ID:
                    <asp:TextBox ID="txtUserID" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div>
                    UserName:
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </div>
                <div>
                    Password:
                    <asp:TextBox ID="txtPass" runat="server" Enabled="false"></asp:TextBox>
                    <asp:CheckBox ID="chkPass" runat="server" Text="Change Password" OnCheckedChanged="chkPass_CheckedChanged" AutoPostBack="true" ViewStateMode="Enabled"/>
                </div>
                <div>
                    First Name:
                    <asp:TextBox ID="txtFirst" runat="server"></asp:TextBox>
                </div>
                <div>
                    Last Name:
                    <asp:TextBox ID="txtLast" runat="server"></asp:TextBox>
                </div>
                <div>
                    Email:
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegExEmailValid"
                        ControlToValidate="txtEmail"
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Invalid Email Format"
                        runat="server"></asp:RegularExpressionValidator>
                    <br />
                </div>
                <div>
                    Role:
                    <asp:DropDownList runat="server" ID="ddlRole">
                        <asp:ListItem>PurchaserComp</asp:ListItem>
                        <asp:ListItem>PurchaserOther</asp:ListItem>
                        <asp:ListItem>Accountant</asp:ListItem>
                        <asp:ListItem>Admin</asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div><br />
                    <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
