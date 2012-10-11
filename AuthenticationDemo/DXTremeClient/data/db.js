$(function () {
    //var URL = "http://localhost:62445/UnSecuredDataService.svc"
    var URL= "http://localhost:54002/CustomAuthenticationDataService.svc"
    DXTremeClient.db = new DevExpress.data.EntityStoreContext({
        service: {
            url: URL,
            errorHandler: function (error) {
                alert(error.message);
            },
            beforeSend: function (sender) {
                sender.headers.UserName = "Sam";
                sender.headers.Password = "sam";
            }
        },
        entities: {
            Contact: { key: "oid" }
        }
    });
});