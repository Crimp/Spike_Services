$(function () {
    var URL = "http://localhost:62445/UnSecuredDataService.svc"
    //var URL = "http://localhost:54002/CustomAuthenticationDataService.svc"
    DXTremeClient.currentUser = {
        Password: ko.observable(""),
        UserName: ko.observable("")
    };
    DXTremeClient.db = new DevExpress.data.EntityStoreContext({
        service: {
            url: URL,
            errorHandler: function (error) {
                if (error.httpStatus == 401) {
                    DXTremeClient.app.navigate("LogOn/null");
                } else {
                    alert(error.message);
                }
            },
            beforeSend: function (sender) {
                sender.headers.UserName = DXTremeClient.currentUser.UserName();
                sender.headers.Password = DXTremeClient.currentUser.Password();
            }
        },
        entities: {
            Contact: { key: "oid" }
        }
    });
});