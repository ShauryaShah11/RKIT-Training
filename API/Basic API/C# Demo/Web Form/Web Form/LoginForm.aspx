<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Web_Form.loginForm" MasterPageFile="~/Site.Master" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
    Login - My Site
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- start login form section -->
        <div class="d-flex justify-content-center align-items-center vh-100">
            <div class="row justify-content-center">
                <div class="card">
                    <div class="card-body">
                        <h3 class="card-title text-center">Login</h3>
                        <form id="LoginForm" runat="server">
                            <!-- Email input -->
                            <div class="mb-3">
                                <label for="email" class="form-label">Email address</label>
                                <asp:TextBox 
                                    ID="emailTextBox" 
                                    runat="server" 
                                    placeholder="Enter your email" 
                                    required="required" 
                                    TextMode="Email" 
                                    CssClass="form-control" />
                                <asp:RequiredFieldValidator 
                                    ID="emailRequired" 
                                    runat="server" 
                                    ControlToValidate="emailTextBox" 
                                    ErrorMessage="Email is required" 
                                    ForeColor="Red" />
                            </div>
                            <!-- Password input -->
                            <div class="mb-3">
                                <label for="password" class="form-label">Password</label>
                                <asp:TextBox 
                                    ID="passwordTextBox" 
                                    runat="server" 
                                    placeholder="Enter your password" 
                                    required="required" 
                                    TextMode="Password" 
                                    CssClass="form-control" />
                                <asp:RequiredFieldValidator 
                                    ID="passwordRequired" 
                                    runat="server" 
                                    ControlToValidate="passwordTextBox" 
                                    ErrorMessage="Password is required" 
                                    ForeColor="Red" />
                            </div>                    

                            <!-- Submit button -->
                            <asp:Button 
                                ID="loginButton" 
                                runat="server" 
                                Text="Login" 
                                OnClick="LoginButton_Click" 
                                CssClass="btn btn-primary w-100" />                         

                            
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <!-- end login form section -->
</asp:Content>
