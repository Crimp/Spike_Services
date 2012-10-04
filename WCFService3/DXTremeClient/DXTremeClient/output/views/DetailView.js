DXTremeClient.DetailView = function (params) {
    var model = {
        FirstName: ko.observable(),
        LastName: ko.observable()
    };

    var createDetailContent = function (list) {
        model.FirstName(list[0].FirstName);
        model.LastName(list[0].LastName);
    };

    DXTremeClient.db.Module_BusinessObjects_Contact.load({
        //filter: ["oid", params.oid]
        filter: ["FirstName", params.FirstName]
    }).done(createDetailContent);

    return model;
}