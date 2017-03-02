dealerConcepts.services.consumer = dealerConcepts.services.consumer || {};

dealerConcepts.services.consumer.notifications = dealerConcepts.services.consumer.notifications || {};

dealerConcepts.services.consumer.notifications.success = function (text, title) {
    toastr.success(text, title)
};

dealerConcepts.services.consumer.notifications.error = function (text, title) {
    toastr.error(text, title)
};

dealerConcepts.services.consumer.notifications.prompt = function (message, okCallBack) {
    swal({
        title: "",
        text: message,
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes",
        closeOnConfirm: true
    }, function (isConfirm) {
        if (isConfirm) {
            okCallBack();
        }
    });
};

toastr.options = {
    "closeButton": true,
    "debug": true,
    "progressBar": false,
    "preventDuplicates": false,
    "positionClass": "toast-top-center",
    "showDuration": "400",
    "hideDuration": "1000",
    "timeOut": "7000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

(function () {
    if (angular) {
        angular.module(APPNAME).factory("consumerNotifications", consumerNotifications);

        consumerNotifications.$inject = ["$baseService", "$dealerConcepts"];
        function consumerNotifications($baseService, $dealerConcepts) {
            var serviceObject = dealerConcepts.services.consumer.notifications;
            var service = $baseService.merge(true, {}, serviceObject, $baseService);
            return service;
        }
    }
})();