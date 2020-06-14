"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Company = /** @class */ (function () {
    function Company(name, managers, stages, daysForAnalyze, id) {
        this.name = name;
        this.managers = managers;
        this.stages = stages;
        this.daysForAnalyze = daysForAnalyze;
        this.id = id;
        this.managers = [];
        this.stages = [];
        this.daysForAnalyze = 21;
    }
    return Company;
}());
exports.Company = Company;
//# sourceMappingURL=Company.js.map