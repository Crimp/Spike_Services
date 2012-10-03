$(function () {
    
    var URL = "https://minakov-w8.corp.devexpress.com/ODataDemoService/ODataDemoService.svc"
    //var URL = "http://localhost:63725/ODataDemoService.svc"
    DXTremeClient.db = new DevExpress.data.EntityStoreContext({
        service: {
            url: URL,
            errorHandler: function (error) {
                alert(error.message);
            }
        },
        entities: {
            Module_BusinessObjects_Contact: { key: "oid" }
        }
    });
});