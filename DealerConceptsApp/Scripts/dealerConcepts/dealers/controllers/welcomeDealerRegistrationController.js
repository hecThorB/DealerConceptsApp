// Dealer Registration JS
(function () {
    "use strict";

    angular.module(APPNAME).controller('DealerRegistrationController', DealerRegistrationController);

    DealerRegistrationController.$inject = ['$scope', '$baseController', 'dealerRegistrationEnums', 'adminNotifications', 'model', '$window'];

    function DealerRegistrationController(
        $scope
        , $baseController
        , dealerRegistrationEnums
        //, dealerAccountInfoService
        , adminNotifications
        , model
        , $window) {

        var vm = this;

        $baseController.$.extend(vm, $baseController);

        vm.$scope = $scope;

        // Services
        vm.dealerAccountInfoService = dealerAccountInfoService;
        //vm.notify = vm.dealerAccountInfoService.getNotifier($scope);
        vm.dealerRegistrationEnums = dealerRegistrationEnums;
        vm.adminNotifications = adminNotifications;
        vm.model = model;

        // Drop Downs
        vm.dealerDropDown = vm.enums.dealershipKindEnum;
        vm.titleDropDown = vm.enums.titleKindEnum;

        // Variables
        vm.showNewDealerFormErrors = false;
        vm.submitButton = "Submit";
        vm.resetButton = "New Registration";
        vm.dealerId = vm.model.dealerId;
        vm.data = {}; // have to define this as an object so it can be read when you getById
        vm.editMode = null;
        vm.editMode = null;

        // Functions
        vm.onDealerFormSubmit = _onDealerFormSubmit;
        vm.OnDealerFormReset = _onDealerFormReset;

        render();

        function render() {
            $window.scrollTo(0, 0);

            var isAdmin = true;
            vm.dealerAccountInfoService.setPrefix(isAdmin);

            vm.$log.debug("Angular Ready");

        }
        }
})();