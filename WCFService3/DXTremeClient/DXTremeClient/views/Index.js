DXTremeClient.index = function (params) {
    var viewModel = {
        todoLists: {
            store: DXTremeClient.db.Module_BusinessObjects_Contact
        }
    }
    return viewModel;
};