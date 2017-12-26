<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Office.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>



    <form action="Index.aspx" method="post">
        <label>User Name</label>
        <input type="text" name="username" placeholder="enter your username" />
        <label>Password</label>
        <input type="password" name="password" placeholder="enter password here"/>
        <div>
            <input type="button" name="submit" value="submit" onclick="getData();"/>
        </div>

    </form>

    <script type="text/javascript">

        

        function getData() {
            var username = document.getElementsByName("username")[0].value;
            var password = document.getElementsByName("password")[0].value;

            var xhr = new XMLHttpRequest();
            var url = "url";
            var data = "email="+username+"&password="+password;
            xhr.open("POST", "profile.aspx", true);
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    // do something with response
                    console.log(xhr.responseText);
                }
            };
            xhr.send(data);
        }


    </script>

</body>
</html>
