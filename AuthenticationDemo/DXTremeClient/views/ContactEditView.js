DXTremeClient.ContactEditView = function (params) {
    var oid;
    var model = {
        FirstName: ko.observable(),
        LastName: ko.observable(),
        Email: ko.observable(),
        
        handleSaveClick: function (e) {
            DXTremeClient.db.Contact.update(
                new DevExpress.data.Guid(oid),
                { FirstName: model.FirstName(), LastName: model.LastName(), Email: model.Email() }
                );
        },
        handleBackClick: function (e) {
            DXTremeClient.app.navigate("_back");
        }
    };
    
    var createDetailContent = function (list) {
        model.FirstName(list[0].FirstName);
        model.LastName(list[0].LastName);
        model.Email(list[0].Email);
        oid = list[0].oid;
    };
    DXTremeClient.db.Contact.load({
        filter: ["oid", new DevExpress.data.Guid(params.oid)]
    }).done(createDetailContent);
    return model;
}