DXTremeClient.index = function (params) {
var handleTripListItemClick = function (e) {
        var uri = DXTremeClient.app.router.format({
            action: "DetailView",
            //oid: e.itemData.oid
            FirstName: e.itemData.FirstName
        });
        DXTremeClient.app.navigate(uri);
    };
    return {
        todoLists: {
            store: DXTremeClient.db.Module_BusinessObjects_Contact
        },
        handleTripListItemClick: handleTripListItemClick,
    };
};