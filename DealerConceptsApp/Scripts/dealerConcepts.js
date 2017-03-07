var dealerConcepts = {
    utilities: {}
    , layout: {}
    , page: {
        handlers: {
        }
        , startUp: null
    }
    , services: {}
    , ui: {}

};

dealerConcepts.moduleOptions = {
    APPNAME: "DealerConceptsApp"
        , extraModuleDependencies: []
        , runners: []
        , page: dealerConcepts.page//we need this object here for later use
}

dealerConcepts.layout.startUp = function () {

    console.debug("dealerConcepts.layout.startUp");

    //this does a null check on sabio.page.startUp
    if (dealerConcepts.page.startUp) {
        console.debug("sabio.page.startUp");
        dealerConcepts.page.startUp();
    }
};

dealerConcepts.APPNAME = "DealerConceptsApp";//legacy

$(document).ready(dealerConcepts.layout.startUp);