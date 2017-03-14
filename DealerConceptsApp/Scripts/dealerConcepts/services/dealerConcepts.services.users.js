﻿dealerConcepts.services.users = dealerConcepts.services.users || {};

dealerConcepts.services.users.create = function (data) {
    return $http.post("/api/users", data).then(onSuccess, onError);

    var onSuccess = function (response) {
        return response;
    }

    var onError = function (response) {
        return response;
    }
}


(function () {

    angular.module(APPNAME).factory("$usersService", Users);
    Users.$inject = ["$baseService", "$dealerConcepts"];

    function Users($baseService, $dealerConcepts) {
        var serviceObject = dealerConcepts.services.users;
        var service = $baseService.merge(true, {}, serviceObject, $baseService);

        return service;
    }

})();