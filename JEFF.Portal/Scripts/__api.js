var urlResolver = {
    resolve: function (url) {
        url = (typeof (url) === 'undefined' || url === null) ? '' : url;
        return url.replace("~/", window.BASE_API_ADDRESS);
    }
};

var api = {
    resolveUrl: function (url) {
        url = (typeof (url) === 'undefined' || url === null) ? '' : url;
        return url.replace("~/", window.BASE_API_ADDRESS);
    },
    onFail: function (xhr) {
        switch (xhr.status) {
            case 401:
                console.log("unauthorized");
                //TODO: redirect?
                break;
            case 404:
                system.log("not found");
                app.showMessage('Element not found...');
                break;
            case 500:
                console.log("server error");
                break;
            default:
                console.log("error with status code: " + xhr.status);
                break;
        }
    },
    get: function (url, data) {
        var self = this;
        var resolvedUrl = urlResolver.resolve(url);
        var jqxhr = $.ajax({
            dataType: "json",
            url: resolvedUrl,
            data: data
        })
        .done(function () {
            //console.log("success");
        })
        .fail(function (xhr) {
            self.onFail(xhr);
        });
        return jqxhr;
    },
    save: function (url, id, data) {
        var self = this;
        var resolvedUrl = urlResolver.resolve(url);
        var verb = id == '' ? "POST" : "PUT";
        var jqxhr = $.ajax({
            contentType: 'application/json',
            url: resolvedUrl,
            data: ko.toJSON(data),
            type: verb
        })
        .done(function () {
            //console.log("success");
        })
        .fail(function (xhr) {
            self.onFail(xhr);
        });
        return jqxhr;
    },
    //
    post: function (url, data) {
        var self = this;
        var resolvedUrl = urlResolver.resolve(url);
        var jqxhr = $.ajax({
            contentType: 'application/json',
            url: resolvedUrl,
            data: ko.toJSON(data),
            type: "POST"
        })
        .done(function () {
            //console.log("success");
        })
        .fail(function (xhr) {
            self.onFail(xhr);
        });
        return jqxhr;
    },
    put: function (url, data) {
        var self = this;
        var resolvedUrl = urlResolver.resolve(url);
        var jqxhr = $.ajax({
            contentType: 'application/json',
            url: resolvedUrl,
            data: ko.toJSON(data),
            type: "PUT"
        })
        .done(function () {
            //console.log("success");
        })
        .fail(function (xhr) {
            self.onFail(xhr);
        });
        return jqxhr;
    },
    del: function (url, id) {
        var self = this;
        var deletedUrl = '';
        if (id)
            deletedUrl = urlResolver.resolve(url) + "?id=" + id;
        else
            deletedUrl = urlResolver.resolve(url);
        var jqxhr = $.ajax({
            contentType: 'application/json',
            url: deletedUrl,
            type: "DELETE"
        })
        .done(function () {
            //console.log("success");
        })
        .fail(function (xhr) {
            self.onFail(xhr);
        });
        return jqxhr;
    }
};