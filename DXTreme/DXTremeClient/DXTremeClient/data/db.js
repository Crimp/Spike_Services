﻿$(function () {
    var URL = "http://minakov-w8.corp.devexpress.com/DXTremeDemoService/ODataDemoService.svc"
    //var URL = "http://localhost:63366/ODataDemoService.svc"
    DXTremeClient.db = new DevExpress.data.EntityStoreContext({
        service: {
            url: URL,
            errorHandler: function (error) {
                alert(error.message);
            },
            beforeSend: function (sender) {
                //sender.headers.Authorization = "sam";
            }
        },
        entities: {
            Module_BusinessObjects_Contact: { key: "oid" }
        }
    });

    //var Module_BusinessObjects_Contact = [
    //{
    //    oid: "e1",
    //    FirstName: "Space Needle",
    //    LastName: "Seattle",
    //},
    //{
    //    oid: "e2",
    //    FirstName: "Chicago Loop",
    //    LastName: "Chicago",
    //}
    //];

    //DXTremeClient.db = {
    //    Module_BusinessObjects_Contact: new DevExpress.data.ArrayStore(Module_BusinessObjects_Contact),
    //};
});