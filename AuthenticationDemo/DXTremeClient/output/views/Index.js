DXTremeClient.index = function (params) {
    var handleTripListItemClick = function (e) {
        var uri = DXTremeClient.app.router.format({
            action: "DetailView",
            oid: e.itemData.oid
        });
        DXTremeClient.app.navigate(uri);
    };
    return {
        todoLists: {
            store: DXTremeClient.db.Contact
        },
        handleTripListItemClick: handleTripListItemClick,
    };
};