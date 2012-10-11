window.DXTremeClient = window.DXTremeClient || {};
$.support.cors = true;
$(function () {
    jQuery.support.cors = true;
    app = DXTremeClient.app = new DevExpress.framework.html.HtmlApplication({
        ns: DXTremeClient,
        viewPortNode: document.getElementById("viewPort"),
        defaultLayout: "navbar",
        navigation: [
            new DevExpress.framework.Command({
                title: "Home",
                uri: "index",
                icon: "home",
                location: "navigation"
            }),
            new DevExpress.framework.Command({
                title: "About",
                uri: "about",
                icon: "about",
                location: "navigation"
            })//,
                        //new DevExpress.framework.Command({
                        //    title: "LogOn",
                        //    uri: "LogOn",
                        //    icon: "about",
                        //    location: "navigation"
                        //})
        ]
    });
    app.router.register("DetailView/:FirstName", { view: "DetailView" });
    app.router.register("LogOn/:id", { view: "LogOn", id: undefined });
    app.router.register(":view/:id", { view: "index", id: undefined });
});
