DXTremeClient.LogOn = function (params) {
    var viewModel = {
        Password: ko.observable(""),
        UserName: ko.observable(""),
        handleLogOnClick: function (e) {
            if (viewModel.UserName()) {
                DXTremeClient.currentUser.UserName(viewModel.UserName());
                DXTremeClient.currentUser.Password(viewModel.Password());
                DXTremeClient.app.navigate("");
            }
        }
    };
    return viewModel;
};