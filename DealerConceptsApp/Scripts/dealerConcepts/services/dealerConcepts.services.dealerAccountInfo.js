dealerConcepts.services.dealerAccountInfo = dealerConcepts.services.dealerAccountInfo || {};
dealerConcepts.services.dealerAccountInfo.apiPrefix = "api/dealer";
dealerConcepts.services.dealerAccountInfo.apiPrefixAdmin = "/api/v2/admin/dealers";
dealerConcepts.services.dealerAccountInfo.apiPrefixKiosk = "/api/dealers/";

dealerConcepts.services.dealerAccountInfo.setPrefix = function (isAdmin) {
    if (isAdmin) {
        dealerConcepts.services.dealerAccountInfo.apiPrefix = dealerConcepts.services.dealerAccountInfo.apiPrefixAdmin;
    }
    else {
        dealerConcepts.services.dealerAccountInfo.apiPrefix = dealerConcepts.services.dealerAccountInfo.apiPrefixKiosk;
    }
}

dealerConcepts.services.dealerAccountInfo.insert = function (data, onSuccess, onError) {
    var url = "/api/dealers/accountinfo";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);

};


(function () {
    if (angular) {
        angular.module(APPNAME).factory("dealerAccountInfoService", DealerAccountInfoService);

        DealerAccountInfoService.$inject = ["$baseService", "$dealerConcepts"];
        function DealerAccountInfoService($baseService, $dealerConcepts) {
            var serviceObject = dealerConcepts.services.dealerAccountInfo;
            var service = $baseService.merge(true, {}, serviceObject, $baseService);
            return service;
        }
    }
})();