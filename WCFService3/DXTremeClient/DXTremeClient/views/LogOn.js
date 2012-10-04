DXTremeClient.LogOn = function (params) {
    function CallService() {
        $.ajax({
            type: varType, //GET or POST or PUT or DELETE verb
            url: varUrl, // Location of the service
            data: varData, //Data sent to server
            contentType: varContentType, // content type sent to server
            dataType: varDataType, //Expected data format from server
            processdata: varProcessData, //True or False
            success: function (msg) {//On Successfull service call
                alert("success");
            },
            error: function (msg) {//On Successfull service call
                alert("error");
            }
        });
    };
    var handleLogOnClick = function (e) {
        var user = UserName.all[0].value;
        var pass = Password.all[0].value;
        //var myTextField = document.getElementById('UserName');
        //if (user != "")
        //    alert("You entered: " + user)
        //else
        //    alert("Please enter 'User Name'?")

     //   alert("Clicked!");
        varType = "POST";
        varUrl = "http://localhost:63366/AuthenticationService.svc/ValidateUser";
        varData = '{"userName":"asdasdasd", "password":"Test"}';
        varContentType = "application/json; charset=utf-8";


        varDataType = "json";
        varProcessData = true;
        CallService();
        DXTremeClient.app.navigate("");
    };
    //var UserName = ko.observable("");
    //var Password = ko.observable("");
    return {
        Password : ko.observable(""),
        UserName : ko.observable(""),
        handleLogOnClick: handleLogOnClick,
    };
};