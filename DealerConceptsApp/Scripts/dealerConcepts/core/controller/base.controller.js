(function () {
    "use strict";


    //  we add baseController as a service factory (even though it will be extended by controllers, not services) so it can be injected
    angular.module(APPNAME)
    .factory('$baseController', BaseController);

    BaseController.$inject = ['$document', '$log', '$route', '$routeParams', '$systemEventService', '$alertService', '$dealerConcepts'];

    function BaseController(
        $document,
        $log,
        $route,
        $routeParams,
        $systemEventService,
        $alertService,
        $dealerConcepts

        ) {

        var base = {
            $document: $document
            , $log: $log
            , merge: $.extend
            , mapData: $.map
            , $dealerConcepts: $dealerConcepts
            , $route: $route
            , $routeParams: $routeParams
            , $systemEventService: $systemEventService
            , $alertService
            , setUpCurrentRequest: function (viewModel) {

                viewModel.currentRequest = { originalPath: "/", isTop: true };

                if (viewModel.$route.current) {
                    viewModel.currentRequest = viewModel.$route.current;
                    viewModel.currentRequest.locals = {};
                    viewModel.currentRequest.isTop = false;
                }

                viewModel.$log.log("setUpCurrentRequest firing:");
                viewModel.$log.debug(viewModel.currentRequest);
            }
            , phones: {

                format: function (number) {
                    var string = "";
                    for (var i = 0; i < number.length; i++) {
                        var index = number.charAt(i);
                        if (i == 0) {
                            string += "(";
                        } else if (i == 3) {
                            string += ")";
                        } else if (i == 6) {
                            string += "-";
                        }
                        string += index;
                    }
                    return string;
                },

                unformat: function unformatPhoneNumber(number) {
                    return number.replace("(", "").replace(")", '').replace("-", "");
                }
            }
            , enumByValue: function (enumObject) {

                var resultObject = {}; 

                for (var key in enumObject) {

                    var value = enumObject[key].value;
                    var displayName = enumObject[key].displayName;

                    resultObject[value] = enumObject[key];
                }
                return resultObject;
            }
        };

        base.getParentController = _getParentController;

        function _getParentController(controllerName) {

            var controller = null;

            if (this.$scope && this.$scope.$parent) {
                controller = this.$scope.$parent[controllerName];
            }

            return controller;

        }

        return base;
    }

})();