window.DXTremeClient = window.DXTremeClient || {};
$.support.cors = true;
$(function () {
    jQuery.support.cors = true;
    app = new DevExpress.framework.html.HtmlApplication({
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
            })
        ]
    });
    app.router.register(":view/:id", { view: "index", id: undefined });
    app.router.register("DetailView/:contactOid", { view: "DetailView" });
});
