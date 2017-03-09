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
var APPNAME = "DealerConceptsApp";
dealerConcepts.moduleOptions = {
    APPNAME: "DealerConceptsApp"
        , extraModuleDependencies: []
        , runners: []
        , page: dealerConcepts.page//we need this object here for later use
}

