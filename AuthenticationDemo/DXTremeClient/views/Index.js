DXTremeClient.index = function (params) {
    var handleTripListItemClick = function (e) {
        var uri = DXTremeClient.app.router.format({
            action: "DetailView",
            oid: e.itemData.oid
        });
        DXTremeClient.app.navigate(uri);
    };
    var handleLogOffClick = function (e) {
        DXTremeClient.currentUser.UserName("");
        DXTremeClient.currentUser.Password("");
        DXTremeClient.app.navigate("LogOn/null");
    };
    return {
        todoLists: {
            store: DXTremeClient.db.Contact
        },
        handleTripListItemClick: handleTripListItemClick,
        handleLogOffClick: handleLogOffClick,
    };
};