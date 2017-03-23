dealerConcepts.services.users = dealerConcepts.services.users || {};

//dealerConcepts.services.users.create = function (data) {
//    return $http.post("/api/users", data).then(onSuccess, onError);

//    var onSuccess = function (response) {
//        return response;
//    }

//    var onError = function (response) {
//        return response;
//    }
//}


//(function () {

//    angular.module(APPNAME).factory("$usersService", Users);
//    Users.$inject = ["$baseService", "$dealerConcepts"];

//    function Users($baseService, $dealerConcepts) {
//        var serviceObject = dealerConcepts.services.users;
//        var service = $baseService.merge(true, {}, serviceObject, $baseService);

//        return service;
//    }

//})();

(function () {
    'use strict'

    angular.module(APPNAME).factory("$usersService", Users);

    Users.$inject = ['$http', "$baseService", "$dealerConcepts"];

    function Users($http, $baseService, $dealerConcepts) {

        var onSuccess = function (response) {
            return response;
        }

        var onError = function (response) {
            return response;
        };

        var serviceObject = {
            create: dealerConcepts.services.users.create = function (data, success, error) {
                return $http.post('/api/users', data).then(success, error);
            }

        , get: dealerConcepts.services.users.get = function () {
            return $http.get('/api/users').then(onSuccess, onError);
        }

    };
        var service = $baseService.merge(true, {}, serviceObject, $baseService);

        
        //service.create = function (data) {
        //    return $http.post('/api/users', data).then(onSuccess, onError);

        //    var onSuccess = function (response) {
        //        return response;
        //    }

        //    var onError = function (response) {
        //        return response;
        //    };
        //};

        //service.get = function () {
        //    return $http.get('/api/users').then(onSuccess, onError);

        //    var onSuccess = function (response) {
        //        return response;
        //    }

        //    var onError = function (response) {
        //        return response;
        //    };
        //}


        return service;
    }

}());