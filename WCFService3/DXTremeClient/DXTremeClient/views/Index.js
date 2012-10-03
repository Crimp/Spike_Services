DXTremeClient.index = function (params) {
    //var viewModel = {
    //    todoLists: {
    //        store: DXTremeClient.db.Module_BusinessObjects_Contact
    //    }
    //}
    var handleTripListItemClick = function (e) {
        var uri = DXTremeClient.app.router.format({
            action: "DetailView",
            contactOid: e.oid,
         
        });
        DXTremeClient.app.navigate(e.itemData.uri);
    };
    return {
        todoLists: {
            store: DXTremeClient.db.Module_BusinessObjects_Contact
        },
        handleTripListItemClick: handleTripListItemClick,
    };
};