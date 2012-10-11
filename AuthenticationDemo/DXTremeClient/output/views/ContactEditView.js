DXTremeClient.ContactEditView = function (params) {
    var model = {
        FirstName: ko.observable(),
        LastName: ko.observable(),
        Email: ko.observable(),
        handleBackClick: function (e) {
            var uri = DXTremeClient.app.router.format({
                action: "DetailView",
                //oid: e.itemData.oid
                FirstName: e.itemData.FirstName
            });
            DXTremeClient.app.navigate(uri);
        }
    };

    var createDetailContent = function (list) {
        model.FirstName(list[0].FirstName);
        model.LastName(list[0].LastName);
        model.Email(list[0].Email);
    };

    DXTremeClient.db.Contact.load({
        //filter: ["oid", params.oid]
        filter: ["FirstName", params.FirstName]
    }).done(createDetailContent);

    //DXTremeClient.db.Contact.update(id, model);

    return model;
}