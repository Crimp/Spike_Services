DXTremeClient.index = function (params) {
    function CallService() {
        $.ajax({
            type: varType, //GET or POST or PUT or DELETE verb
            url: varUrl, // Location of the service
            data: varData, //Data sent to server
            contentType: varContentType, // content type sent to server
            dataType: varDataType, //Expected data format from server
            processdata: varProcessData, //True or False
            success: function (msg) {//On Successfull service call
                
            },
            error: function (msg) {//On Successfull service call

            }
        });
    } var handleTripListItemClick = function (e) {

        varType = "POST";
        varUrl = "http://localhost:63366/AuthenticationService.svc/ValidateUser";
        varData = '{"userName":"asdasdasd", "password":"Test"}';
        varContentType = "application/json; charset=utf-8";

        
        varDataType = "json";
        varProcessData = true;
        CallService();

        //$.getJSON("https://minakov-w8.corp.devexpress.com/ODataDemoService/WebService1.asmx/Login2 {userName:ad}", {},
		//			function (data) {
		//			    alert(data);
		//			});

        //var uri = DXTremeClient.app.router.format({
        //    action: "DetailView",
        //    //oid: e.itemData.oid
        //    FirstName: e.itemData.FirstName
        //});
        //DXTremeClient.app.navigate(uri);
    };
    return {
        todoLists: {
            store: DXTremeClient.db.Module_BusinessObjects_Contact
        },
        handleTripListItemClick: handleTripListItemClick,
    };
};