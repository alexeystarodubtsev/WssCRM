"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Company_1 = require("./Company");
var Call = /** @class */ (function () {
    function Call(id, stage, points, company, date, quality, manager) {
        this.id = id;
        this.stage = stage;
        this.points = points;
        this.company = company;
        this.date = date;
        this.quality = quality;
        this.manager = manager;
        this.company = new Company_1.Company();
        this.points = [];
    }
    return Call;
}());
exports.Call = Call;
//# sourceMappingURL=Call.js.map