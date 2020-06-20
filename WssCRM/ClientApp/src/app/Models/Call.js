"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Company_1 = require("./Company");
var Stage_1 = require("./Stage");
var Manager_1 = require("./Manager");
var Call = /** @class */ (function () {
    function Call(id, stage, points, company, date, quality, manager, correction, correctioncolor, duration, clientName, clientLink, clientState, objections, hasObjections, dateNext, hasDateNext, firstCalltoClient) {
        this.id = id;
        this.stage = stage;
        this.points = points;
        this.company = company;
        this.date = date;
        this.quality = quality;
        this.manager = manager;
        this.correction = correction;
        this.correctioncolor = correctioncolor;
        this.duration = duration;
        this.clientName = clientName;
        this.clientLink = clientLink;
        this.clientState = clientState;
        this.objections = objections;
        this.hasObjections = hasObjections;
        this.dateNext = dateNext;
        this.hasDateNext = hasDateNext;
        this.firstCalltoClient = firstCalltoClient;
        this.company = new Company_1.Company();
        this.points = [];
        this.objections = [];
        this.stage = new Stage_1.Stage();
        this.manager = new Manager_1.Manager();
        this.hasDateNext = false;
        this.firstCalltoClient = false;
    }
    return Call;
}());
exports.Call = Call;
//# sourceMappingURL=Call.js.map