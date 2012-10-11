﻿DXTremeClient.DetailView = function (params) {
    var model = {
        FirstName: ko.observable(),
        LastName: ko.observable(),
        Email: ko.observable(),
        handleBackClick: function (e) {
            DXTremeClient.app.navigate("");
        },
        handleEditClick: function (e) {
            var uri = DXTremeClient.app.router.format({
                action: "ContactEditView",
                //oid: e.itemData.oid
                FirstName: model.FirstName
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