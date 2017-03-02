dealerConcepts.services.admin = dealerConcepts.services.admin || {};

dealerConcepts.services.admin.notifications = dealerConcepts.services.admin.notifications || {};

dealerConcepts.services.admin.notifications.success = function (message, optionalTitle) {
	toastr.success(message, optionalTitle);
};

dealerConcepts.services.admin.notifications.error = function (message, optionalTitle) {
	toastr.error(message, optionalTitle);
};

toastr.options = {
	"closeButton": false,
	"debug": false,
	"progressBar": false,
	"preventDuplicates": false,
	"positionClass": "toast-top-center",
	"onclick": null,
	"showDuration": "100",
	"hideDuration": "1000",
	"timeOut": "1500",
	"extendedTimeOut": "1000",
	"showEasing": "swing",
	"hideEasing": "linear",
	"showMethod": "fadeIn",
	"hideMethod": "fadeOut"
};

dealerConcepts.services.admin.notifications.prompt = function (message, okCallBack) {
	//sweet alert 
	swal({
		title: "",
		text: message,
		showCancelButton: true,
		confirmButtonColor: "#DD6B55",
		confirmButtonText: "Yes"
	},
    function (isConfirm) {
    	if (isConfirm) {
    		okCallBack();
    	}
    }
    );
};

(function () {
	if (angular) {
		angular.module(APPNAME).factory("adminNotifications", adminNotifications);

		adminNotifications.$inject = ["$baseService", "$dealerConcepts"];
		function adminNotifications($baseService, $dealerConcepts) {
			var serviceObject = dealerConcepts.services.admin.notifications;
			var service = $baseService.merge(true, {}, serviceObject, $baseService);
			return service;
		}
	}
})();
