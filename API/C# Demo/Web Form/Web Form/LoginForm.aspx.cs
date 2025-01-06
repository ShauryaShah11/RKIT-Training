using System;

namespace Web_Form
{
    public partial class loginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email, password;
            email = emailTextBox.Text;
            password = passwordTextBox.Text;

            // Here, you can add logic to validate the email and password
            // Example: check credentials against a database or a predefined list

            // For simplicity, just checking hardcoded values
            if (email == "shaurya@gmail.com" && password == "123456")
            {
                // Successful login logic (redirect, show message, etc.)
                Response.Redirect("HomePage.aspx"); // redirect to a protected page
            }
            else
            {
                // Invalid credentials logic
                Response.Write("<script>alert('Invalid email or password');</script>");
            }
        }
    }
}