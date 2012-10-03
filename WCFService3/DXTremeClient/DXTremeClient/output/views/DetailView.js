DXTremeClient.DetailView = function (params) {
    var model = {
        pageTitle: ko.observable(),
        events: ko.observable()
    };

    var createDetailContent = function (list) {
        model.pageTitle(list[0].title);
        model.events(list[0].events);
    };

    DXTremeClient.db.Module_BusinessObjects_Contact.load({
        filter: ["oid", params.oid]
    }).done(createDetailContent);

    return model;
}