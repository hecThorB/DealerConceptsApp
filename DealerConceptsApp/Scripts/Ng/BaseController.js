(function () {
    "use strict";

    angular.module(APPNAME).factory('$baseController', BaseController);

    BaseController.$inject = [
        '$document'
        , '$log'];

    function BaseController(
        $document
        , $log) {

        var base = {
        $document: $document
        , $log: $log
        }
        return base;
    }
})();